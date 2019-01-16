using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    Transform leftBorder;
    Transform rightBorder;
    Transform topBorder;

    float bottomCameraBorder;

    float obstacleSpeed = 5000f;

	void Start () {
        //assign Transforms to variables
        leftBorder = GameObject.Find("LeftBorder").GetComponent<Transform>();
        rightBorder = GameObject.Find("RightBorder").GetComponent<Transform>();
        topBorder = GameObject.Find("TopBorder").GetComponent<Transform>();

        //assign camera's bottom border (view) to variable
        bottomCameraBorder = Camera.main.ScreenToWorldPoint(new Vector3()).y-2f;


        //set position in between left and right border, above top border
        transform.position = new Vector2(Random.Range(leftBorder.position.x, rightBorder.position.x), topBorder.position.y + 2f);

        //adds force randomly at x position, and -100 at y position
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f,5f), -100) * Time.deltaTime * obstacleSpeed);

        //adds torque so meteor moves around randomly
        transform.GetComponent<Rigidbody2D>().AddTorque(Random.Range(100f, 30f));


        //ignores collision with borders, so obstacle can penetrate it
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), topBorder.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), leftBorder.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), rightBorder.GetComponent<Collider2D>(), true);
    }

    private void Update()
    {
        HandleDestroy();
    }


    //Handles any type of obstacle - killer ends the game etc.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (transform.tag)
        {
            //if its 'killer' type, teleport ball outside of camera view to its game over
            case "obstacle_killer":
                if(collision.gameObject.tag == "Player")
                    collision.transform.SetPositionAndRotation(new Vector2(leftBorder.position.x - 5f, collision.transform.position.y), Quaternion.Euler(0, 0, 0));
            break;
        }
    }

    //Destroys clone after it reaches certain point (bottomCameraBorder)
    void HandleDestroy()
    {
        if (transform.position.y < bottomCameraBorder)
            Destroy(gameObject);
    }
}
