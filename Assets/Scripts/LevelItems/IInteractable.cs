/* an interface that supports items which can be interacted with */
public interface IInteractable
{
    void OnInteract();
    ItemType GetItemType();
}
