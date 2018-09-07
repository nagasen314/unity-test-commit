using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    // config params
    List<Transform> waypoints; // imported from waveConfig
    WaveConfig waveConfig;

    // state
    int indexWaypoint = 0;

	// Use this for initialization
	void Start () {
        waypoints = waveConfig.GetWaypoints();
        // waypoint 0 is the initial position
        transform.position = waypoints[indexWaypoint].transform.position;
        indexWaypoint++;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    // can use this pattern if you set script execution order to be EnemySpawner before EnemyPathing
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        // if haven't reached last waypoint
        if (indexWaypoint < waypoints.Count)
        {
            var targetPosition = waypoints[indexWaypoint].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                // next waypoint reached
                indexWaypoint++;
            }
        }
        else // otherwise, reached last waypoint
        {
            Destroy(gameObject);
        }
    }
}
