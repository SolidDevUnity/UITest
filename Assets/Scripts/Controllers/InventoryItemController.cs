using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : ItemController
{
    [SerializeField]
    protected Button showButton;

    public void Initialize(ItemRuntime itemRuntime, Action OnButtonClickAction)
    {
        base.Initialize(itemRuntime);
        showButton.onClick.AddListener(() => OnButtonClickAction?.Invoke());
    }

    protected void OnDisable()
    {
        showButton.onClick.RemoveAllListeners();
    }
}
