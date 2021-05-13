using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour
{

    #region Private Variables
    private bool flipX = false;
    private GameObject ground;
    private Rigidbody2D body;
    private float goBack = 0.5f;
    int currentHealth;
    #endregion

    #region Public Variables
    public Animator animator;
    public int maxHealth = 100;
    public Vector3 playerPosition;
    public LayerMask player;
    public GameObject attackPoint;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        ground = transform.Find("GroundColliders").gameObject;
    }

    private void Update()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        flipEnemy();
    }

    public void flipEnemy()
    {
        if (transform.position.x < playerPosition.x  &&  flipX!=true)
        {
            goBack = -1 * goBack;
            flipX = true;
            float temp = -1 * transform.localScale.x;

            transform.localScale = new Vector3(temp, transform.localScale.y, transform.localScale.z);
        }
        else if (playerPosition.x < transform.position.x && flipX !=false)
        {
            flipX = false;
            goBack = -1 * goBack;
            float temp = -1 * transform.localScale.x;
            transform.localScale = new Vector3(temp, transform.localScale.y, transform.localScale.z);
        }
    }


    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;

        //Debug.Log(transform.position.x);

        Vector2 Back = new Vector2(transform.position.x + goBack, transform.position.y);
        transform.position = Back;
        
        //Debug.Log(transform.position.x);
        if(currentHealth<=0) 
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        foreach (Collider2D c in ground.GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
        this.enabled = false;
    }


}
