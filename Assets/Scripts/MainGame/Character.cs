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
    float waitTimeBullet = 0.5f;
    [SerializeField]
    float levelBullet = 1;
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
    protected bool enabledEffect = false;
    [SerializeField]
    private MeshRenderer meshNormal;
    [SerializeField]
    private MeshRenderer meshSkin;
    [SerializeField]
    private float intensityEffectColor = 3f;
    protected GameManager GM;
    [SerializeField]
    private AudioSource shootAudio;
    [SerializeField]
    private AudioSource healingAudio;
    [SerializeField]
    private AudioSource upgradeAudio;
    [SerializeField]
    private AudioSource hitAudio;
    [SerializeField]
    private AudioSource shieldAudio;
    protected virtual void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = healthBase;
        ShieldState(shieldBlueState);
        EffectSpaceship(false);
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
            if (shootAudio.gameObject.activeSelf)
                shootAudio.Play();
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
            // bullets[0].GetComponent<BulletHandler>().activeBullet();
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
        GM.updateHealthBar(health / healthBase);
    }
    public void divHealth(float value)
    {
        if (CheckShield()) return;
        health -= value;
        health = Mathf.Clamp(health, 0, healthBase);
        if (this.gameObject.name.Equals("MainPlayer"))
        {
            GM.updateHealthBar(health / healthBase);
        }
        if (health <= 0 && !this.gameObject.name.Equals("MainPlayer"))
        {
            StopAllCoroutines();
            GM.increaseScore(1);
            GM.ResetCreep(this.gameObject);
        }
        if (health <= 0 && this.gameObject.name.Equals("MainPlayer"))
        {
            GM.GameOver();
        }
        // Debug.Log(health);
    }
    public bool CheckShield()
    {
        if (shieldBlueState)
        {
            ShieldState(false);
            return true;
        }
        return false;
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
        shieldBlueState = state;
        objShield.SetActive(state);
        if (state) shieldAudio.Play();
        else shieldAudio.Stop();
    }
    public void CharacterExplosion(int state)
    {
        if (shieldBlueState) return;
        switch (state)
        {
            case 0:
                {
                    Debug.Log("Player death");
                    GM.GameOver();
                    break;
                }
            case 1:
                {
                    GM.increaseScore(1);
                    GM.ResetCreep(this.gameObject);
                    break;
                }
            default:
                break;
        }
    }
    public void EffectSpaceship(bool state)
    {
        enabledEffect = state;
        if (state)
        {
            meshSkin.sharedMaterial.SetInt("_EmissionEffect", 1);
            meshSkin.enabled = true;
            meshNormal.enabled = false;
            StartCoroutine(spaceshipEmissionEffect());
        }
        else
        {
            healingAudio.Stop();
            // upgradeAudio.Stop();
            meshSkin.sharedMaterial.SetInt("_EmissionEffect", 0);
            meshNormal.enabled = true;
            meshSkin.enabled = false;
        }
    }

    IEnumerator spaceshipEmissionEffect()
    {
        float angle = 0;
        while (enabledEffect)
        {
            float temp = 0.275f * Mathf.Sin(angle * Mathf.PI / 180.0f) + 0.875f;
            temp = Mathf.Clamp(temp, 0.6f, 1.15f);
            angle += 30f;
            if (angle > 360) angle = 0;
            meshSkin.sharedMaterial.SetFloat("_FresnelEffect", temp);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void StartEffectHurt()
    {
        StartCoroutine(EffectHurt());
    }
    public void StartEffectHealing()
    {
        StartCoroutine(EffectHealing());
    }
    public void StartEffectUpdateSpaceship()
    {
        StartCoroutine(EffectUpdateSpaceship());
    }

    protected IEnumerator EffectHurt()
    {
        EffectSpaceship(true);
        hitAudio.Play();
        spaceshipEmissionEffect();
        float factor = Mathf.Pow(2, intensityEffectColor);
        Color color = new Color(factor * 191f / 255f, factor * 9f / 255f, 0f, 1f);
        meshSkin.sharedMaterial.SetColor("_EmissionColor", color);
        yield return new WaitForSeconds(0.15f);
        EffectSpaceship(false);
    }
    protected IEnumerator EffectHealing()
    {
        EffectSpaceship(true);
        healingAudio.Play();
        spaceshipEmissionEffect();
        float factor = Mathf.Pow(2, intensityEffectColor);
        Color color = new Color(0f, factor * 191f / 255f, factor * 65f / 255f, 1f);
        meshSkin.sharedMaterial.SetColor("_EmissionColor", color);
        while (enabledEffect && health < healthBase)
        {
            plusHealth(0.1f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        EffectSpaceship(false);
    }
    protected IEnumerator EffectUpdateSpaceship()
    {
        EffectSpaceship(true);
        upgradeAudio.Play();
        spaceshipEmissionEffect();
        float factor = Mathf.Pow(2, intensityEffectColor);
        Color color = new Color(0f, factor * 79f / 255f, factor * 191f / 255f, 1f);
        meshSkin.sharedMaterial.SetColor("_EmissionColor", color);
        yield return new WaitForSeconds(1f);
        EffectSpaceship(false);
    }

    public void increaseBulletDamage()
    {
        levelBullet += 0.25f;
        if (levelBullet <= 3)
        {
            StartEffectUpdateSpaceship();
            waitTimeBullet = 0.5f / levelBullet;
        }
    }
}
