using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI itemDisplayName;
    [SerializeField]
    protected Image itemImage;

    public ItemRuntime itemRuntime { get; private set; }

    public virtual void Initialize(ItemRuntime itemRuntime)
    {
        this.itemRuntime = itemRuntime;
        itemDisplayName.text = itemRuntime.itemName;
        itemImage.sprite = itemRuntime.itemImage;
    }
}
