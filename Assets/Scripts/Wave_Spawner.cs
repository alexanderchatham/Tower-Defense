using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wave_Spawner : MonoBehaviour {

    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public Transform spawnPoint;
    private int waveNumber = 0;
    public Text waveCountdownText;
    public Text waveCounter;
    public bool isOver = true;
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
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
