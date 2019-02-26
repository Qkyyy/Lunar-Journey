using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public Transform leftBorder;
    public Transform rightBorder;
    public Transform topBorder;

    float cameraLeftBorder = 0;
    float cameraTopBorder = 0;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        //camera's view left border x position
        cameraLeftBorder = Camera.main.ScreenToWorldPoint(new Vector3()).x;
        cameraTopBorder = Mathf.Abs(Camera.main.ScreenToWorldPoint(new Vector3()).y);

        //sets left and right border to align with what camera sees
        leftBorder.SetPositionAndRotation(new Vector2(cameraLeftBorder+0.1f, leftBorder.position.y), transform.rotation);
        rightBorder.SetPositionAndRotation(new Vector2(Mathf.Abs(cameraLeftBorder)-0.1f, rightBorder.position.y), rightBorder.rotation);
        topBorder.SetPositionAndRotation(new Vector2(topBorder.position.x, cameraTopBorder-0.1f), topBorder.rotation);
    }
}
