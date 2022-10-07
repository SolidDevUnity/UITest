using UnityEngine;
using UnityEngine.UI;

public class DeleteConfirmationScreenView : ScreenView
{
    [SerializeField]
    private Button yesDeleteButton;

    public void Initialize(ItemRuntime item, InventoryManager inventoryManager)
    {
        yesDeleteButton.onClick.RemoveAllListeners();
        yesDeleteButton.onClick.AddListener(() => inventoryManager.DeleteItemFromInventory(item.itemDataStruct));
    }
}
