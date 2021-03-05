using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FogController : MonoBehaviour
{
    public GameObject[] clouds;
    public float _speed = 1f;
    bool canSpawn = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            _speed = Random.Range(85, 100) / 100f;
            StartCoroutine(cloudMove(Random.Range(0, clouds.Length)));
        }
    }

    IEnumerator cloudMove(int idx)
    {
        clouds[idx].SetActive(true);
        float newY = clouds[idx].transform.position.y;
        Vector3 defaultPos = clouds[idx].transform.position;
        while (newY >= -40)
        {
            newY -= Time.deltaTime * 40f / _speed;
            clouds[idx].transform.position = new Vector3(clouds[idx].transform.position.x, newY, clouds[idx].transform.position.z);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(Random.Range(1,10) / 10f);
        canSpawn = true;
        clouds[idx].SetActive(false);
        clouds[idx].transform.position = defaultPos;
    }
    // IEnumerator cloudMove2(int idx)
    // {
    //     clouds[idx].SetActive(true);
    //     float newY = clouds[idx].transform.position.y;
    //     Vector3 defaultPos = clouds[idx].transform.position;
    //     Vector3 newPos = new Vector3(clouds[idx].transform.position.x, -41f, clouds[idx].transform.position.z);
    //     while (newY >= -40)
    //     {
    //         clouds[idx].transform.position = Vector3.Lerp(clouds[idx].transform.position, newPos, Time.deltaTime * _speed);
    //         newY = clouds[idx].transform.position.y;
    //         yield return new WaitForSeconds(Time.deltaTime);
    //     }
    //     canSpawn = true;
    //     clouds[idx].SetActive(false);
    //     clouds[idx].transform.position = defaultPos;
    // }
}
