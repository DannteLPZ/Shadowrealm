using System.Collections;
using UnityEngine;

public class FireflyBehaviour : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    [Header("Parameters")]
    [SerializeField] private float _fireflySpeed;
    [SerializeField] private float _deltaX;
    [SerializeField] private Vector2 _yValues;

    private float _deltaY;
    private float _positionX;
    private float _positionY;
    private float _initPositionX;
    private float _initPositionY;
    private Vector2 _target;

    void OnEnable()
    {
        _deltaY = Random.Range(_yValues.x, _yValues.y);

        _initPositionX = transform.position.x;
        _initPositionY = transform.position.y;

        _positionX = _initPositionX - _deltaX;
        _positionY = _initPositionY + _deltaY;

        _target = new(_positionX, _positionY);

        StartCoroutine(LifeTimeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

        float step = _fireflySpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, _target) > 0.3f) 
        {
            Vector3 targetLerp = Vector3.Slerp(transform.position, new Vector3(_positionX, _positionY, transform.position.z), Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, targetLerp, step); 
        }
        else
        {
            _deltaY = Random.Range(_yValues.x, _yValues.y);

            _positionX = transform.position.x - _deltaX;
            _positionY = _initPositionY + _deltaY;

            _target = new(_positionX, _positionY);
        }
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }
}
