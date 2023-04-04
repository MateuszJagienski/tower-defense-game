using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class WaveManager : MonoBehaviour
    {
        public static Action OnNewEnemySpawn;

        private int maxNumberOfWaves;
        private int currentWave;
        public bool IsRunning { get; private set; }
        public List<WaveData> Waves;
        public Text StartText;

        public event Action GameWon;


        private static WaveManager _instance;
        public static WaveManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<WaveManager>();
                }
                return _instance;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            maxNumberOfWaves = Waves.Count;
            currentWave = 0;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Space) && !IsRunning && currentWave < maxNumberOfWaves)
            {
                SpawnEnemies();
                IsRunning = true;
                StartText.enabled = false;
            }
            if (currentWave >= maxNumberOfWaves && !IsRunning)
            {
                GameWon?.Invoke();
            }
        }

        private List<PartWave> GetWaveList(int waveIndex)
        {
            return Waves[waveIndex].PartWave;
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
                for (var i = 0; i < part.Quantity; i++)
                {
                    EnemySpawner.Instance.SpawnEnemy(part.EnemyModelType);
                    yield return new WaitForSeconds(part.TimeBetweenSpawn);
                }

            }
            while (FindObjectOfType<Enemy>() != null)
            {
                yield return new WaitForSeconds(2);
            }
            IsRunning = false;
            currentWave++;
        }

        private void SpawnRandomEnemies(int count)
        {

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

        [ContextMenu("Fill Waves")]
        void FillWaves()
        {
            Waves = Resources.LoadAll("waves", typeof(WaveData))
                .Cast<WaveData>()
                .ToList();
        }
    }
}
