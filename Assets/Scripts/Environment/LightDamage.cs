using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _playerMask;

    private ITakeDamage _playerDamageable;
    private bool _playerInArea;

    private void Update()
    {
        if(_playerInArea == true)
        {
            _playerDamageable?.TakeDamage(_damage, transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_playerMask, 2))
        {
            _playerDamageable = collision.transform.parent.GetComponentInChildren<ITakeDamage>();
            _playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(_playerMask, 2))
            _playerInArea = false;
    }
}
