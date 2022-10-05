public class InventoryManager : ScreenManager
{
    public override void OpenScreen()
    {
        uiManager.ToggleInventoryScreen(true);
    }

    public override void CloseScreen()
    {
        uiManager.ToggleInventoryScreen(false);
    }
}
