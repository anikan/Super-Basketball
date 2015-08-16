using UnityEngine;
using System.Collections;

public class PlayerBallCollect : MonoBehaviour {

    public bool hasBall = false;
    public float throwStrength = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        //Let user go if escape is pushed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Make GUI visible

            //Time.timeScale = 0;
        }

        //Lock mouse.
        if (Input.GetMouseButtonDown(0))
        {
            //Allow user to rejoin if left.
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        //Handle ball actions
        if (hasBall)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasBall = false;
                GameObject ball = this.gameObject.transform.Find("MainCamera/Ball").gameObject;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.transform.parent = null;
                ball.GetComponent<Rigidbody>().AddForce((ball.transform.position - transform.position).normalized * throwStrength);
            }

            //Handle dribble
            if (Input.GetMouseButtonDown(1))
            {

            }
        }
    }

    //Grab the ball if collides with it.
    void OnCollisionEnter(Collision col)
    {
        //If we collided with the ball, then this player now owns the ball.
        if (col.gameObject.name == "Ball")
        {
            Debug.Log("Ball received");
            //Make this player now the parent of the ball.
            col.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            col.gameObject.transform.parent = this.transform.Find("MainCamera").transform;
            
            //This stops it from moving stupidly. Makes it not handle collisions.
            //Also allows the ball to properly follow the player.
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //Puts ball in front of player.
            col.gameObject.transform.localPosition = new Vector3(0,0,2);
            hasBall = true;
        }
    }

    private IEnumerator ChargeAndThrow()
    {
        Time.deltaTime;
        yield return null;

        if (Input.GetMouseButtonUp(0))
        {

        }
    }
}
