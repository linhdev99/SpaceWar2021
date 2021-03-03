using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Character
{
    protected override void Start()
    {
        InitBullet();
        dirBullet = transform.forward;
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        Shoot();
        base.FixedUpdate();
    }
}
