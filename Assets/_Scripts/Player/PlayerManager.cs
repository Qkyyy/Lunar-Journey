using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    bool started = false;       //tells us if player started clicking

    Rigidbody2D rb;             //player's rigidbody2d

    float power = 10f;         //power of rb's addforce



    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

    }

    //after click, 'player' bounces on random directions
    private void OnMouseDown()
    {
        Time.timeScale = 1;
        //unblocks player after first click 
        if (started == false)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            started = true;
        }

        rb.AddForce(new Vector2(Random.Range(-50f, 50f), Random.Range(60f, 75f) * Time.deltaTime * power));

        //adds .y velocity so 'player' bounces up after click
        rb.velocity = new Vector2(0, 1.5f);
        
        // adds a torque so ball doesn't seems so static
        rb.AddTorque(0.35f);
    }



}
