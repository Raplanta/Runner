using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _distance = new Vector3(0, 3, -9);
    private Vector3 _positionToGo;
    private Vector3 _smoothPosition;
    [SerializeField] private float _speed = 5;

    void Update()
    {
        if (_player == null)
            return;
        _positionToGo = _player.transform.position + _distance;
        _smoothPosition = Vector3.Lerp(transform.position, _positionToGo, 0.3f * Time.deltaTime * _speed);
        transform.position = _smoothPosition;

    }
}
