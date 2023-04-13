using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int speed = 0;
    private Animator animator;
    private Rigidbody2D rb;         // Rigidbody2D 컴포넌트
    private Vector2 moveDirection;  // 이동 방향 벡터

    private Camera mainCamera;
    private Vector3 mousePosition;
    
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        mainCamera = Camera.main;

        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Move();
        Character();
      
      
    }

    void FixedUpdate()
    {
        // Rigidbody2D 컴포넌트의 velocity 속성을 이용해 이동
        rb.velocity = moveDirection * speed;
    }

    void Character()
    {
        Vector3 look;
        look.x = mousePosition.x - transform.position.x;
        look.y = mousePosition.y - transform.position.y;
        look.z = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isFrontIdle", false);
            animator.SetBool("isSideIdle", false);
            animator.SetBool("isBackIdle", false);

            if (look.x > 0 && look.y > 0)
            {
                if (look.x > look.y)
                {
                    animator.SetBool("isMoveFront", false);
                    animator.SetBool("isMoveSide", true);
                    animator.SetBool("isMoveBack", false);

                  
                }

                else
                {
                    animator.SetBool("isMoveFront", false);
                    animator.SetBool("isMoveSide", false);
                    animator.SetBool("isMoveBack", true);
                }
            }

            if (look.x > 0 && look.y < 0)
            {
                if (look.x > Mathf.Abs(look.y))
                {
                    animator.SetBool("isMoveFront", false);
                    animator.SetBool("isMoveSide", true);
                    animator.SetBool("isMoveBack", false);
                }

                else
                {
                    animator.SetBool("isMoveFront", true);
                    animator.SetBool("isMoveSide", false);
                    animator.SetBool("isMoveBack", false);
                }
            }

            if (look.x < 0 && look.y < 0)
            {
                if (Mathf.Abs(look.x) > Mathf.Abs(look.y))
                {
                    animator.SetBool("isMoveFront", false);
                    animator.SetBool("isMoveSide", true);
                    animator.SetBool("isMoveBack", false);
                }

                else
                {
                    animator.SetBool("isMoveFront", true);
                    animator.SetBool("isMoveSide", false);
                    animator.SetBool("isMoveBack", false);
                }
            }

            if (look.x < 0 && look.y > 0)
            {
                if (Mathf.Abs(look.x) > Mathf.Abs(look.y))
                {
                    animator.SetBool("isMoveFront", false);
                    animator.SetBool("isMoveSide", true);
                    animator.SetBool("isMoveBack", false);
                }

                else
                {
                    animator.SetBool("isMoveFront", false);
                    animator.SetBool("isMoveSide", false);
                    animator.SetBool("isMoveBack", true);
                }
            }
        }

        else
        {
            animator.SetBool("isMoveFront", false);
            animator.SetBool("isMoveSide", false);
            animator.SetBool("isMoveBack", false);

            if (look.x > 0 && look.y > 0)
            {
                if (look.x > look.y)
                {
                    animator.SetBool("isFrontIdle", false);
                    animator.SetBool("isSideIdle", true);
                    animator.SetBool("isBackIdle", false);


                }

                else
                {
                    animator.SetBool("isFrontIdle", false);
                    animator.SetBool("isSideIdle", false);
                    animator.SetBool("isBackIdle", true);
                }
            }

            if (look.x > 0 && look.y < 0)
            {
                if (look.x > Mathf.Abs(look.y))
                {
                    animator.SetBool("isFrontIdle", false);
                    animator.SetBool("isSideIdle", true);
                    animator.SetBool("isBackIdle", false);
                }

                else
                {
                    animator.SetBool("isFrontIdle", true);
                    animator.SetBool("isSideIdle", false);
                    animator.SetBool("isBackIdle", false);
                }
            }

            if (look.x < 0 && look.y < 0)
            {
                if (Mathf.Abs(look.x) > Mathf.Abs(look.y))
                {
                    animator.SetBool("isFrontIdle", false);
                    animator.SetBool("isSideIdle", true);
                    animator.SetBool("isBackIdle", false);
                }

                else
                {
                    animator.SetBool("isFrontIdle", true);
                    animator.SetBool("isSideIdle", false);
                    animator.SetBool("isBackIdle", false);
                }
            }

            if (look.x < 0 && look.y > 0)
            {
                if (Mathf.Abs(look.x) > Mathf.Abs(look.y))
                {
                    animator.SetBool("isFrontIdle", false);
                    animator.SetBool("isSideIdle", true);
                    animator.SetBool("isBackIdle", false);
                }

                else
                {
                    animator.SetBool("isFrontIdle", false);
                    animator.SetBool("isSideIdle", false);
                    animator.SetBool("isBackIdle", true);
                }
            }

        }
    }
    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        /* if (Input.GetKey(KeyCode.A))
         {

             transform.Translate(Vector3.left * speed * Time.deltaTime);
         }

         if (Input.GetKey(KeyCode.D))
         {

             transform.Translate(Vector3.right * speed * Time.deltaTime);
         }

         if (Input.GetKey(KeyCode.W))
         {

             transform.Translate(Vector3.up * speed * Time.deltaTime);
         }

         if (Input.GetKey(KeyCode.S))
         {

             transform.Translate(Vector3.down * speed * Time.deltaTime);
         }*/




    }
    
   

}
