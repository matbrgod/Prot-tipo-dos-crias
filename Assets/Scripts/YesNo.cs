using System.Collections;
using UnityEngine;

public class YesNo : MonoBehaviour
{
    public GameObject yesButton;
    public GameObject noButton;

    public void ShowButtons()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);
    }

    public void HideButtons()
    {
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    private IEnumerator Start()
    {
        HideButtons();
        yield return new WaitForSeconds(2.4f);
        ShowButtons();
    }
}
