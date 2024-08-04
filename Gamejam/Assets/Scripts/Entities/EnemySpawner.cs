using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnedGroup
    {
        public Base_Enemy typeOfEnemy;
        public float timeUntilSpawn;
    }

    [System.Serializable]
    public class SpawnedWave
    {
        public SpawnedGroup[] groups;
        public float waveTimeLength;
    }

    public SpawnedWave[] waves;
    public Transform spawnPoint;
    public int spawnerIndex;

    public float timeBetweenWaves = 3f;
    private float countdown = 2f;

    private int wavesIndex = 0;
    private int groupIndex = 0;

    private bool spawning;
    void Start()
    {
    }

    void Update()
    {
        if(countdown <= 0)
        {
            Debug.Log("Wave " + wavesIndex + 1);
            StartCoroutine(SpawnWaves());
            countdown = float.PositiveInfinity;
        }
        countdown -= Time.deltaTime;
    }

    private IEnumerator SpawnWaves()
    {
        if(wavesIndex < waves.Length)
        {
            spawning = true;

            for (int i = 0; i < waves[wavesIndex].groups.Length; i++)
            {
                StartCoroutine(SpawnEnemy(waves[wavesIndex].groups[groupIndex]));
                groupIndex++;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(waves[wavesIndex].waveTimeLength);

            wavesIndex++;
            groupIndex = 0;
            spawning = false;
            countdown = timeBetweenWaves;
        }
        else
        {
            EndLevel();
        }
    }

    private IEnumerator SpawnEnemy(SpawnedGroup group)
    {
        while(spawning)
        {
            Base_Enemy enemy = Instantiate(group.typeOfEnemy, spawnPoint.position, spawnPoint.rotation);
            enemy.SetSpawnerIndex(spawnerIndex);
            enemy.gameObject.transform.localScale = Vector3.one / 2f;
            yield return new WaitForSeconds(group.timeUntilSpawn);
        }
    }

    private void EndLevel()
    {
        Debug.Log("End");
    }
}
