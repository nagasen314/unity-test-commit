using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerSingleton : MonoBehaviour {

    [SerializeField] AudioClip startMenuMusic;
    [SerializeField] AudioClip gameOverMusic;
    [SerializeField] AudioClip level00Music;

    AudioSource AS;

    private static MusicPlayerSingleton _instance;

    public static MusicPlayerSingleton instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicPlayerSingleton>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        LoadSceneMusic(0);
    }

    private void SetUpSingleton()
    {
        if(FindObjectsOfType(GetType()).Length > 1) // GetType() returns current type in this context
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadSceneMusic(int sceneIndex)
    {
        AS = instance.GetComponent<AudioSource>();

        AS.Stop();
        if (sceneIndex == 0)
        {
            AS.clip = startMenuMusic;
        }
        if (sceneIndex == 1)
        {
            AS.clip = gameOverMusic;
        }
        if (sceneIndex == 2)
        {
            AS.clip = level00Music;
        }
        AS.loop = true;
        AS.Play();
    }
}
