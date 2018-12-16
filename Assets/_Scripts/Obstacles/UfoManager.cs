using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoManager : MonoBehaviour {

    Transform leftBorder;
    Transform topBorder;
    Transform rightBorder;
    Transform ufoBeam;
    Transform playerTransform;

    Animator beamAnimator;
    Animator ufoAnimator;

    float finishPoint;          //point where ufo stops
    float playerAlpha = 1f;     //Alpha of player sprite
    float beamAlpha = 0f;


    bool move = true;                   //tells if ufo should move(translate)   
    bool beamCaughtPlayer = false;      //tells if player get caught in the beam
    bool playerInUfo = false;           //tells if player is up in ufo object
    bool pullPlayer = false;            //tells if player should still be pulled in beam
    bool ufoInRetreat = false;          //tells if ufo is retreating

    private void Awake()
    {
        //gameobjects Transforms
        leftBorder = GameObject.Find("LeftBorder").GetComponent<Transform>();
        topBorder = GameObject.Find("TopBorder").GetComponent<Transform>();
        rightBorder = GameObject.Find("RightBorder").GetComponent<Transform>();
        ufoBeam = transform.GetChild(0).GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        beamAnimator = ufoBeam.GetComponent<Animator>();
        ufoAnimator = transform.GetComponent<Animator>();


        //sets finish point where ufo stops
        finishPoint = Random.Range(leftBorder.position.x+0.2f, rightBorder.position.x - 0.2f);
        Debug.Log("ufo finish point " + finishPoint);
        //ignore collision with left border
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), leftBorder.GetComponent<Collider2D>());

        //at Awake teleport ufo away from the screen
        transform.SetPositionAndRotation(new Vector2(leftBorder.position.x - 3f, topBorder.position.y-0.7f), Quaternion.Euler(0, 0, 0));


        transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().material.color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().material.color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().material.color.b, beamAlpha);

    }
    private void Start()
    {
        ufoAnimator.Play("ufoMove");
    }

    private void FixedUpdate()
    {
        HandleBeamRetraction();
        HandleUfoMovement();
        HandlePlayerCatching();
        StartCoroutine(HandleUfoRetreat());
        HandleUfoDestroy();

    }

    //Pulls trigger from child(beam)
    public void PullTrigger(Collider2D c)
    {
        if (c.tag == "Player")
        {
            c.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            beamCaughtPlayer = true;
            pullPlayer = true;
        }
    }


    //if player is up in ufo object, 'player' alpha slowly changes to 0, after that UFO turns off beam and flies away
    void HandlePlayerCatching()
    {
        if (playerInUfo == true)
        {
            playerTransform.GetComponent<SpriteRenderer>().material.color = new Color(playerTransform.GetComponent<SpriteRenderer>().material.color.r, playerTransform.GetComponent<SpriteRenderer>().material.color.g, playerTransform.GetComponent<SpriteRenderer>().material.color.b, playerAlpha -= 0.2f * Time.deltaTime * 2f);
            if (playerTransform.GetComponent<SpriteRenderer>().material.color.a <= 0)
            {
                beamAnimator.Play("beamClose");
                ufoBeam.gameObject.SetActive(false);
                transform.Translate(Vector3.right * Time.deltaTime * 3f);
            }
        }
    }

    //When player enters the beam, it frozes him and moves into UFO
    void HandleBeamRetraction()
    {
        if (beamCaughtPlayer == true && pullPlayer == true)
        {
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
            playerTransform.position = new Vector2(ufoBeam.transform.position.x, playerTransform.position.y);
            playerTransform.GetComponent<Rigidbody2D>().AddTorque(0);
            playerTransform.Translate(Vector3.up * Time.deltaTime);

            if (playerTransform.position.y >= transform.position.y)
            {
                playerInUfo = true;
                pullPlayer = false;
                playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y);
            }
        }
    }

    //if bool 'move' is true, UFO flies onto the screen and stops at certain point (finishpoint), then releases beam
    void HandleUfoMovement()
    {
        if (move == true)
            transform.Translate(Vector3.right * Time.deltaTime);

        if (transform.position.x >= finishPoint)
        {
            move = false;
            if (ufoInRetreat == false)
                ufoBeam.gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().material.color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().material.color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().material.color.b, beamAlpha += 0.2f * Time.deltaTime * 2f);

            beamAnimator.Play("beamOpen");
        }
    }

    //Turns off beam and moves away if didnt catch player
    IEnumerator HandleUfoRetreat()
    {
        if (transform.position.x >= finishPoint)
        {
            yield return new WaitForSeconds(5f);
            if (beamCaughtPlayer == false)
            {
                ufoInRetreat = true;
                beamAnimator.Play("beamClose");
                yield return new WaitForSeconds(0.420f);
                ufoBeam.gameObject.SetActive(false);
                transform.Translate(Vector3.right * Time.deltaTime * 5f);
                
            }
        }
    }

    void HandleUfoDestroy()
    {
        if (transform.position.x > rightBorder.position.x+3f)
        {
            Destroy(transform.gameObject);
        }
    }

}
