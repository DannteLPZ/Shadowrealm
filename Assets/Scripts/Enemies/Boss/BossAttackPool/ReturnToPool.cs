using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    public void ReturnToPoolEvent()
    {
        transform.parent.gameObject.SetActive(false);
        //Debug.Log("Si me desactive rey?");
    }
}
