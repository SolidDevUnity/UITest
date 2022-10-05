using UnityEngine;

public abstract class ScreenManager : MonoBehaviour
{
    [SerializeField]
    protected UIManager uiManager;
    [SerializeField]
    protected DataManager dataManager;

    public abstract void OpenScreen();

    public abstract void CloseScreen();
}
