using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUpdater : MonoBehaviour {

    Text highscore;

	void Start () {
        highscore = transform.GetComponent<Text>();
        highscore.text = PlayerPrefs.GetInt("highscore").ToString();
	}
	
}
