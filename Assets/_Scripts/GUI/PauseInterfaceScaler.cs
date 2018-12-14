using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInterfaceScaler : MonoBehaviour {
    RectTransform background;
    RectTransform exitButton;
    RectTransform playButton;

    private void Awake()
    {
        background = transform.GetChild(0).GetComponent<RectTransform>();
        playButton = transform.GetChild(1).GetComponent<RectTransform>();
        exitButton = transform.GetChild(2).GetComponent<RectTransform>();
        Debug.Log(Screen.width + " " + Screen.height);
    }

    private void Start()
    {
        background.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height / 1.75f);
        background.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 1.5f);

        //places the playButton above the center of background
        playButton.anchoredPosition = new Vector2(background.anchoredPosition.x, background.anchoredPosition.y + (background.sizeDelta.y / 6));
        playButton.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, background.sizeDelta.x/1.5f);
        playButton.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, background.sizeDelta.y / 5);

        exitButton.anchoredPosition = new Vector2(background.anchoredPosition.x, background.anchoredPosition.y - (background.sizeDelta.y / 6));
        exitButton.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, background.sizeDelta.x/1.5f);
        exitButton.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, background.sizeDelta.y / 5);



        // Debug.Log(background.sizeDelta.x); // szer
        // Debug.Log(background.sizeDelta.y); // wys

    }
}
