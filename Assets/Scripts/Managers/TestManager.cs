using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField]
    private ItemSOHolder allItems;
    [SerializeField]
    private DataManager dataManager;

    public void Add20ItemsToMarket()
    {
        for (int i = 0; i < 20; i++)
        {
            var tempItem = allItems.items[Random.Range(0, allItems.items.Length)];
            var tempItemStruct = dataManager.GenerateItemDataStruct(tempItem, i);
            dataManager.AddMarketItem(tempItemStruct);
        }
    }
}
