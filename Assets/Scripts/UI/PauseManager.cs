using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private BoolEvent _gamePaused;
    private PlayerControls _uiInputs;

    public static bool GameIsPaused;

    private void Awake()
    {
        _uiInputs = new();
    }

    private void OnEnable()
    {
        _uiInputs.UI.Enable();
        _uiInputs.UI.Pause.performed += TogglePause;
    }

    private void OnDisable()
    {
        _uiInputs.UI.Pause.performed -= TogglePause;
        _uiInputs.UI.Disable();
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        GameIsPaused = !GameIsPaused;
        _gamePaused.Invoke(GameIsPaused);
    }

    public void SetTimeScale(bool pause)
    {
        GameIsPaused = pause;
        Time.timeScale = pause ? 0.0f : 1.0f;
    }
}
