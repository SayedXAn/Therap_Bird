using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public GameManager mngr;
    public Rigidbody2D birdBody;
    public Text debug;
    private void Start()
    {
        birdBody.transform.position = new Vector3(-150f,163f, 0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            birdBody.gravityScale = 3f;
            //debug.text = collision.gameObject.name;
            Debug.Log("Hit korse");
            mngr.BirdHitObstacle();
        }
        else if (collision.gameObject.tag == "gamewinobs")
        {
            mngr.BirdHitGameWinObs();
        }
        else if(collision.gameObject.tag == "right")
        {
            Destroy(collision.gameObject);
            mngr.BirdHitOption(true);

        }
        else if (collision.gameObject.tag == "wrong")
        {
            Destroy(collision.gameObject);
            //mngr.BirdHitObstacle();
            mngr.BirdHitOption(false);
        }
        else if(collision.gameObject.tag == "marker")
        {
            Destroy(collision.gameObject);
            mngr.BirdHitSpeedMarker();
        }
    }
}
