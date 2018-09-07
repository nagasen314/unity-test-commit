using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    [Header("EnemyBehavior")]
    [SerializeField] float health=100;
    [SerializeField] float shotCounter=0.8f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] int scoreValue = 100;

    [Header("VFX")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 1f;

    [Header("Audio")]
    [SerializeField] AudioClip destroySFX;
    [SerializeField] AudioClip shootSFX;
    [Range(0f, 1f)] [SerializeField] float deathSFXVolume = 0.7f;
    [Range(0f, 1f)] [SerializeField] float shootSFXVolume = 0.7f;

    // cache
    GameSession GS;

    // Use this for initialization
    void Start () {
        GS = FindObjectOfType<GameSession>();
        GenerateShotInterval();
    }
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime; // amount of time last frame took
        if(shotCounter <= 0f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity); // may need to offset this. FYI, Quaternion.identity = current ** ROTATION **
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f*projectileSpeed); // 0 x speed for laser
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position,shootSFXVolume);
        GenerateShotInterval(); // regenerate
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamagerDealer damageDealer = other.gameObject.GetComponent<DamagerDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamagerDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void GenerateShotInterval()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Die()
    {
        GS.addToScore(scoreValue);
        Destroy(gameObject);

        AudioSource.PlayClipAtPoint(destroySFX, Camera.main.transform.position,deathSFXVolume);
        GameObject explosionObject = Instantiate(explosionVFX, transform.position, transform.rotation); // show at breaking block's location
        //^ is basically duplicating a prefab (since we are pointing a prefab to this field)
        Destroy(explosionObject, explosionDuration);
    }
}
