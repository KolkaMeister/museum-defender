using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    GameObject Quests;
    // Start is called before the first frame update
    void Awake()
    {
        Quests = GameObject.Find("QuestTriggers");
        UpdateQuestText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateQuestText() {
        for (int i = 0; i < Quests.transform.childCount; i++)
        {
            GameObject Quest = Quests.transform.GetChild(i).gameObject;
            GameObject.Find("QuestViewText").GetComponent<TMP_Text>().text = "";
            if (Quest.activeSelf) {
                Debug.Log(Quest.GetComponent<CQuest>().ProgressString);
                GameObject.Find("QuestViewText").GetComponent<TMP_Text>().text += Quest.GetComponent<CQuest>().ProgressString + "\n";
            }
        }
    }

}
