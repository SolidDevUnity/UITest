using UnityEngine;

public class BootstrapManager : MonoBehaviour
{
    [SerializeField]
    private DataManager dataManager;

    [Space]
    [SerializeField]
    private StartScreenView startScreen;

    private void Start()
    {
        var saveData = dataManager.GetPlayerGold();
        startScreen.Initialize(saveData, dataManager);
    }
}
