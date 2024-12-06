using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NotesUI : MonoBehaviour
{
    UIDocument UIDocument;
    VisualElement root;
    VisualElement background;
    //VisualElement LeftPanel;
    //VisualElement RightPanel;
    [SerializeField]
    VisualTreeAsset NotesItemAsset;
    [SerializeField]
    bool UnlockAll;
    ItemListController itemListController = new ItemListController();
    private void OnEnable()
    {
        UIDocument = GetComponent<UIDocument>();
        root = UIDocument.rootVisualElement;
        background = root.Q("Background");
        background.transform.scale = new Vector3(1, 1, 1);
        //LeftPanel = background.Q("LeftPanel");
        //RightPanel = background.Q("RightPanel");

        itemListController.InitializeItemList(background, NotesItemAsset, UnlockAll);
    }
    public void AddItem(NotesItem notesItem)
    {
        itemListController.AddItem(notesItem);
    }
    public void Toggle()
    {
        if (root.style.visibility == Visibility.Hidden)
        {
            Show();
        }
        else
            Hide();
    }
    private void Hide()
    {
        root.style.visibility = Visibility.Hidden;
    }
    private void Show()
    {
        root.style.visibility = Visibility.Visible;
    }
}
