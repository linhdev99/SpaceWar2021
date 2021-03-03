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
    private float waitTimeSpawnCreep = 10f;
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
            creeps[0].transform.position = creepPoints[Random.Range(0,2)].position;
            creeps[0].SetActive(true);
            creeps[0].GetComponent<Creep>().canMove = true;
            StartCoroutine(KillCreep(creeps[0]));
            creeps.Remove(creeps[0]);
            canSpawnCreep = false;
            yield return new WaitForSeconds(waitTimeSpawnCreep);
            canSpawnCreep = true;
        }
    }
    IEnumerator KillCreep(GameObject obj) 
    {
        yield return new WaitForSeconds(5f);
        creeps.Add(obj);
        obj.SetActive(false);
    }
}
