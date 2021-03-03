using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [HideInInspector] public MeshRenderer meshRenderer;
    public float _speed = 0.5f;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMoveDown();
    }
    
    void backgroundMoveDown()
    {
        Vector2 offset = new Vector2(0, Time.time * _speed);
        meshRenderer.sharedMaterials[0].SetTextureOffset("_BaseMap", offset);
        // Debug.Log(offset);
    }
}
