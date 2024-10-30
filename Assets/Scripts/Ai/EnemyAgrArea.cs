using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgrArea : MonoBehaviour
{
    public GameObject[] Enemys;
    private int CountCollision = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Allies")
        {
            CountCollision += 1;
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Allies")
        {
            CountCollision -= 1;
            if (CountCollision <= 0) {
                foreach (GameObject enemy in Enemys)
                {
                    try
                    {
                        enemy.gameObject.GetComponent<EnemyAI>().stop = true;
                    }
                    catch
                    {
                    }

                }
            }
        }
    }
}
