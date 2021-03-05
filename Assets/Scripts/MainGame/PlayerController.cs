using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool canMove = true;
    private Rigidbody rb;
    [SerializeField]
    private float _deltaSpeed = 0.1f;
    private Vector2 startPos;
    private Vector2 direction;
    Vector2 cur;
    Vector2 pre;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        IdleShip();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            GetInput();
        }
    }
    void GetInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = Camera.main.ScreenToWorldPoint(touch.position);
                    cur = startPos;
                    break;
                case TouchPhase.Stationary:
                    // Record initial touch position.
                    cur = Camera.main.ScreenToWorldPoint(touch.position);
                    IdleShip();
                    break;
                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    pre = new Vector2(cur.x, cur.y);
                    cur = Camera.main.ScreenToWorldPoint(touch.position);
                    // Debug.Log("Cur");
                    // Debug.Log(cur);
                    // Debug.Log("Start");
                    // Debug.Log(startPos);
                    // Debug.Log("NewPos");
                    direction = cur - pre;
                    Debug.DrawLine(pre, cur, Color.red, 1f);
                    // Debug.Log(direction);
                    Move(direction);
                    break;

                case TouchPhase.Ended:
                    StopMove();
                    break;
            }
        }
    }
    void Move(Vector2 dir)
    {
        // rb.velocity = new Vector3(dir.x, dir.y, 0f) * _speed;
        if (dir.x < 0)
        {
            LeftShip();
        }
        else if (dir.x > 0)
        {
            RightShip();
        }
        float newX = transform.position.x + dir.x * (1f + _deltaSpeed);
        float newY = transform.position.y + dir.y * (1f + _deltaSpeed);
        if (newX < -8.2f) newX = -8.2f;
        if (newX > 8.2f) newX = 8.2f;
        if (newY < -15f) newY = -15f;
        if (newY > 10f) newY = 10f;

        // transform.position = new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0f);
        rb.MovePosition(new Vector3(newX, newY, 0f));
    }
    void StopMove()
    {
        IdleShip();
    }

    void IdleShip()
    {
        anim.SetBool("idle", true);
        anim.SetBool("left", false);
        anim.SetBool("right", false);
    } 
    void LeftShip()
    {
        anim.SetBool("left", true);
        anim.SetBool("right", false);
        anim.SetBool("idle", false);
    } 
    void RightShip()
    {
        anim.SetBool("right", true);
        anim.SetBool("left", false);
        anim.SetBool("idle", false);
    } 
}
