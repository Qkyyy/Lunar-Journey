using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBeamTrigger : MonoBehaviour {


    //Detects when child component's trigger collides with ball
    private void OnTriggerEnter2D(Collider2D c)
    {
        gameObject.GetComponentInParent<UfoManager>().PullTrigger(c);
    }
}
