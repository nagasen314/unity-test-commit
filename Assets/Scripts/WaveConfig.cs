using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create an asset menu
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    // config params
	[SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float randomMagnitude = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    // return individual wavepoints in this path
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform) // multiple transform values??
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetRandomMagnitude() { return randomMagnitude; }

    public float GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }
}
