using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject powerUpPrefab;

    [SerializeField] private float spawnRange;

    private List<Enemy> _enemies = new List<Enemy>();

    public List<Enemy> Enemies => _enemies;

    public int EnemyCountInWave { get; set; } = 1;
    

    private void Start()
    {
       SpawnEnemyWave(EnemyCountInWave);
    }

    private void Update()
    {
        if (_enemies.Count == 0)
        {
            EnemyCountInWave += 1;
            SpawnEnemyWave(EnemyCountInWave);
        }
    }

    private void SpawnEnemyWave(int count)
    {
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        
        for (int i = 0; i < count; i++)
        {
            var enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            enemy.SpawnManager = this;
            _enemies.Add(enemy);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        var spawnPosX = Random.Range(-spawnRange, spawnRange);
        var spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}