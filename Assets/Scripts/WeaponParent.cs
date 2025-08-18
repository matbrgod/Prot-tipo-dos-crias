using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 Pointerposition { get; set; }
    public float offset = 0f;

    public Animator animator;
    public float delay = 0.5f;
    private bool attackBlocked;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
        Vector2 scale = transform.localScale;

        if (Mathf.Abs(rotation_z) > 90)
        {
            scale.y = -1;
        }
        else if (Mathf.Abs(rotation_z) < 90)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

    }
    public void Attack()
    {
        if (attackBlocked) return;
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(AttackDelay());

    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}
