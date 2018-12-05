using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

    public float speed = 0.5f;

    private void Awake()
    {
        float quadHeight = Camera.main.orthographicSize * 2.2f;
        float quadWidth = quadHeight * Screen.width / Screen.height;
        transform.localScale = new Vector3(quadWidth, quadHeight, 1);
    }

    private void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * speed);
        transform.GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
