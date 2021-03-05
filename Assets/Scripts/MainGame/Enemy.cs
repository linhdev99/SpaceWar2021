using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float _speed;
    Rigidbody rb;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        dirBullet = transform.forward;
        InitBullet();
        Shoot();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Shoot();
        base.Update();
    }
    protected override void FixedUpdate()
    {
        Shoot();
        base.FixedUpdate();
    }

    protected void Move(Vector3 dir)
    {
        rb.velocity = dir * _speed;
    }
}
