using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite icon;
    public string description;

    [Space]
    public PriceRange priceRange;
}
