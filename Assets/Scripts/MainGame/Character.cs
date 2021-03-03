using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletObject;
    List<GameObject> bullets = new List<GameObject>();
    List<GameObject> defaultBullets = new List<GameObject>();
    public Transform transAttack;
    ParticleSystem effect;
    public bool canShoot = true;
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
            defaultBullets.Add(temp);
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
            GameObject temp = bullets[0];
            bullets[0].transform.position = transAttack.transform.position;
            bullets[0].SetActive(true);
            bullets[0].GetComponent<BulletHandler>().canMove = true;
            bullets[0].GetComponent<BulletHandler>().dirBullet = dirBullet;
            bullets.Remove(bullets[0]);
            StartCoroutine(KillBullet(temp));
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
    public void setDefaultBullets()
    {
        bullets.Clear();
        canShoot = true;
        foreach (GameObject obj in defaultBullets)
        {
            obj.SetActive(false);
            obj.GetComponent<BulletHandler>().canMove = false;
            bullets.Add(obj);
        }
    }
}
