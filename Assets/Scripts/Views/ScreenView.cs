using UnityEngine;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
public class ScreenView : MonoBehaviour
{
    protected Canvas canvas;
    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void ToggleScreen(bool isActive)
    {
        canvas.enabled = isActive;

        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = isActive;
        canvasGroup.interactable = isActive;
    }
}
