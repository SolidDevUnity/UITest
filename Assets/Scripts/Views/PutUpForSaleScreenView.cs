using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PutUpForSaleScreenView : ScreenView
{
    [SerializeField]
    private Button putUpForSaleButton;
    [SerializeField]
    private TMP_InputField priceInput;

    public void Initialize(ItemRuntime itemRuntime, InventoryManager inventoryManager)
    {
        priceInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
        putUpForSaleButton.onClick.RemoveAllListeners();
        putUpForSaleButton.onClick.AddListener(() =>
        {
            if (!int.TryParse(priceInput.text, out var parsedPrice) || parsedPrice < 0)
            {
                Debug.LogWarning($"Invalid sale price entered: '{priceInput.text}'. Please enter a non-negative whole number.");
                return;
            }

            itemRuntime.itemDataStruct.marketPrice = parsedPrice;
            inventoryManager.PutUpItemForSale(itemRuntime.itemDataStruct);
        });
    }
}
