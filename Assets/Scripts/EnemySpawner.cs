using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

	// Use this for initialization
	IEnumerator Start() { // can make your Start() method into a coroutine simply by changing return type, looks like
        do // does once anyway
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
        	
	}

    private IEnumerator SpawnAllWaves()
    {
        for(int i=startingWave;i<waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        // Generate GetNumberOfEnemies() number of enemies.
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            // generate a prefab object, pass in 
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), // grab enemy prefab
                waveConfig.GetWaypoints()[0].transform.position, // spawn at initial position
                Quaternion.identity); // don't rotate
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
