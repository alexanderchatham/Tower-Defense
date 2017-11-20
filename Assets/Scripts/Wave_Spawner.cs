using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wave_Spawner : MonoBehaviour {

    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public Transform spawnPoint;
    public int waveNumber = 0;
    public Text waveCountdownText;
    public Text waveCounter;
    public bool isOver = true;
    private Transform enemy;
    public Wave_Spawner instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameMap in scene!");
            return;
        }

        instance = this;
    }
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            isOver = false;
        }

        if (isOver)
        {
            countdown -= Time.deltaTime;

            waveCountdownText.text = Mathf.Round(countdown).ToString() + "    ";
        }
    }



    IEnumerator SpawnWave ()
    {
        
        Debug.Log("Wave Incoming!");
        waveNumber++;
        PlayerStats.Rounds++;
        waveCounter.text = waveNumber.ToString();
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            
            yield return new WaitForSeconds(0.5f);
        }
        isOver = true;
        
    }

    void SpawnEnemy()
    {
       enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy script = enemy.GetComponent<Enemy>();
        script.health = 100 + waveNumber * 10;
    }

}
