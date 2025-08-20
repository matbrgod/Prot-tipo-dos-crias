using UnityEngine;
using System.Collections.Generic;

public class Melee : MonoBehaviour
{
    public float damage;
    private HashSet<Collider2D> hitThisAttack = new HashSet<Collider2D>();

    private void OnEnable()
    {
        hitThisAttack.Clear();
    }
    private void OnDisable()
{
    hitThisAttack.Clear();
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDealDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDealDamage(collision);
    }

    private void TryDealDamage(Collider2D collision)
    {
        if (hitThisAttack.Contains(collision)) return;

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            hitThisAttack.Add(collision);
        }

        Rocks rock = collision.GetComponent<Rocks>();
        if (rock != null)
        {
            rock.ReceberDano(damage);
            hitThisAttack.Add(collision);
        }
    }
}