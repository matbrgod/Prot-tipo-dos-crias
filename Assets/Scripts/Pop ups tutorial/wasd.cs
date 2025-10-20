using UnityEngine;

public class wasd : MonoBehaviour
{
    public Transform player; // Assign this in the Inspector
    public Vector3 offset = new Vector3(0, 1, 0); // 1 unit above

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}