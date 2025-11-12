using System.Collections;
using UnityEngine;

public class HealthFadeIn : MonoBehaviour
{   
    
    public GameObject healthPopUp;
    void Start()
    {
        if (healthPopUp != null)
            healthPopUp.SetActive(false);
        StartCoroutine(delayPopUp());
    }

    private IEnumerator delayPopUp()
    {
        yield return new WaitForSeconds(0.8f);
        if (healthPopUp != null)
        healthPopUp.SetActive(true);
    }
}
