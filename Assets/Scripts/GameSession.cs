using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {

    // Specify range inside Unity Inspector --> will give you a slider
    [Range(0.1f,10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI finalScoreText;

    // state
    [SerializeField] int score = 0;
    [SerializeField] int scoreMaxDigits = 10;

    private static GameSession _instance;

    public static GameSession instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameSession>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    // on awake: for SINGLETON
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1) // we have already made our own singleton
        {
            Destroy(gameObject); // don't make a new one
        }
        else
        {
            DontDestroyOnLoad(gameObject); // produce our single GameState
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Time.timeScale = gameSpeed;
	}

    private void UpdateScoreText()
    {
        /*StringBuilder SB = new StringBuilder("");
        for(int i=0;i<scoreMaxDigits-score.ToString().Length;i++)
        {
            SB.Append("0");
        }
        SB.Append(score.ToString());
        scoreText.text = SB.ToString();*/
        instance.scoreText.text = instance.score.ToString();
    }

    public void addToScore(int enemyScoreValue)
    {
        instance.score += enemyScoreValue;
        UpdateScoreText();
    }

    public void StartGame()
    {
        ResetGame();
        instance.score = 0;
        UpdateScoreText();
    }

    public void FinishGame()
    {
        instance.finalScoreText.text = instance.score.ToString();
        instance.scoreText.text = "";
    }

    public void ResetGame()
    {
        instance.finalScoreText.text = "";
        instance.scoreText.text = "";
    }
}
