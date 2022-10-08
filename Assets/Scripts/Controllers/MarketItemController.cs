using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketItemController : ItemController
{
    [SerializeField]
    protected TextMeshProUGUI marketPriceText;
    [SerializeField]
    protected Button buyButton;
    [SerializeField]
    protected Button aboutButton;
    
    private int price;

    public void Initialize(ItemRuntime itemRuntime, DataManager dataManager, Action OnBuyButtonClickAction, Action OnAboutButtonClickAction)
    {
        base.Initialize(itemRuntime);
        
        price = itemRuntime.marketPrice;
        marketPriceText.text = "Price: " + price.ToString();

        dataManager.OnGoldAmountUpdate += OnGoldAmountUpdateListener;

        buyButton.onClick.AddListener(() => OnBuyButtonClickAction?.Invoke());
        aboutButton.onClick.AddListener(() => OnAboutButtonClickAction?.Invoke());
    }

    private void OnGoldAmountUpdateListener(int goldAmount)
    {
        bool hasEnoughGold = goldAmount >= price;
        buyButton.interactable = hasEnoughGold;
    }

    protected void OnDisable()
    {
        buyButton.onClick.RemoveAllListeners();
        aboutButton.onClick.RemoveAllListeners();
    }
}
