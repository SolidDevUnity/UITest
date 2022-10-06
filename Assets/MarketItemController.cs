using System;
using UnityEngine;
using UnityEngine.UI;

public class MarketItemController : ItemController
{
    [SerializeField]
    protected Button buyButton;
    [SerializeField]
    protected Button aboutButton;

    public void Initialize(ItemRuntime itemRuntime, Action OnBuyButtonClickAction, Action OnAboutButtonClickAction)
    {
        base.Initialize(itemRuntime);
        buyButton.onClick.AddListener(() => OnBuyButtonClickAction?.Invoke());
        aboutButton.onClick.AddListener(() => OnAboutButtonClickAction?.Invoke());
    }

    protected void OnDisable()
    {
        buyButton.onClick.RemoveAllListeners();
        aboutButton.onClick.RemoveAllListeners();
    }
}
