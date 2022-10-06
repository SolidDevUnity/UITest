using UnityEngine;

public class ItemRuntime
{
    public ItemSO itemTemplate;

    public Sprite itemImage;
    public string itemDescription;
    public string itemName;
    public int marketPrice;

    public ItemRuntime(ItemSO itemTemplate)
    {
        this.itemTemplate = itemTemplate;

        itemImage = itemTemplate.icon;
        itemDescription = itemTemplate.description;
        itemName = itemTemplate.displayName;
        marketPrice = itemTemplate.initialMarketPrice;
    }
}
