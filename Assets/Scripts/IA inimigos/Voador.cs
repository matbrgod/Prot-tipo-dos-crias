using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Voador : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public float followSpeed = 1.3f;
    private bool caçando = false;
    private bool atacou = false;
    public float healthEnemy;
    public float maxHealhtEnemy = 30;
    public float timer1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthEnemy = maxHealhtEnemy;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 posToGo = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        // Move the camera towards the player's position
        // Lerp is used to smoothly transition the camera position
        /*if (caçando == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToGo, followSpeed*Time.deltaTime);
        }*/
        if (caçando == true)
        {
            Caçando();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caçando = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caçando = false;
            Animator anim = GetComponent<Animator>();
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
            anim.Play("Idle");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            healthEnemy -= 10;
            if (healthEnemy <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            /*atacou = true;
            caçando = false;
            timer1 = 2f;
            timer1 -= 1f * Time.deltaTime;
            if(timer1 < 0)
            {
                atacou = false;
                caçando = true;
            }*/
        }
    }
    void Caçando()
    {
        if (atacou == false)
        {
            if (caçando == true)
            {
                Collider2D col = GetComponent<Collider2D>();
                col.enabled = true;
                Animator anim = GetComponent<Animator>();
                anim.Play("Voando");
                Vector3 posToGo = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, posToGo, followSpeed * Time.deltaTime);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        healthEnemy -= damage;
        if (healthEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    /*void Timer(tempo)
    {
        timer1 -= tempo * Time.deltaTime
    }*/
}