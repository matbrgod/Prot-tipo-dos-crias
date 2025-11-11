using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public int playerHealth;
    public List<InventorySaveData> inventorySaveData;
}

[System.Serializable]
public class SceneSaveData
{
    public string sceneName;
    public SaveData data;
}

[System.Serializable]
public class AllSceneSaves
{
    public List<SceneSaveData> scenes = new List<SceneSaveData>();
}