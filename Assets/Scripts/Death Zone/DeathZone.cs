using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private BoolEvent _gameOverEvent;
    [SerializeField] private StringEvent _fallSFXEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randomNumber = Random.Range(1, 100);

        if (randomNumber == 50) { _fallSFXEvent.Invoke("SFX_Fall"); }

        else { _fallSFXEvent.Invoke("SFX_GameOver"); }

        _gameOverEvent.Invoke(false);

        GameObject.Find("Player").GetComponentInChildren<ITakeDamage>().TakeDamage(1000, transform.position);
    }
        
}
