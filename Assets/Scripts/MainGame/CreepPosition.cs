using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreepPosition : MonoBehaviour
{
    [SerializeField]
    private Transform point;
    void FixedUpdate()
    {
        point.position = new Vector3(Random.Range(-57,57) / 10f, point.position.y, point.position.z);
    }
}
