using UnityEngine;

public class S_anim : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("IsPushed", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("IsPushed", false);
        }
    }
}
