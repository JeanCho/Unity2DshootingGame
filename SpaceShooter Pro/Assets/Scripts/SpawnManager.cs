using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerUp;

    [SerializeField]
    private float _spawnTime = 5.0f;


    private bool _stopSpawning = false;

    private IEnumerator coroutine;
   
    public void StartSpawning()
    {
        coroutine = SpawnEnemyRoutine(_spawnTime);
        StartCoroutine(coroutine);
        StartCoroutine(SpawnPowerUpRoutine());
    }

    void Update()
    {
        
    }
    private IEnumerator SpawnEnemyRoutine(float SpawnTime)
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 9f), 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(Random.Range(SpawnTime,6));

        }
    }


    private IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 9f), 0);
            int powerID = Random.Range(0, _powerUp.Length);
            GameObject newPowerUp = Instantiate(_powerUp[powerID], posToSpawn, Quaternion.identity);
            
            yield return new WaitForSeconds(Random.Range(5,8));

        }
    }


    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
