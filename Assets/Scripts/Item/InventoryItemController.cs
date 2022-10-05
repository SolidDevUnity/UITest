public class InventoryItemController : ItemController
{
    private InventoryManager inventoryManager;

    public void Initialize(ItemRuntime itemRuntime, InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;

        Initialize(itemRuntime);
    }

    protected override void ItemButtonClick()
    {
        base.ItemButtonClick();
        inventoryManager.ShowPreview(itemRuntime);
    }
}
