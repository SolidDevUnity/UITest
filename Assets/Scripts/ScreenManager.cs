using UnityEngine;

public abstract class ScreenManager : MonoBehaviour
{
    [SerializeField]
    protected UIManager uiManager;

    public abstract void OpenScreen();

    public abstract void CloseScreen();
}
