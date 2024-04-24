using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementHandler : MonoBehaviour
{
    public void LoadLevel(int buildIndex)
    {
        SceneManagement.Instance.LoadScene(buildIndex);
    }
}
