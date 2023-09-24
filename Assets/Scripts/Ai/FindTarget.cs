using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    public string targetTag = "";
    // Start is called before the first frame update
    public void FindTargets() {
        
        GameObject[] myItems = GameObject.FindGameObjectsWithTag(targetTag);
        //Debug.Log("Found " + myItems.Length + " instances with this script attached");
        try {
            GetComponent<EnemyAI>().target = myItems[Random.Range(0, myItems.Length)].transform;
        }
        catch {
            // Debug.Log("��� �����");
        }
        
    }
    void Start()
    {
        FindTargets();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
