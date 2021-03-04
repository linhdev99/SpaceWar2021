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
    protected float health;
    [SerializeField]
    protected float healthBase;
    [SerializeField]
    protected bool shieldBlueState = false;
    [SerializeField]
    protected GameObject objShield;
    [SerializeField]
    protected bool increasePower = false;
    private MeshRenderer mesh;
    protected virtual void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        health = healthBase;
        ShieldState(shieldBlueState);
        IncreasePower();
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
            bullets[0].GetComponent<BulletHandler>().activeBullet();
            bullets.Remove(bullets[0]);
            StartCoroutine(KillBullet(temp));
            canShoot = false;
            yield return new WaitForSeconds(waitTimeBullet);
            canShoot = true;
        }
    }

    IEnumerator KillBullet(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        bullets.Add(obj);
        // obj.SetActive(false);
    }
    public void setDefaultBullets()
    {
        bullets.Clear();
        canShoot = true;
        foreach (GameObject obj in defaultBullets)
        {
            // obj.SetActive(false);
            // obj.GetComponent<BulletHandler>().canMove = false;
            bullets.Add(obj);
        }
    }


    public float getHealth()
    {
        return health;
    }
    public void setHealth(float value)
    {
        health = value;
    }
    public void plusHealth(float value)
    {
        health += value;
    }
    public void divHealth(float value)
    {
        if (shieldBlueState) return;
        health -= value;
        health = Mathf.Clamp(health, 0, healthBase);
        if (health <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ResetCreep(this.gameObject);
        }
        Debug.Log(health);
    }
    public bool isDeath()
    {
        if (health <= 0) return true;
        else return false;
    }
    public float getHealthBase()
    {
        return healthBase;
    }
    public void setHealthBase(float value)
    {
        healthBase = value;
    }
    public void plusHealthBase(float value)
    {
        healthBase += value;
    }
    public void divHealthBase(float value)
    {
        float healthPre = healthBase;
        healthBase -= value;
        healthBase = Mathf.Clamp(healthBase, 0, healthPre);
    }
    public void ShieldState(bool state)
    {
        objShield.SetActive(state);
    }
    public void CharacterExplosion(int state)
    {
        if (shieldBlueState) return;
        switch (state)
        {
            case 0:
                {
                    Debug.Log("Player death");
                    break;
                }
            case 1:
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().ResetCreep(this.gameObject);
                    break;
                }
            default:
                break;
        }
    }
    public void IncreasePower()
    {

        if (increasePower)
        {
            mesh.sharedMaterial.SetInt("_EmissionEffect", 1);
            StartCoroutine(spaceshipEmissionEffect());
        }
        else
        {
            mesh.sharedMaterial.SetInt("_EmissionEffect", 0);
        }
    }

    IEnumerator spaceshipEmissionEffect()
    {
        float angle = 0;
        while (increasePower)
        {
            float temp = 1.2f * Mathf.Sin(angle * Mathf.PI / 180.0f);
            temp = Mathf.Clamp(temp, -0.8f, 1.15f);
            angle += 10f;
            if (angle > 360) angle = 0;
            mesh.sharedMaterial.SetFloat("_FresnelEffect", temp);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
