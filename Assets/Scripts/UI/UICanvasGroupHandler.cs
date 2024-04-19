using UnityEngine;

public class UICanvasGroupHandler : MonoBehaviour
{
    public void ShowCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void SetThisCanvasGroup(bool show)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup != null )
        {
            canvasGroup.alpha = show ? 1.0f : 0.0f;
            canvasGroup.interactable = show;
            canvasGroup.blocksRaycasts = show;
        }
    }

    public void ShowThisIfTrue(bool show)
    {
        if(show == true)
            SetThisCanvasGroup(true);
    }

    public void ShowThisIfFalse(bool show)
    {
        if (show == false)
            SetThisCanvasGroup(true);
    }

    public void HideThisIfTrue(bool show)
    {
        if (show == true)
            SetThisCanvasGroup(false);
    }

    public void HideThisIfFalse(bool show)
    {
        if (show == false)
            SetThisCanvasGroup(false);
    }
}
