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
    [SerializeField]
    private float damage = 1f;
    [SerializeField]
    private float maxDamage = 100f;
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
    public float getDamage()
    {
        return damage;
    }
    public void increaseDamage(float value)
    {
        damage += value;
    }
    public void decreaseDamage(float value)
    {
        damage -= value;
        damage = Mathf.Clamp(damage, 1f, maxDamage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WallTop") || other.gameObject.CompareTag("WallBottom"))
        {
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Shield"))
        {
            GameObject.Find("MainPlayer").GetComponent<Character>().CheckShield();
            this.gameObject.SetActive(false);
        }
    }
    // public void activeBullet()
    // {
    //     StartCoroutine(hideBullet(3f));
    // }
    // IEnumerator hideBullet(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     this.gameObject.SetActive(false);
    // }
}
