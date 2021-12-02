using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour {
    public SuperTextSuperSeks waveText;
    public SuperTextSuperSeks enemiesText;

    public bool tutorial = false;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [SerializeField] UnityEvent endOfWave;

    [System.Serializable]
    public class Wave
    {
        public string name;
        //public Transform enemy;
        //public GameObject[] enemy;
        public int count;
        public float rate;
    }

    public GameObject[] enemy;


    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;
    public int enemyCount;

    public bool allWavesCompleted;

    void Start() {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;

        //clones = GameObject.FindGameObjectWithTag("SBClone").transform;
    }

    public void DecrementEnemyCount() {
        enemyCount--;
        enemiesText.SetText("Enemies Left: " + enemyCount);
    }

    void Update()
    {
        waveText.SetText("Wave: " + (1+nextWave) + "/" + waves.Length);
        
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                endOfWave.Invoke();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            enemiesText.SetText("Next wave in: " + Mathf.RoundToInt(waveCountdown) + "s");
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1 && tutorial == true)
        {
            // nextWave = 0;
            // Debug.Log("ALL WAVES COMPLETE! Looping...");

            // disable this object
            allWavesCompleted = true;
            gameObject.SetActive(false);
        } else if (nextWave + 1 > waves.Length - 1 && tutorial == false)
        {
            // nextWave = 0;
            // Debug.Log("ALL WAVES COMPLETE! Looping...");

            // Next Scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("ALL WAVES COMPLETE! Loading Next Scene in Build Index...");
        }
        else
        {
            allWavesCompleted = false;
            nextWave++;
        }
    }

    public bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            //SpawnEnemy(_wave.enemy);
            SpawnEnemy();
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    // void SpawnEnemy(Transform _enemy)
    // {
    //     Debug.Log("Spawning Enemy: " + _enemy.name);

    //     Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
    //     Instantiate(_enemy, _sp.position, _sp.rotation);
    // }

    public void SpawnEnemy() {
        StartCoroutine(AttemptSpawning());
    }

    private IEnumerator AttemptSpawning() {
        int randomEnemy = Random.Range(0, enemy.Length); //Selecing a random enemy
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        while (Vector2.Distance(_sp.position, playerPos) < 20) {
            _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            yield return null;
        }
        Instantiate(enemy[randomEnemy], _sp.position, _sp.rotation);
        IncrementEnemyCount();
    }

    public void IncrementEnemyCount() {
        enemyCount++;
        enemiesText.SetText("Enemies Right: " + enemyCount);
        
    }
}
