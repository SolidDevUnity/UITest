using UnityEngine;

public class InventoryManager : ScreenManager
{
    [SerializeField]
    private RectTransform itemSpawnPoint;
    [SerializeField]
    private InventoryItemController inventoryItemPrefab;

    private void Start()
    {
        var items = dataManager.GetInventoryItems();
        foreach (var item in items)
        {
            var spawnedItem = Instantiate(inventoryItemPrefab, itemSpawnPoint);
            spawnedItem.Initialize(item);
        }
    }

    public override void OpenScreen()
    {
        uiManager.ToggleInventoryScreen(true);
    }

    public override void CloseScreen()
    {
        uiManager.ToggleInventoryScreen(false);
    }
}
