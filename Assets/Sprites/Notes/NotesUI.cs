using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
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
    [SerializeField]
    Character _character;
    [SerializeField] bool OpenOnStart = false;
    private void OnCharacterInventory_ItemGrabbed(NotesItem item)
    {
       AddItem(item);
    }
    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.C))
        //{
        //    Show();   
        //}
        //if(Input.GetKeyUp(KeyCode.Escape))
        //{
        //    Hide(); 
        //}
    }
    private void Start()
    {
        UIDocument = GetComponent<UIDocument>();
        root = UIDocument.rootVisualElement;
        background = root.Q("Background");
        background.transform.scale = new Vector3(1, 1, 1);
        //LeftPanel = background.Q("LeftPanel");
        //RightPanel = background.Q("RightPanel");

        itemListController.InitializeItemList(background, NotesItemAsset, UnlockAll);
        if (!OpenOnStart)
            Hide();
        _character.GetInventory().itemGrabed += OnCharacterInventory_ItemGrabbed;
    }
    public void AddItem(NotesItem notesItem)
    {
        itemListController.AddItem(notesItem);
    }
    public void Toggle()
    {
        if (IsOpen())
        {
            Hide(); 
        }
        else
            Show();
    }

    public bool IsOpen()
    {
        return root.style.visibility != Visibility.Hidden;
    }

    public void Hide()
    {
        root.style.visibility = Visibility.Hidden;
        root.style.display = DisplayStyle.None;
    }
    public void Show()
    {
        root.style.visibility = Visibility.Visible;
        root.style.display = DisplayStyle.Flex;
    }
}
