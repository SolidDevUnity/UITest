using System;
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

    public Action OnButtonClickAction { get; private set; }

    public virtual void Initialize(ItemRuntime itemRuntime, Action OnButtonClickAction)
    {
        this.itemRuntime = itemRuntime;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        this.OnButtonClickAction = OnButtonClickAction;

        itemDisplayName.text = itemRuntime.itemName;
        itemImage.sprite = itemRuntime.itemImage;
    }

    protected virtual void OnButtonClicked()
    {
        OnButtonClickAction?.Invoke();
    }

    protected virtual void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
