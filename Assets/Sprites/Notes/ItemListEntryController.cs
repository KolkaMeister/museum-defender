using UnityEngine.UIElements;

public class ItemListEntryController
{
    VisualElement Icon;
    VisualElement root;
    public NotesItem NotesItem;
    public void SetVisualElement(VisualElement visualElement)
    {
        root= visualElement;
        Icon = visualElement.Q("IconBack").Q<VisualElement>("Icon");
    }

    public void SetItemData(NotesItem notesItem)
    {
        Icon.style.backgroundImage = new StyleBackground(notesItem.Icon);
        NotesItem = notesItem;
    }
    public void Hide()
    {
        root.style.display = DisplayStyle.None;
        root.style.visibility = Visibility.Hidden;
    }
    public void Show()
    {
        root.style.display = DisplayStyle.Flex;
        root.style.visibility = Visibility.Visible;
    }
}