using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI itemDisplayName;
    [SerializeField]
    protected Image itemImage;

    protected ItemRuntime itemRuntime;

    public virtual void Initialize(ItemRuntime itemRuntime)
    {
        this.itemRuntime = itemRuntime;
        
        itemDisplayName.text = itemRuntime.itemName;
        itemImage.sprite = itemRuntime.itemImage;
    }
}
