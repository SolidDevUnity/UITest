using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField]
    private Canvas InventoryScreen;
    [SerializeField]
    private Canvas ItemPreviewScreen;

    [Space]
    [Header("Market")]
    [SerializeField]
    private Canvas MarketScreen;

    #region Inventory
    public void ToggleInventoryScreen(bool isActive)
    {
        ToggleScreen(InventoryScreen, isActive);
    }

    public void ToggleItemPreviewScreen(bool isActive)
    {
        ToggleScreen(ItemPreviewScreen, isActive);
    }
    #endregion

    #region Market
    public void ToggleMarketScreen(bool isActive)
    {
        ToggleScreen(MarketScreen, isActive);
    }
    #endregion

    private void ToggleScreen(Canvas canvas, bool isActive)
    {
        canvas.enabled = isActive;

        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = isActive;
        canvasGroup.interactable = isActive;
    }
}
