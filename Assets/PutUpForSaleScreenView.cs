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
        var cachedInventoryManager = inventoryManager;
        var cachedItemRuntime = itemRuntime;

        priceInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
        putUpForSaleButton.onClick.RemoveAllListeners();
        putUpForSaleButton.onClick.AddListener(() =>
        {
            var tempItemDataStruct = cachedItemRuntime.itemDataStruct;
            tempItemDataStruct.marketPrice = Int32.Parse(priceInput.text);
            
            cachedInventoryManager.PutUpItemForSale(tempItemDataStruct);
        });
    }
}
