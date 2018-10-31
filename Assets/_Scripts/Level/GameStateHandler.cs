using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour {


    Transform playerTransform;
    ObstaclesGenerator obGen;


    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        obGen = new ObstaclesGenerator();
        //Time.timeScale = 0;
    }
    private void Start()
    {
        InvokeRepeating("SpawnObstacle_Normal", 5f, 5f);
        InvokeRepeating("SpawnObstacle_Killer", 15f, 15f);

    }

    private void Update()
    {
        StartCoroutine(HandleGameOver());
    }


    IEnumerator HandleGameOver()
    {
        yield return new WaitForEndOfFrame();
        if (playerTransform.GetComponent<SpriteRenderer>().isVisible == false || playerTransform.GetComponent<SpriteRenderer>().material.color.a <= 0)
        {
            Debug.Log("game over");
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
}
