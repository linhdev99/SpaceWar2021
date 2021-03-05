using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fog2Controller : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public bool stopFog = true;
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(moveFog());
    }
    void Update()
    {
    }
    IEnumerator moveFog()
    {
        float offsetX = 0.25f;
        float offsetY = -0.5f;
        float n = 0.5f;
        while (!stopFog)
        {
            offsetY += Time.deltaTime * _speed;
            if (offsetY > n)
            {
                offsetX = Random.Range(-25, 25) / 100f;
                // offsetY = -0.5f;
                n += 1f;
            }
            Vector2 offset = new Vector2(offsetX, offsetY);
            meshRenderer.sharedMaterials[0].SetTextureOffset("_BaseMap", offset);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
