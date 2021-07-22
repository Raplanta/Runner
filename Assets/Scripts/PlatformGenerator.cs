using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _platformPrefabs;
    private List<GameObject> _activePlatform = new List<GameObject>();
    private float _spawnPositionPlatform = 0;
    private float _lengthPlatform = 40;

    [SerializeField] private Transform _player;
    private int _startPlatform = 1;

    void Start()
    {
        _spawnPositionPlatform = _platformPrefabs[0].transform.position.x;
    }

    void Update()
    {
        if (_player.position.x > _spawnPositionPlatform - _startPlatform * _lengthPlatform)
        {
            SpawnPlatform(Random.Range(0, _platformPrefabs.Length));
            if (_activePlatform.Count > 2)
            {
                DeletePlatform();
            }
        }
    }

    private void SpawnPlatform(int platformIndex)
    {
        _activePlatform.Add(Instantiate(_platformPrefabs[platformIndex], _spawnPositionPlatform * transform.right, transform.rotation));
        _spawnPositionPlatform += _lengthPlatform;
    }

    private void DeletePlatform()
    {
        Destroy(_activePlatform[0]);
        _activePlatform.RemoveAt(0);
    }
}
