using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : Enemy
{
    // Start is called before the first frame update
    public bool canMove = false;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (canMove)
        {
            Move(-Vector3.up);
        }
        base.Update();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallBottom"))
        {
            GM.ResetCreep(this.gameObject);
        }
    }
}
