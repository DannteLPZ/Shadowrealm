using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float _effectMultiplier;

    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;
    private float _textureUnitSizeX;


    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        float deltaMovement = _lastCameraPosition.x - _cameraTransform.position.x;
        transform.position += deltaMovement * _effectMultiplier * Vector3.right;
        _lastCameraPosition = _cameraTransform.position;

        if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            float offsetPosition = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new(_cameraTransform.position.x + offsetPosition, transform.position.y);
        }
    }
}
