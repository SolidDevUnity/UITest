using System;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    protected DataManager dataManager;
    
    protected void ToggleScreen(bool isActive, ScreenView screenView, Action onActiveAction = null, Action onInActiveAction = null)
    {
        screenView.ToggleScreen(isActive);

        if (isActive)
        {
            onActiveAction?.Invoke();
        }
        else
        {
            onInActiveAction?.Invoke();
        }
    }
}
