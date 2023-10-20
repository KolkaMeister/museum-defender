using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    public string targetTag = "Enemies";
    public string targetTagPVE = "Landmark";
    public string weaponTag = "Weapon";
    private GameObject[] myItems;
    EnemyAI enemy;
    // Start is called before the first frame update
    public void FindTargets() {
        if (enemy.PVE)
        {
            myItems = GameObject.FindGameObjectsWithTag(targetTagPVE);
        }
        else {
            if (transform.Find("HoldPoint").transform.childCount == 0)
            {
                enemy.stopDistance = 0;
                myItems = GameObject.FindGameObjectsWithTag(weaponTag);
            }
            else {
                myItems = GameObject.FindGameObjectsWithTag(targetTag);
            }
            
        }
        
        
        //Debug.Log("Found " + myItems.Length + " instances with this script attached");
        try {
            enemy.target = myItems[Random.Range(0, myItems.Length)].transform;
        }
        catch {
            Debug.Log("Нет целей");
            enemy.PVE = true;
            enemy.stopDistance = 0;
        }
        
    }
    public bool CheckTag() {
        try
        {
            if (enemy.target.tag == targetTag ||
            enemy.target.tag == targetTagPVE ||
            enemy.target.tag == weaponTag)
            {
                return true;
            }
        }
        catch { }
        return false;
    }
    void Start()
    {
        enemy = GetComponent<EnemyAI>();
        FindTargets();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
