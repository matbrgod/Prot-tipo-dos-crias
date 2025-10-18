using UnityEngine;

public class Mouse_anim : MonoBehaviour
{
     public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsPushed", true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsPushed", false);
        }
    }
}
