using UnityEngine;

public class E_anim : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsPushed", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("IsPushed", false);
        }
    }
}
