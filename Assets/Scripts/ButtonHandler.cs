using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UltimaCenaJogada()
    {
        CenasManager.Instance.UltimaCenaCarregada();
    }
}
