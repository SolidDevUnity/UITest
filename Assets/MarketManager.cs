using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;

    [Space]
    [SerializeField]
    private MarketScreenView marketScreenView;

    public void ToggleMarketScreen(bool isActive)
    {
        marketScreenView.ToggleScreen(isActive);

        if (isActive)
        {
            var items = dataManager.GetMarketItems();
            marketScreenView.Initialize(items);
        }
    }
}
