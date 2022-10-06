using UnityEngine;
using UnityEngine.UI;

public class PutUpForSaleScreenView : MonoBehaviour
{
    [SerializeField]
    private Button sellButton;
    [SerializeField]
    private Button deleteButton;

    public void Initialize(ItemRuntime itemRuntime, InventoryManager inventoryManager)
    {
        sellButton.onClick.AddListener(inventoryManager.PutUpItemForSale);
        deleteButton.onClick.AddListener(inventoryManager.DeleteItem);
    }
}
