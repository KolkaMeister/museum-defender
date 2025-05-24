using Dialogs;
using Dialogs.Nodes;
using UI;
using UnityEngine;
using Zenject;

public class SignalTowerStart : MonoBehaviour
{
    [SerializeField] private string _olegDialogFirst;
    [SerializeField] private string _olegDialogEndTag;
    [SerializeField] private string _olegDialogSecond;

    private DialogNode _olegDialogEndNode;
    private IDialogDataProvider _provider;

    [SerializeField] GameObject SignalTowerQuestGO;

    [Inject]
    public void Construct(IDialogDataProvider provider)
    {
        _provider = provider;
    }

    private void Start()
    {
        _olegDialogEndNode = _provider.Find(_olegDialogFirst).Find(_olegDialogEndTag);
        _olegDialogEndNode.OnPhraseEnded += TagDone;
    }

    private void OnDestroy()
    {
        _olegDialogEndNode.OnPhraseEnded -= TagDone;
    }

    private void TagDone()
    {
        GameObject go = Instantiate(SignalTowerQuestGO, GameObject.Find("QuestTriggers").transform);
        SignalTowerQuest quest = go.GetComponent<SignalTowerQuest>();
        quest.transform.name = "SignalTowerQuestGO";
        quest.OnCompleted += OnQuestCompleted;
        GameObject.Find("QuestViewText").GetComponent<QuestView>().UpdateQuestText();
    }
    private void OnQuestCompleted()
    {
        var nextDialog = _provider.Find(_olegDialogSecond);
        DialogStarter dialogStarter = GetComponent<DialogStarter>();
        dialogStarter.ChangeDialog(nextDialog);
    }
}
