using TMPro;
using UnityEngine;

public class StartScreenView : ScreenView
{
    [SerializeField]
    private TextMeshProUGUI playerGoldText;
    
    public void Initialize(int playerGold, DataManager dataManager)
    {
        UpdateGoldText(playerGold);
        dataManager.OnGoldAmountUpdate += UpdateGoldText;
    }

    public void UpdateGoldText(int playerGold)
    {
        playerGoldText.text = "Player Gold: " + playerGold;
    }
}
