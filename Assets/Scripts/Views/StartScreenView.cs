using TMPro;
using UnityEngine;

public class StartScreenView : ScreenView
{
    [SerializeField]
    private TextMeshProUGUI playerGoldText;

    public void Initialize(int playerGold)
    {
        UpdateGoldText(playerGold);
    }

    public void UpdateGoldText(int playerGold)
    {
        playerGoldText.text = "Player Gold: " + playerGold;
    }
}
