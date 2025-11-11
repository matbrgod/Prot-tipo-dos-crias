using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;

    private static SaveController instance;
    private AllSceneSaves allSaves = new AllSceneSaves();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadAllSaves();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // tenta usar a instância singleton primeiro
        inventoryController = InventoryController.Instance ?? FindObjectOfType<InventoryController>();

        var entry = allSaves.scenes.Find(s => s.sceneName == scene.name);
        if (entry != null && entry.data != null)
        {
            StartCoroutine(ApplySaveWhenReady(entry.data));
        }
    }

    public void SaveCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("SaveController: Player não encontrado ao salvar cena.");
            return;
        }

        inventoryController = InventoryController.Instance ?? inventoryController ?? FindObjectOfType<InventoryController>();

        var items = inventoryController != null ? inventoryController.GetInventoryItems() : null;
        Debug.Log($"SaveCurrentScene: salvando cena '{sceneName}' com {items?.Count ?? 0} itens.");

        SaveData saveData = new SaveData
        {
            playerPosition = playerObj.transform.position,
            playerHealth = 0,
            inventorySaveData = items
        };

        var existing = allSaves.scenes.Find(s => s.sceneName == sceneName);
        if (existing != null)
        {
            existing.data = saveData;
        }
        else
        {
            allSaves.scenes.Add(new SceneSaveData { sceneName = sceneName, data = saveData });
        }

        SaveAllToFile();
    }

    private void LoadAllSaves()
    {
        if (File.Exists(saveLocation))
        {
            var json = File.ReadAllText(saveLocation);
            var loaded = JsonUtility.FromJson<AllSceneSaves>(json);
            if (loaded != null)
                allSaves = loaded;
        }
        else
        {
            SaveAllToFile();
        }
    }

    private void SaveAllToFile()
    {
        try
        {
            File.WriteAllText(saveLocation, JsonUtility.ToJson(allSaves));
            Debug.Log($"SaveController: arquivo salvo em {saveLocation}");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"SaveController: falha ao salvar arquivo. {ex.Message}");
        }
    }

    private IEnumerator ApplySaveWhenReady(SaveData saveData)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        inventoryController = InventoryController.Instance ?? inventoryController ?? FindObjectOfType<InventoryController>();

        int maxFramesToWait = 300;
        int waited = 0;
        while ((playerObj == null || inventoryController == null || !inventoryController.IsReady) && waited < maxFramesToWait)
        {
            yield return null;
            waited++;
            playerObj = playerObj == null ? GameObject.FindGameObjectWithTag("Player") : playerObj;
            inventoryController = InventoryController.Instance ?? inventoryController ?? FindObjectOfType<InventoryController>();
        }

        if (playerObj != null && saveData != null)
        {
            playerObj.transform.position = saveData.playerPosition;
        }
        else
        {
            Debug.LogWarning("SaveController: Não foi possível aplicar posição do player (Player ou saveData nulos).");
        }

        if (inventoryController != null && inventoryController.IsReady && saveData != null && saveData.inventorySaveData != null)
        {
            Debug.Log($"ApplySaveWhenReady: aplicando {saveData.inventorySaveData.Count} itens ao inventário.");
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
        else
        {
            Debug.LogWarning("SaveController: InventoryController não pronto ou não encontrado — items não restaurados.");
        }
    }

    private void OnApplicationQuit()
    {
        SaveCurrentScene();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveCurrentScene();
    }

    [ContextMenu("Log Save Location")]
    public void LogSaveLocation()
    {
        if (string.IsNullOrEmpty(saveLocation))
            saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        Debug.Log($"Save file path: {saveLocation}");
    }

    [ContextMenu("Force Save Current Scene")]
    public void ContextSaveCurrentScene()
    {
        SaveCurrentScene();
    }
}
