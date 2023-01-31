using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    private int maxNumberOfWaves;
    private int currentWave;
    public bool IsRunning { get; private set; }
    public List<WaveData> waves;
    public Text startText;

    public event Action GameWon;


    private static WaveManager instance;
    public static WaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WaveManager>();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxNumberOfWaves = waves.Count;
        currentWave = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !IsRunning && currentWave < maxNumberOfWaves)
        {
            SpawnEnemies();
            IsRunning = true;
            startText.enabled = false;
        }
        if (currentWave >= maxNumberOfWaves && !IsRunning)
        {
            GameWon();
        }
    }

    private List<PartWave> GetWaveList(int waveIndex)
    {
        return waves[waveIndex].PartWave;
    }
    void SpawnEnemies()
    {
        StartCoroutine(InstantiateEnemies());
    }
    IEnumerator InstantiateEnemies()
    {
        var wave = GetWaveList(currentWave);
        foreach (var part in wave)
        {
            for (int i = 0; i < part.quantity; i++)
            {
                if (part.enemyID > 0)
                {
                    EnemySpawner.Instance.SpawnEnemy(part.enemyID);
                }
                yield return new WaitForSeconds(part.timeBetweenSpawn);
            }

        }
        while (FindObjectOfType<Enemy>() != null)
        {
            yield return new WaitForSeconds(2);
        }
        IsRunning = false;
        currentWave++;
    }
    public void SkipWave()
    {
        currentWave++;
    }
    private void EndGame()
    {
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }

    public int GetCurrentWave()
    {
        return currentWave + 1;
    }
}
