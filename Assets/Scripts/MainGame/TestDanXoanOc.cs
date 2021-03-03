using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanXoanOc : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    float angle = 0f;
    float radius = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        radius += Time.deltaTime * 2f;
        rb.MovePosition(new Vector3(transform.position.x + radius * Mathf.Sin((angle * Mathf.PI / 180f)), transform.position.y + radius * Mathf.Cos((angle * Mathf.PI / 180f)), 0f));
        angle += 10;
        if (angle >= 360)
        {
            angle = 0;
        }
    }
}
