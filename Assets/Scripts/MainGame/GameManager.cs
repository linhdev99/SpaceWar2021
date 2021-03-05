using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject creep;
    private List<GameObject> creeps = new List<GameObject>();
    [SerializeField]
    private Transform creepPoint;
    [SerializeField]
    private int creepLength = 10;
    [SerializeField]
    private float waitTimeSpawnCreep = 5f;
    private bool canSpawnCreep = true;
    
    [SerializeField]
    private GameObject objExplosion;
    private int score = 0;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private AudioSource gameoverAudio;
    void Start()
    {
        Time.timeScale = 0;
        score = 0;
        InitCreep();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PoolCreep();
    }
    public void increaseScore(int value) 
    {
        score += value;
        uiManager.updateScore(score);
    }
    public void updateHealthBar(float value) 
    {
        uiManager.updateHealthBar(value);
    }
    void InitCreep()
    {
        for (int i = 0; i < creepLength; i++)
        {
            GameObject temp = Instantiate(creep, creepPoint.position, creep.transform.rotation);
            temp.SetActive(false);
            temp.GetComponent<Creep>().canMove = false;
            temp.GetComponent<Creep>().canShoot = false;
            temp.GetComponent<Creep>().EffectSpaceship(false);
            creeps.Add(temp);
        }
    }

    void PoolCreep()
    {
        if (canSpawnCreep)
        {
            StartCoroutine(SpawnCreep());
        }
    }
    IEnumerator SpawnCreep()
    {
        if (creeps.Count > 0)
        {
            GameObject temp = creeps[0];
            creeps[0].transform.position = new Vector3(Random.Range(-57,57) / 10f, creepPoint.position.y, creepPoint.position.z);
            creeps[0].SetActive(true);
            creeps[0].GetComponent<Creep>().canMove = true;
            creeps[0].GetComponent<Creep>().canShoot = true;
            creeps[0].GetComponent<Creep>().EffectSpaceship(false);
            creeps.Remove(creeps[0]);
            // StartCoroutine(KillCreep(temp));
            canSpawnCreep = false;
            yield return new WaitForSeconds(waitTimeSpawnCreep);
            canSpawnCreep = true;
        }
    }
    IEnumerator KillCreep(GameObject obj)
    {
        yield return new WaitForSeconds(6f);
        if (obj.activeSelf)
        {
            ResetCreep(obj);
        }
        yield return new WaitForSeconds(1f);
    }

    public void ResetCreep(GameObject obj)
    {
        creeps.Add(obj);
        obj.SetActive(false);
        obj.GetComponent<Creep>().setDefaultBullets();
        GameObject explosion = Instantiate(objExplosion, new Vector3(obj.transform.position.x, obj.transform.position.y - 2.35f, obj.transform.position.z), Quaternion.identity);
    }

    public void IncurDamaged(Character objChar, float value)
    {
        objChar.divHealth(value);
    }

    public void CharacterExplosion(Character objChar, int state)
    {
        objChar.CharacterExplosion(state);
    }

    public void GameOver() 
    {
        gameoverAudio.Play();
        uiManager.GameOver();
    }

}
