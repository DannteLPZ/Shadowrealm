using System.Collections;
using UnityEngine;

public class FireflySpawnHandler : MonoBehaviour
{
    [SerializeField] private Vector2 _SpawnTime;
    [SerializeField] private FireflyPool _fireflyPool;

    private void Start()
    {
        StartCoroutine(SpawnFirefliesCoroutine());
    }

    private IEnumerator SpawnFirefliesCoroutine()
    {
        while(true)
        {
            float spawnTime = Random.Range(_SpawnTime.x, _SpawnTime.y);

            yield return new WaitForSeconds(spawnTime);

            _fireflyPool.RequestFirefly();
        }
    }

}
