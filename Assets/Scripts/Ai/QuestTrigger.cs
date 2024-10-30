using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string triggerName;

    private GameObject QuestObj;
    void Start()
    {
        try
        {
            QuestObj = GameObject.Find("QusetTriggers");
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Игрок не найден на тригере: " + this.gameObject);
            throw;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Allies") {
            QuestObj.GetComponent<QuestScripter>().TriggerScript(triggerName);
        }
    }
}
