using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI itemDisplayName;
    [SerializeField]
    protected Image itemImage;

    public virtual void Initialize(ItemRuntime itemRuntime)
    {
        itemDisplayName.text = itemRuntime.itemName;
        itemImage.sprite = itemRuntime.itemImage;
    }
}
