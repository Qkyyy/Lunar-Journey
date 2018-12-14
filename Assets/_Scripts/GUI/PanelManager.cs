using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    public List<GameObject> panels;

    GameObject startPanel;

	// Use this for initialization
	void Start ()
    {
        startPanel = GameObject.Find("MainMenu_Panel");
        panels.Add(GameObject.Find("Credits_Panel"));
        panels.Add(startPanel);
        ActivatePanel(startPanel);
    }
	


    public void ActivatePanel(GameObject panel)
    {
        foreach(GameObject element in panels)
        {
            if (element == panel)
            {
                element.SetActive(true);
            }
            else
                element.SetActive(false);
        }
    }
}
