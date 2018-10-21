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

    private void Update()
    {
        Debug.Log(rb.velocity.y);
    }

    //after click, 'player' bounces on random directions
    private void OnMouseDown()
    {
        //unblocks player after first click 
        if (started == false)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            started = true;
        }

        //stops velocity.y so 'player' stops falling down after click
        //rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(Random.Range(-75f, 100f), Random.Range(75f, 75f) * Time.deltaTime * power));

        //adds .y velocity so 'player' bounces up after click
        rb.velocity = new Vector2(0, 1.5f);
    }



    //'player' bouncing off of borders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-3, 0), ForceMode2D.Impulse);
            }
            else if (collision.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(3, 0), ForceMode2D.Impulse);
            }
        }
    }

}
