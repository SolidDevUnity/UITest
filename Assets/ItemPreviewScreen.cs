using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

    [Space]
    [SerializeField]
    private Button putUpForSaleButton;
    [SerializeField]
    private Button deleteButton;

    public void Initialize(ItemRuntime itemRuntime, InventoryManager inventoryManager)
    {
        itemName.text = itemRuntime.itemName;
        itemDescription.text = itemRuntime.itemDescription;
        itemImage.sprite = itemRuntime.itemImage;

        putUpForSaleButton.onClick.AddListener(inventoryManager.PutUpForSaleItem);
        deleteButton.onClick.AddListener(inventoryManager.DeleteItem);
    }

    private void OnDisable()
    {
        putUpForSaleButton.onClick.RemoveAllListeners();
        deleteButton.onClick.RemoveAllListeners();
    }
}