using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    // cache
    //SceneLoader sL;

    // State
    // level?

    // Use this for initialization
    /*void Start()
    {
        //noBlocks = 0;
        //sL = FindObjectOfType<SceneLoader>();
    }*/

    [SerializeField] float loadDelay = 1f;
    [SerializeField] MusicPlayerSingleton musicPlayerSingleton;

    GameSession GS;

    private void Start()
    {
        GS = FindObjectOfType<GameSession>();
        Screen.SetResolution(540, 960, false);
    }

    public void LoadStartMenu()
    {
        musicPlayerSingleton.LoadSceneMusic(0);
        SceneManager.LoadScene(0);
        GS.ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndGameOver());
    }

    private IEnumerator WaitAndGameOver()
    {
        yield return new WaitForSeconds(loadDelay);
        musicPlayerSingleton.LoadSceneMusic(1);
        SceneManager.LoadScene(1);
        GS.FinishGame();
        
    }

    public void LoadGameScene()
    {
        musicPlayerSingleton.LoadSceneMusic(2);
        SceneManager.LoadScene(2);
        GS.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

