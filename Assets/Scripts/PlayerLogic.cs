using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    // config parameters
    [Header("Player")] // to organize components in the Unity inspector
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 0.07f;
    [SerializeField] float yPadding = 0.03f;
    [SerializeField] int health = 200;
    [SerializeField] float projectileSpeed = 10f;

    [Header("VFX")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Audio")]
    [SerializeField] AudioClip destroySFX;
    [SerializeField] AudioClip shootSFX;
    [Range(0f, 1f)] [SerializeField] float deathSFXVolume = 0.7f;
    [Range(0f, 1f)] [SerializeField] float shootSFXVolume = 0.7f;

    Coroutine fireCoroutine;

    // runtime-calculated variables
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Use this for initialization
    void Start () {
        SetUpMoveBoundaries();
	}

    // Update is called once per frame
    // movement happening on listener
    void Update()
    {
        Move();
        Fire();
    }

    // processing getting hit
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamagerDealer damageDealer = other.gameObject.GetComponent<DamagerDealer>();
        if (!damageDealer) { return; }
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

    private void Die()
    {
        Destroy(gameObject);

        // play death sfx
        AudioSource.PlayClipAtPoint(destroySFX, Camera.main.transform.position,deathSFXVolume);

        // Load scene
        FindObjectOfType<LevelManager>().LoadGameOver();
    }

    private void Move()
    {
        // var because we know for sure the types will match...?
        // Time.deltaTime = time in seconds it took to complete the last frame (READ ONLY)
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; // makes game "frame rate independent" 
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // Debug.Log(deltaX);
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    // shooting
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1")) // can look at InputManager (Edit -> Settings -> Input)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1")) // can look at InputManager (Edit -> Settings -> Input)
        {
            // button let up, stop coroutines
            //StopAllCoroutines();
            StopCoroutine(fireCoroutine); // need to pass the same ** REFERENCE **
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            // Create reference to laser as GameObject
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity); // may need to offset this. 
                                                                                                  // Quaternion.identity = current ** ROTATION **
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed); // 0 x speed for laser

            // Emit shooting sound
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position,shootSFXVolume);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    // clamping movement
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        // ViewPortToWorldPoint() converts the posititon of something as it relates to the camera view, into a world space value
        // 0 -> 1 x 0 -> 1 relative to camera view
        // do it this way so that we don't have to re-adjust the coordinates for the "walls" (if we change camera size...)
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(xPadding, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f - xPadding, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, yPadding, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1f - yPadding, 0)).y;
    }

    public int GetHealth()
    {
        return health;
    }

}
