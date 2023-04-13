using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 3f;
    public float pushForce = 0.5f;
    public float pushDuration = 0.5f;
    public bool isPushing = false;

   
    public GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAt();
    }



    void Move()
    {
        if (isPushing == false)
        {
            Vector2 direction = player.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
           
        }

      
        
    }

    void LookAt()
    {
        if (transform.position.x < player.transform.position.x)
        {
            animator.SetBool("moveLeft", false);
        }

        else
        {
            animator.SetBool("moveLeft", true);
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
                StartCoroutine(AddForceCoroutine(rb, pushDirection.normalized)); // 밀어내는 코루틴 시작
            } 
        }
    }

    IEnumerator AddForceCoroutine(Rigidbody2D enemyRb, Vector2 pushDirection)
    {
        isPushing = true;
        enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // 적을 밀어냅니다.
        yield return new WaitForSeconds(pushDuration); // 일정 시간 대기
        enemyRb.velocity = Vector2.zero; // 적의 속도 초기화
        isPushing = false;
    }
}
