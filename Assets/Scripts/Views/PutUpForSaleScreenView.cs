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
            itemRuntime.itemDataStruct.marketPrice = Int32.Parse(priceInput.text);
            inventoryManager.PutUpItemForSale(itemRuntime.itemDataStruct);
        });
    }
}
