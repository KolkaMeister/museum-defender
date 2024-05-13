using Dialogs;
using Dialogs.Nodes;
using UI;
using UnityEngine;
using Zenject;

public class SignalTowerStart : MonoBehaviour
{
    [SerializeField] private string _olegDialogName;
    [SerializeField] private string _olegDialogEndTag;
    private DialogNode _olegDialogEndNode;
    private IDialogDataProvider _provider;

    [SerializeField] GameObject SignalTowerQuest;

    [Inject]
    public void Construct(IDialogDataProvider provider)
    {
        _provider = provider;
    }

    private void Start()
    {
        _olegDialogEndNode = _provider.Find(_olegDialogName).Find(_olegDialogEndTag);
        _olegDialogEndNode.OnPhraseEnded += TagDone;
    }

    private void OnDestroy()
    {
        _olegDialogEndNode.OnPhraseEnded -= TagDone;
    }

    private void TagDone()
    {
        GameObject ins = Instantiate(SignalTowerQuest, GameObject.Find("QuestTriggers").transform);
        ins.transform.name = "SignalTowerQuest";
        GameObject.Find("QuestViewText").GetComponent<QuestView>().UpdateQuestText();
    }
}
