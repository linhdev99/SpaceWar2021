using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageIncurred : MonoBehaviour
{
    [SerializeField]
    private float decreaseDamage = 50f;
    [SerializeField]
    private bool isDecreaseDamage = false;
    private void OnTriggerEnter(Collider other)
    {
        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Character objChar = transform.parent.gameObject.transform.parent.gameObject.GetComponent<Character>();
        if (other.gameObject.CompareTag("BulletPlayer"))
        {
            if (isDecreaseDamage)
            {
                GM.IncurDamaged(objChar, other.gameObject.GetComponent<BulletHandler>().getDamage() * (decreaseDamage / 100f));
            }
            else
            {
                GM.IncurDamaged(objChar, other.gameObject.GetComponent<BulletHandler>().getDamage());
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            GM.CharacterExplosion(objChar, 1);
        }
    }
}
