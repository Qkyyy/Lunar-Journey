using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    bool started = false;       //tells us if player started clicking

    Rigidbody2D rb;             //player's rigidbody2d

    float power = 15f;         //power of rb's addforce



    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }


    //after click, 'player' bounces on random directions
    private void OnMouseDown()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = transform.position - clickPosition;
        //unblocks player after first click 
        if (started == false)
        {
            Time.timeScale = 1;
            rb.bodyType = RigidbodyType2D.Dynamic;
            started = true;
        }

        rb.velocity = Vector2.zero;
        rb.AddForce(dir.normalized * power,ForceMode2D.Impulse);

        //adds .y velocity so 'player' bounces up after click
        rb.velocity = new Vector2(rb.velocity.x, 2.5f);

        // adds a torque so ball doesn't seems so static
        rb.AddTorque(0.35f);
    }



}
