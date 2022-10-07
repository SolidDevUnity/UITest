using UnityEngine;

public class ItemRuntime
{
    public ItemSO itemTemplate;

    public Sprite itemImage;
    public string itemDescription;
    public string itemName;
    public int marketPrice;
    public ItemDataStruct itemDataStruct;

    public ItemRuntime(ItemSO itemTemplate, ItemDataStruct itemDataStruct)
    {
        this.itemTemplate = itemTemplate;

        itemImage = itemTemplate.icon;
        itemDescription = itemTemplate.description;

        itemName = itemDataStruct.itemName;
        marketPrice = itemDataStruct.marketPrice;
        this.itemDataStruct = itemDataStruct;
    }

    public ItemRuntime(ItemSO itemTemplate)
    {
        this.itemTemplate = itemTemplate;

        itemImage = itemTemplate.icon;
        itemDescription = itemTemplate.description;
        itemName = itemTemplate.name;
    }
}
