using Dialogs;
using Dialogs.Nodes;
using UI;
using UnityEngine;
using Zenject;

public class OlegDialogEnd : MonoBehaviour
{
    [SerializeField] private string _olegDialogName;
    [SerializeField] private string _olegDialogEndTag;
    private DialogNode _olegDialogEndNode;
    private IDialogDataProvider _provider;

    [Inject]
    public void Construct(IDialogDataProvider provider)
    {
        _provider = provider;
    }

    private void Start()
    {
        _olegDialogEndNode = _provider.Find(_olegDialogName).Find(_olegDialogEndTag);
        _olegDialogEndNode.OnPhraseEnded += LoadScene;
    }

    private void OnDestroy()
    {
        _olegDialogEndNode.OnPhraseEnded -= LoadScene;
    }

    private void LoadScene()
    {
        SceneLoader.LoadScene(0, false);
    }
}