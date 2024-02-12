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
        if (enemy != null)
        {
            if (enemy.PVE)
            {
                enemy.stopDistance = 0;
                myItems = GameObject.FindGameObjectsWithTag(targetTagPVE);
            }
            else
            {
                if (transform.Find("HoldPoint").transform.childCount == 0)
                {
                    enemy.stopDistance = 0;
                    myItems = GameObject.FindGameObjectsWithTag(weaponTag);
                }
                else
                {
                    myItems = GameObject.FindGameObjectsWithTag(targetTag);
                }

            }


            //Debug.Log("Found " + myItems.Length + " instances with this script attached");
            try
            {
                if (enemy.PVE)
                {
                    enemy.target = myItems[Random.Range(0, myItems.Length)].transform;
                }
                else {
                    enemy.target = GetNearestItem(myItems).transform;
                }
            }
            catch
            {
                Debug.Log("Нет целей");
                enemy.PVE = true;
                enemy.stopDistance = 0;
            }
        }
        else {
            Debug.Log("aaa");
        }
    }
    public GameObject GetNearestItem(GameObject[] myItems)
    {
        // Инициализируем переменные
        float minDistance = float.MaxValue;
        GameObject nearestItem = null;

        // Перебираем массив объектов
        for (int i = 0; i < myItems.Length; i++)
        {
            // Рассчитываем расстояние до текущего объекта
            float distance = Vector3.Distance(myItems[i].transform.position, transform.position);

            // Если расстояние меньше текущего минимального расстояния, то обновляем переменные
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestItem = myItems[i];
            }
        }

        // Возвращаем ближайший объект
        return nearestItem;
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
    void Awake()
    {
        enemy = GetComponent<EnemyAI>();
    }
    private void Start()
    {
        FindTargets();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
