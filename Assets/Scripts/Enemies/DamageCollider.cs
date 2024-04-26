using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private LayerMask _layerToDamage;
    [SerializeField] private int _attackDamage;

    private Collider2D _attackCollider;
    private ContactFilter2D _contactFilter;
    private void Start()
    {
        _attackCollider = GetComponent<Collider2D>();
        _contactFilter = new()
        {
            useLayerMask = true,
            layerMask = _layerToDamage
        };
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        List<Collider2D> results = new();
        Physics2D.OverlapCollider(_attackCollider, _contactFilter, results);
        if (results.Count > 0)
        {
            foreach (Collider2D collider in results)
            {
                GameObject objectHit = collider.transform.parent.gameObject;
                ITakeDamage health = objectHit.GetComponentInChildren<ITakeDamage>();
                health?.TakeDamage(_attackDamage, transform.position);
            }
        }
    }
}
