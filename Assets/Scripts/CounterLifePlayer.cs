using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterLifePlayer : MonoBehaviour {
    
    // config params
    [SerializeField] GameObject playerHealthIconPrefab;
    [SerializeField] int healthPerCounter = 100;

    private PlayerLogic PL;

	// Use this for initialization
	void Start () {
        PL = FindObjectOfType<PlayerLogic>();
    }

    public void ProcessDamage()
    {

    }

    private IEnumerator DrawAllCounters()
    {
        int health = PL.GetHealth();
        for (int i = 0; i < health/healthPerCounter; i++)
        {
            yield return StartCoroutine(DrawCounter(i));
        }
    }

    private IEnumerator DrawCounter(int i)
    {
               // generate a prefab object, pass in 
            /*
             * var newEnemy = Instantiate(PL.GetEnemyPrefab(), // grab enemy prefab
                waveConfig.GetWaypoints()[0].transform.position, // spawn at initial position
                Quaternion.identity); // don't rotate
                */
            yield return new WaitForSeconds(0);

    }
}
