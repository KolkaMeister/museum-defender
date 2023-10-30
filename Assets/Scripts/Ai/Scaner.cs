using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] float Scanertime = 0.1f;
    IEnumerator Scaners()
    {
        gameObject.GetComponent<AstarPath>().Scan();
        yield return new WaitForSeconds(Scanertime);
        StartCoroutine(Scaners());
        // Start is called before the first frame update
    }
    void Start()
    {
            StartCoroutine(Scaners());
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
