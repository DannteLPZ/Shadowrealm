using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OnTriggerPlay : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private LayerMask _playerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Mathf.Log(_playerMask, 2))
        {
            _director.Play();
            _collider.enabled = false;
        }
    }
}
