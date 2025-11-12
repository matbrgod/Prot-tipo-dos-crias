using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class TelaPreta : MonoBehaviour
{
    [SerializeField] private GameObject telaPreta;
    [SerializeField] private GameObject persephone;
    [SerializeField] private GameObject tumulo;
    [SerializeField] private GameObject triggerE;
    [SerializeField] private GameObject newTrigger;
    [SerializeField] private GameObject quest01;
    [SerializeField] private GameObject quest1;
    public float fadeDuration = 0.5f;
    public float holdDuration = 3f;

    private Image telaImage;
    private CanvasGroup canvasGroup;
    private bool canPress;
    private Coroutine running;

    void Start()
    {
        if (telaPreta == null) return;

        // try Image first, then CanvasGroup
        telaImage = telaPreta.GetComponent<Image>();
        canvasGroup = telaPreta.GetComponent<CanvasGroup>();

        // if no CanvasGroup, add one (better control over whole canvas)
        if (canvasGroup == null)
            canvasGroup = telaPreta.AddComponent<CanvasGroup>();

        // start hidden
        SetAlpha(0f);
        telaPreta.SetActive(false);
    }

    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E) && running == null)
        {
            telaPreta.SetActive(true);
            running = StartCoroutine(FadeSequence());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPress = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPress = false;
        }
    }

    private IEnumerator FadeSequence()
    {
        // fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
        persephone.SetActive(false);
        quest01.SetActive(false);
        tumulo.SetActive(true);
        newTrigger.SetActive(true);
        // hold
        yield return new WaitForSeconds(holdDuration);
        // fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        telaPreta.SetActive(false);
        quest1.SetActive(true);
        running = null;
        Destroy(triggerE);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / Mathf.Max(0.0001f, duration));
            SetAlpha(a);
            yield return null;
        }
        SetAlpha(to);
    }

    private void SetAlpha(float a)
    {
        if (telaImage != null)
        {
            Color c = telaImage.color;
            c.a = a;
            telaImage.color = c;
        }
        if (canvasGroup != null)
        {
            canvasGroup.alpha = a;
            // option: block raycasts while visible
            canvasGroup.blocksRaycasts = a > 0f;
            canvasGroup.interactable = a > 0f;
        }
    }
}