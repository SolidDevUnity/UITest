using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewScreenView : ScreenView
{
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

    public void Initialize(ItemRuntime itemRuntime)
    {
        itemName.text = itemRuntime.itemName;
        itemDescription.text = itemRuntime.itemDescription;
        itemImage.sprite = itemRuntime.itemImage;
    }
}
