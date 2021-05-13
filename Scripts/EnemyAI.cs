using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyAI : MonoBehaviour
{
    #region Public Variables

    public float attackDistance, alertDistance, attackRate, nextAttackTime;
    public BoxCollider2D bodyBox;
    public static bool playerAlive = true;
    public int damage;

    #endregion
    
    #region Private Variables
    [SerializeField] private LayerMask layer;
    private GameObject target;
    //private PlayerCombat player;
    private float distance;
    private RaycastHit2D hit;
    private bool playerEntered;
    private Animator anim;
    
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //player = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        
        if(playerEntered)
        {
            distance = Vector2.Distance(transform.position, target.transform.position);
            anim.SetBool("Alert", true);
            if (Time.time >= nextAttackTime)
            {
                checkInAttackRange();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            drawRange();
        }
        else
        {
            anim.SetBool("Alert", false);
        }
    }

    private void drawRange()
    {
        Vector2 dir;
        if (transform.localScale.x > 0)
            dir = Vector2.left;
        else
            dir = Vector2.right;
        hit = Physics2D.Raycast(bodyBox.bounds.center,dir,alertDistance, layer);
        Color rayColour;
        if (hit.collider != null)
        {
            rayColour = Color.green;
           
        }
        else if(hit.collider == null)
        {
            rayColour = Color.red;
       
        }
        else
        {
            rayColour = Color.white;
        }
        ///Debug.Log(hit.collider);
        Debug.DrawRay(bodyBox.bounds.center,dir*alertDistance, rayColour);
        Debug.DrawRay(bodyBox.bounds.center, dir * attackDistance, Color.red);
    }

    private void checkInAttackRange()
    {
        
        if(distance<=attackDistance && playerAlive)
        {
          
            anim.SetTrigger("Attack");
            target.GetComponent<PlayerCombat>().TakeDamage(damage);
          
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
            playerEntered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerEntered = false;
        }
    }


}


