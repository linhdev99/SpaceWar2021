using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyGameObject(2f));
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator destroyGameObject(float time) 
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
