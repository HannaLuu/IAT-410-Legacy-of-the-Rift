using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{

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
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;

        //clones = GameObject.FindGameObjectWithTag("SBClone").transform;
    }

    void Update()
    {
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
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            // nextWave = 0;
            // Debug.Log("ALL WAVES COMPLETE! Looping...");

            // Next Scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("ALL WAVES COMPLETE! Loading Next Scene in Build Index...");
        }
        else
        {
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

    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemy.Length); //Selecing a random enemy
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy[randomEnemy], _sp.position, _sp.rotation);
    }

}
