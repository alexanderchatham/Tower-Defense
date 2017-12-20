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
    public bool startbool = false;
    private Transform enemy;
    public static Wave_Spawner instance;
    public GameMap gamemap;
    public int enemyCount = 0;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameMap in scene!");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        gamemap = GameMap.instance; 
    }

    void Update()
    {
        if (countdown <= 0f && startbool)
        {

            gamemap.findBestRoute();
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            isOver = false;
            startbool = false;
        }

        if (isOver)
        {
            if(countdown > 0)
            countdown -= Time.deltaTime;

            waveCountdownText.text = Mathf.Round(countdown).ToString() + "    ";
        }
    }

    public void startRound()
    {
        if(isOver)
        startbool = true;
    }

    IEnumerator SpawnWave ()
    {
        
        Debug.Log("Wave Incoming!");
        waveNumber++;
        PlayerStats.Rounds++;
        waveCounter.text = waveNumber.ToString();
        enemyCount = 0;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            enemyCount++;
            yield return new WaitForSeconds(0.5f);
        }
        isOver = true;
        
    }

    void SpawnEnemy()
    {
       enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy script = enemy.GetComponent<Enemy>();
        script.health = 100 + waveNumber * 20;
    }

}
