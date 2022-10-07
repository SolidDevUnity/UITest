using UnityEngine;

[CreateAssetMenu(fileName = "New ItemSOHolder", menuName = "Items/Holder")]
public class ItemSOHolder : ScriptableObject
{
    // I probably should start learning addressables so I can stop using these lol
    public ItemSO[] items;
}
