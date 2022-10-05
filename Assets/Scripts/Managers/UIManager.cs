using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField]
    private Canvas InventoryScreen;

    [Space]
    [Header("Market")]
    [SerializeField]
    private Canvas MarketScreen;

    public void ToggleInventoryScreen(bool isActive)
    {
        ToggleScreen(InventoryScreen, isActive);
    }

    public void ToggleMarketScreen(bool isActive)
    {
        ToggleScreen(MarketScreen, isActive);
    }

    private void ToggleScreen(Canvas canvas, bool isActive)
    {
        canvas.enabled = isActive;

        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = isActive;
        canvasGroup.interactable = isActive;
    }
}
