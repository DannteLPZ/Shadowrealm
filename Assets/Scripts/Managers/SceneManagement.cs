using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private BoolEvent _gamePaused;
    public static SceneManagement Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(int index)
    {
        _gamePaused.Invoke(false);
        if(index >= 0)
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
