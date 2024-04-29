using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePillarPool : MonoBehaviour
{
    [SerializeField] private GameObject _flamePillarPrefab;
    [SerializeField] private List<GameObject> _flamePillarList;
    [SerializeField] private int _poolSize;


    private void Start()
    {
        AddFlamePillarsToPool(_poolSize); 
    }

    private void AddFlamePillarsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject firePillar = Instantiate(_flamePillarPrefab);
            firePillar.SetActive(false);
            firePillar.transform.parent = transform;
            _flamePillarList.Add(firePillar);
        }
    }

    public GameObject RequestFlamePillar()
    {
        for (int i = 0; i < _flamePillarList.Count; i++)
        {
            if (!_flamePillarList[i].activeSelf)
            {
                _flamePillarList[i].SetActive(true);
                return _flamePillarList[i];
            }
        }

        AddFlamePillarsToPool(1);
        _flamePillarList[_flamePillarList.Count - 1].SetActive(true);
        return _flamePillarList[_flamePillarList.Count - 1];
    }

}
