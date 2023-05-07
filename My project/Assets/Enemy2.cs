using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 4f;
    
    public float range = 5f;
    public float attackDelay = 1f;
    public GameObject projectilePrefab;
   

    public float pushForce = 0.5f;
    public float pushForce2 = 0.1f;
    public float pushDuration = 0.5f;
    public float pushDuration2 = 0.2f;
    public bool isPushing = false;

    public Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private int hp;

    private float attackTimer = 0.5f;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hp = 2;
        
     
    }

    // Update is called once per frame
    void Update()
    {
        
        LookAt();

        // Move towards player

        direction = player.position - transform.position;
        // Check if in attack range
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= range)
        {

            if (attackTimer <= 0)
            {
                // Shoot projectile
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 500f);
                attackTimer = attackDelay;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        if (isPushing == false)
        {
            Vector2 direction = player.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

        }
    }

    void LookAt()
    {
        direction = player.position - transform.position;

        direction.y = 0;

        if (direction.x > 0)
        {
            animator.SetBool("moveLeft", false);
        }
        else
        {
            animator.SetBool("moveLeft", true);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet_P"))
        {
            hp -= 1;
            if (hp == 0)
            {
                Destroy(gameObject);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPushing = true;
            if (pushDuration > 0f)
            {
                Vector2 pushDirection = transform.position - collision.gameObject.transform.position;
                StartCoroutine(AddForceCoroutine(rb, pushDirection.normalized)); // �о�� �ڷ�ƾ ����
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            isPushing = true;
            if (pushDuration > 0f)
            {
                Vector2 pushDirection = transform.position - collision.gameObject.transform.position;
                StartCoroutine(AddForceCoroutine2(rb, pushDirection.normalized)); // �о�� �ڷ�ƾ ����
            }
        }
    }
    IEnumerator AddForceCoroutine(Rigidbody2D enemyRb, Vector2 pushDirection)
    {
        isPushing = true;
        enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // ���� �о���ϴ�.
        yield return new WaitForSeconds(pushDuration); // ���� �ð� ���
        enemyRb.velocity = Vector2.zero; // ���� �ӵ� �ʱ�ȭ
        isPushing = false;
    }

    IEnumerator AddForceCoroutine2(Rigidbody2D enemyRb, Vector2 pushDirection)
    {
        isPushing = true;
        enemyRb.AddForce(pushDirection * pushForce2, ForceMode2D.Impulse); // ���� �о���ϴ�.
        yield return new WaitForSeconds(pushDuration2); // ���� �ð� ���
        enemyRb.velocity = Vector2.zero; // ���� �ӵ� �ʱ�ȭ
        isPushing = false;
    }

}