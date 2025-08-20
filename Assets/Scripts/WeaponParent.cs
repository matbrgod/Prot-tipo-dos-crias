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

    bool isAttacking = false;

    float atkduration = 0.3f;
    float atkTimer = 0f;
    public GameObject Melee;

    private void Update()
    {
        CheckMeleeTimer();

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
        Melee.SetActive(true);
        var collider = Melee.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
        Physics2D.SyncTransforms();
        
        isAttacking = true;
        StartCoroutine(AttackDelay());

    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkduration)
            {
                atkTimer = 0f;
                isAttacking = false;
                Melee.SetActive(false);
                                
            }
        }
    }
}
