using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour {

    public void SpawnObstacle_Normal()
    {
        Instantiate(Resources.Load("Prefabs/Obstacle_normal"), new Vector2(0, 20),Quaternion.Euler(0,0,0));
    }

    public void SpawnObstacle_Killer()
    {
        Instantiate(Resources.Load("Prefabs/Obstacle_killer"), new Vector2(0, 20), Quaternion.Euler(0, 0, 0));
    }
}
