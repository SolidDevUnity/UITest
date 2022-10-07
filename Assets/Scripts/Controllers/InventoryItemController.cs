using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : ItemController
{
    [SerializeField]
    protected Button showButton;

    public ItemRuntime itemRuntime { get; private set; }

    public void Initialize(ItemRuntime itemRuntime, Action OnButtonClickAction)
    {
        this.itemRuntime = itemRuntime;

        base.Initialize(itemRuntime);
        showButton.onClick.AddListener(() => OnButtonClickAction?.Invoke());
    }

    protected void OnDisable()
    {
        showButton.onClick.RemoveAllListeners();
    }
}
