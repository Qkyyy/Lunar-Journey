using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateHandler : MonoBehaviour {

    public GameObject pauseInterface;
    public Image highscoreImage;

    public delegate void SpawnedObstacle();
    public static event SpawnedObstacle NormalObstacleSpawned;
    public static event SpawnedObstacle UfoSpawned;

    Transform playerTransform;
    ObstaclesGenerator obGen;
    Text scoreText;

    bool calculateScore = true;

    float score = 0f;


    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        obGen = new ObstaclesGenerator();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        Time.timeScale = 0;
    }
    private void Start()
    {
        InvokeRepeating("SpawnObstacle_Normal", 2.5f, 2.5f);
        InvokeRepeating("SpawnObstacle_Killer", 10f, 10f);
        InvokeRepeating("SpawnUfo", 10f, 60f);
    }

    private void Update()
    {
        StartCoroutine(HandleGameOver());
        CalculateScore();
    }


    IEnumerator HandleGameOver()
    {
        yield return new WaitForEndOfFrame();
        if (playerTransform.GetComponent<SpriteRenderer>().isVisible == false || playerTransform.GetComponent<SpriteRenderer>().material.color.a <= 0)
        {
            pauseInterface.SetActive(true);
            calculateScore = false;
            UpdateHighscore();
            Time.timeScale = 0;
        }
    }

    void SpawnObstacle_Normal()
    {
        obGen.SpawnObstacle_Normal();
        if (NormalObstacleSpawned != null)
            NormalObstacleSpawned();
    }

    void SpawnObstacle_Killer()
    {
        obGen.SpawnObstacle_Killer();
    }

    void SpawnUfo()
    {
        obGen.SpawnUfo();
        if (UfoSpawned != null)
            UfoSpawned();
    }

    void CalculateScore()
    {
        if (calculateScore == true)
        {
            score += Time.deltaTime;
            scoreText.text = Mathf.Round(score).ToString();
            if (CheckIfHighScore() == true)
            {
                Debug.Log("showing trophy");
                highscoreImage.gameObject.SetActive(true);
            }
            else
                highscoreImage.gameObject.SetActive(false);
        }
    }

    void UpdateHighscore()
    {
        if (CheckIfHighScore() == true)
        { 
            PlayerPrefs.SetInt("highscore", (int)score);
        }
    }

    bool CheckIfHighScore()
    {
        if (score > PlayerPrefs.GetInt("highscore"))
            return true;
        else
            return false;
    }

}
