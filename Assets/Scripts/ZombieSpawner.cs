using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    [SerializeField] private GameObject _zombiePool;
    [SerializeField] private Transform[] _spots;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _safeDistance;

    private float _lastSpawnTime;
    private Vector3 _spawnCoords;

    private void Start()
    {
        _zombiePool = GameObject.Find("Zombie Pool");
        _lastSpawnTime = Time.time;
    }

    void Update()
    {
        if(Time.time > _lastSpawnTime + _spawnDelay)
        {
            ChooseSpawnSpot();
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        Instantiate(_zombie, _spawnCoords, Quaternion.identity, _zombiePool.transform);
        _lastSpawnTime = Time.time;
    }

    private void ChooseSpawnSpot()
    {
        GameObject player = GameObject.Find("Player");
        _spawnCoords = _spots[Random.Range(0, _spots.Length)].transform.position;
        if (Vector3.Distance(player.transform.position, _spawnCoords) < _safeDistance)
            ChooseSpawnSpot();
    }
}
