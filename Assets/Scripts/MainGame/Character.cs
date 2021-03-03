using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletObject;
    List<GameObject> bullets = new List<GameObject>();
    List<GameObject> activeBullets = new List<GameObject>();
    public Transform transAttack;
    ParticleSystem effect;
    bool canShoot = true;
    [SerializeField]
    float waitTimeBullet = 0.05f;
    [SerializeField]
    int bulletLength = 50;
    protected Vector3 dirBullet;
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    protected virtual void FixedUpdate()
    {

    }
    protected void InitBullet()
    {
        for (int i = 0; i < bulletLength; i++)
        {
            GameObject temp = Instantiate(bulletObject, transAttack.position, transAttack.rotation);
            temp.SetActive(false);
            bullets.Add(temp);
        }
    }
    protected void Shoot()
    {
        if (canShoot)
        {
            StartCoroutine(SpawnBullet());
        }
    }
    IEnumerator SpawnBullet()
    {
        if (bullets.Count > 0)
        {
            bullets[0].transform.position = transAttack.transform.position;
            bullets[0].SetActive(true);
            bullets[0].GetComponent<BulletHandler>().canMove = true;
            bullets[0].GetComponent<BulletHandler>().dirBullet = dirBullet;
            StartCoroutine(KillBullet(bullets[0]));
            bullets.Remove(bullets[0]);
            canShoot = false;
            yield return new WaitForSeconds(waitTimeBullet);
            canShoot = true;
        }
    }

    IEnumerator KillBullet(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        bullets.Add(obj);
        obj.SetActive(false);
    }
}
