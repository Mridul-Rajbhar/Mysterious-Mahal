using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public float speed,jumpForce;
    private bool canJump = true;
    private float goBack = -0.5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;
    public Transform attackPoint;
    private GameObject ground;
    public float attackRange = 0.5f;
    public int damage = 20, playerLife =100;
    public LayerMask enemyLayers;
    public float attackRate = 2f;

    
 
    float nextAttackTime = 0f;
    float InputX;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ground = transform.Find("GroundColliders").gameObject;

    }
    void Update()
    {
        AttackFunction();
        InputFunction();
       
    }

    void InputFunction()
    {
        InputX = Input.GetAxis("Horizontal");
        if (InputX > 0)
            RightMove();
        else if (InputX < 0)
            LeftMove();
        else
            animator.SetBool("Run", false);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Tilemap" || collision.gameObject.tag == "MovingBricks")
        {
            canJump = true;
        }
        animator.SetBool("Jump", false);
    }

    void AttackFunction()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Jump()
    {   
        body.velocity = new Vector2(body.velocity.x,jumpForce);
        animator.SetBool("Jump", true);
        canJump = false;
    }
    void RightMove()
    {
        animator.SetBool("Run",true);
        
        
        if (spriteRenderer.flipX != true)
        {
            Debug.Log(goBack);
        goBack = -1 * goBack;
        attackPoint.localPosition = new Vector3(-1f * attackPoint.localPosition.x, attackPoint.localPosition.y, attackPoint.localPosition.z);
        }

        
        spriteRenderer.flipX = true;
        body.velocity = new Vector2(InputX*speed, body.velocity.y);
    }

    void LeftMove()
    {
        animator.SetBool("Run",true);
        
        if (spriteRenderer.flipX != false)
        {
            Debug.Log(goBack);
            goBack = -1 * goBack;
            attackPoint.localPosition = new Vector3(-1f * attackPoint.localPosition.x, attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
        
        spriteRenderer.flipX = false;
        body.velocity = new Vector2(InputX*speed, body.velocity.y);

    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,enemyLayers);
        if (hitEnemies.Length != 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
    public void TakeDamage( int playerDamage)
    {
        animator.SetTrigger("Hurt");
        animator.SetBool("Dead", false);
      //  Debug.Log(playerDamage);
        playerLife -= playerDamage;
       
            Vector3 goBackAfterHurt = new Vector3(transform.position.x + goBack, transform.position.y, transform.position.z);
            transform.position = goBackAfterHurt;
       
        if (playerLife<=0)
        {
            Die();

        }
    }

    void Die()
    {
        EnemyAI.playerAlive = false;
        Debug.Log("mar gya");
        animator.SetBool("Dead", true);
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);   
    }
}
