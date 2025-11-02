using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f; // Lower = snappier, higher = smoother
    public Vector3 offset = new Vector3(0, 0, -10);

    private Vector3 velocity = Vector3.zero;
    [Header("Caminho da câmera")]
    public Transform[] pontos; // Adicione aqui os pontos no inspetor
    public float velocidade = 1.5f;
    public float tempoEspera = 2f;

    private int indiceAtual = 0;
    private float tempoParado = 0f;

    // Lock/override camera state
    private bool isLocked = false;
    private Vector3 lockedPosition;
    private Quaternion lockedRotation;
    private float lockMoveSpeed = 5f;
    private Coroutine lockCoroutine;

    void Start()
    {
        if (target == null && Player.instance != null)
        {
            target = Player.instance.transform;
        }
    }

    void Update()
    {
        // If camera is locked (cutscene/pan), move directly toward lockedPosition and don't run patrol path
        if (isLocked)
        {
            // Move towards locked position smoothly
            transform.position = Vector3.MoveTowards(transform.position, lockedPosition, lockMoveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, lockedRotation, Time.deltaTime * lockMoveSpeed);
            return;
        }

        // Patrol path logic (when pontos provided)
        if (pontos != null && pontos.Length > 0)
        {
            Transform alvo = pontos[indiceAtual];
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidade * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, alvo.rotation, Time.deltaTime * velocidade);

            if (Vector3.Distance(transform.position, alvo.position) < 0.1f)
            {
                tempoParado += Time.deltaTime;
                if (tempoParado >= tempoEspera)
                {
                    indiceAtual = (indiceAtual + 1) % pontos.Length;
                    tempoParado = 0f;
                }
            }
        }
    }

    void LateUpdate()
    {
        // While locked, do not follow the player
        if (isLocked) return;

        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    /// <summary>
    /// Immediately pan camera to worldPosition, hold for holdTime seconds, then resume following the player.
    /// If a lock is already active it will be replaced.
    /// </summary>
    public void LockCameraAt(Vector3 worldPosition, Quaternion worldRotation, float moveSpeed = 5f, float holdTime = 2f)
    {
        if (lockCoroutine != null) StopCoroutine(lockCoroutine);
        lockCoroutine = StartCoroutine(LockCameraRoutine(worldPosition, worldRotation, moveSpeed, holdTime));
    }

    private System.Collections.IEnumerator LockCameraRoutine(Vector3 worldPosition, Quaternion worldRotation, float moveSpeed, float holdTime)
    {
        isLocked = true;
        lockedPosition = worldPosition;
        lockedRotation = worldRotation;
        lockMoveSpeed = Mathf.Max(0.01f, moveSpeed);

        // Wait until camera reaches the locked position (or timeout)
        float maxMoveWait = 5f;
        float elapsed = 0f;
        while (Vector3.Distance(transform.position, lockedPosition) > 0.05f && elapsed < maxMoveWait)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Hold at position for the requested time
        float holdElapsed = 0f;
        while (holdElapsed < holdTime)
        {
            holdElapsed += Time.deltaTime;
            yield return null;
        }

        // Resume following
        isLocked = false;
        lockCoroutine = null;
    }

    // Optional convenience overload to lock by Transform
    public void LockCameraAt(Transform t, float moveSpeed = 5f, float holdTime = 2f)
    {
        if (t == null) return;
        LockCameraAt(t.position, t.rotation, moveSpeed, holdTime);
    }
}