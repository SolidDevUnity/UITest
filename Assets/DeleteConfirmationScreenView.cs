using UnityEngine;
using UnityEngine.UI;

public class DeleteConfirmationScreenView : ScreenView
{
    [SerializeField]
    private Button yesDeleteButton;

    public void Initialize(ItemRuntime item, InventoryManager inventoryManager)
    {
        var cachedInventoryManager = inventoryManager;

        yesDeleteButton.onClick.RemoveAllListeners();
        yesDeleteButton.onClick.AddListener(() => cachedInventoryManager.DeleteItemFromInventory(item.itemDataStruct));
    }
}
