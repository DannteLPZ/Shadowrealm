using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorePopUp : MonoBehaviour
{
    [SerializeField] private CanvasGroup _lorePopUpCanvas;
    [SerializeField] private LayerMask _playerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_playerMask, 2))
        {
            
            _lorePopUpCanvas.alpha = 1.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_playerMask, 2))
        {
            _lorePopUpCanvas.alpha = 0.0f;
        }
    }
}
