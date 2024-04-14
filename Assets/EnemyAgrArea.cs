using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgrArea : MonoBehaviour
{
    public GameObject[] Enemys;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Allies")
        {
            foreach (GameObject enemy in Enemys)
            {
                try
                {
                    enemy.gameObject.GetComponent<EnemyAI>().stop = false;
                }
                catch
                {
                }
            }
        }
    }
}
