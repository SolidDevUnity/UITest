using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;

    [Space]
    [SerializeField]
    private MarketScreenView marketScreenView;
    [SerializeField]
    private ItemPreviewScreenView itemPreviewScreenView;

    public void ToggleMarketScreen(bool isActive)
    {
        marketScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            var items = dataManager.GetMarketItems();
            marketScreenView.Initialize(items, this);
        }
    }

    public void ToggleItemPreviewScreen(bool isActive)
    {
        itemPreviewScreenView.ToggleScreen(isActive);
    }

    public void ToggleItemPreviewScreen(bool isActive, ItemRuntime itemRuntime)
    {
        itemPreviewScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            itemPreviewScreenView.Initialize(itemRuntime);
        }
    }
}
