using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public bool canMove = false;
    public float _speed = 20f;
    [HideInInspector]
    public Vector3 dirBullet;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }
    void Move()
    {
        rb.velocity = dirBullet * _speed;
    }
}
