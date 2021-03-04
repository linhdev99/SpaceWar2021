using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject creep;
    private List<GameObject> creeps = new List<GameObject>();
    [SerializeField]
    private List<Transform> creepPoints;
    [SerializeField]
    private int creepLength = 10;
    [SerializeField]
    private float waitTimeSpawnCreep = 5f;
    private bool canSpawnCreep = true;
    void Start()
    {
        InitCreep();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PoolCreep();
    }
    void InitCreep()
    {
        for (int i = 0; i < creepLength; i++)
        {
            GameObject temp = Instantiate(creep, creepPoints[0].position, creep.transform.rotation);
            temp.SetActive(false);
            temp.GetComponent<Creep>().canMove = false;
            temp.GetComponent<Creep>().canShoot = false;
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
            creeps[0].transform.position = creepPoints[Random.Range(0, 2)].position;
            creeps[0].SetActive(true);
            creeps[0].GetComponent<Creep>().canMove = true;
            creeps[0].GetComponent<Creep>().canShoot = true;
            creeps.Remove(creeps[0]);
            StartCoroutine(KillCreep(temp));
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
    }

    public void IncurDamaged(Character objChar, float value)
    {
        objChar.divHealth(value);
    }

    public void CharacterExplosion(Character objChar, int state)
    {
        objChar.CharacterExplosion(state);
    }
}
