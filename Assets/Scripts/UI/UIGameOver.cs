using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private CanvasGroup victoryScreen;
    [SerializeField] private CanvasGroup defeatScreen;
    [SerializeField] private UICanvasGroupHandler canvasGroupHandler;

    public void ShowGameOverScreen(bool victory)
    {
        if (victory) 
        {
            canvasGroupHandler.ShowCanvasGroup(victoryScreen);
        }
        else
        {
            canvasGroupHandler.ShowCanvasGroup(defeatScreen);
        }
    }
}
