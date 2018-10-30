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
    }
    private void Start()
    {
        InvokeRepeating("SpawnObstacle_Normal", 3f, 3f);
        InvokeRepeating("SpawnObstacle_Killer", 10f, 10f);

    }

    private void Update()
    {
        StartCoroutine(HandleGameOver());
    }


    IEnumerator HandleGameOver()
    {
        yield return new WaitForEndOfFrame();
        if (playerTransform.GetComponent<SpriteRenderer>().isVisible == false)
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
