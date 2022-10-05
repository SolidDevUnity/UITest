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
    protected Button button;

    public virtual void Initialize(ItemRuntime itemRuntime)
    {
        this.itemRuntime = itemRuntime;
        button = GetComponent<Button>();
        button.onClick.AddListener(ItemButtonClick);

        itemDisplayName.text = itemRuntime.itemName;
        itemImage.sprite = itemRuntime.itemImage;
    }

    protected virtual void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    protected virtual void ItemButtonClick()
    {

    }
}
