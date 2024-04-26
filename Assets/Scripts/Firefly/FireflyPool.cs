
using System.Collections.Generic;
using UnityEngine;

public class FireflyPool : MonoBehaviour
{
    [SerializeField] private GameObject _fireflyPrefab;
    [SerializeField] private List<GameObject> _fireflyList;
    [SerializeField] private float _yBoundary;
    [SerializeField] private int _poolSize;

    private Vector2 _yBounds;

    void Start()
    {
        _yBounds = new(transform.position.y - _yBoundary, transform.position.y + _yBoundary);
        AddFirefliesToPool(_poolSize);
    }
    
    void AddFirefliesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject firefly = Instantiate(_fireflyPrefab);
            firefly.SetActive(false);
            firefly.transform.parent = transform;
            _fireflyList.Add(firefly);
        }
    }

    public GameObject RequestFirefly()
    {
        for(int i = 0; i < _fireflyList.Count; i++)
        {
            if (!_fireflyList[i].activeSelf)
            {
                SetFireflyPosition(_fireflyList[i]);
                _fireflyList[i].SetActive(true);
                return _fireflyList[i];
            }
        }

        AddFirefliesToPool(1);
        _fireflyList[_fireflyList.Count - 1].SetActive(true);
        return _fireflyList[_fireflyList.Count - 1];
    }

    private void SetFireflyPosition(GameObject firefly)
    {

        float randomPositionY = Random.Range(_yBounds.x, _yBounds.y);

        firefly.transform.position = new Vector2(transform.position.x, randomPositionY);

    }

}
