using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateHandler : MonoBehaviour {

    public GameObject pauseInterface;

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
        InvokeRepeating("SpawnUfo", 60f, 60f);
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
            CheckIfHighscore();
            Time.timeScale = 0;
        }
    }

    void SpawnObstacle_Normal()
    {
        obGen.SpawnObstacle_Normal();
    }

    void SpawnObstacle_Killer()
    {
        obGen.SpawnObstacle_Killer();
    }

    void SpawnUfo()
    {
        obGen.SpawnUfo();
    }

    void CalculateScore()
    {
        if (calculateScore == true)
        {
            score += Time.deltaTime;
            scoreText.text = Mathf.Round(score).ToString();
        }
    }

    void CheckIfHighscore()
    {
        if (score > PlayerPrefs.GetInt("highscore"))
            PlayerPrefs.SetInt("highscore", (int)score);

    }

}
