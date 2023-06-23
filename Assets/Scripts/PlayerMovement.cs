using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // References the Rigidbody Component
    public Rigidbody rb;
    public Transform playerPosition;

    public float forwardForce = 4000f;
    public float movementSpeed = 100f;
    public float inAirMove;

    public float jump = 100f;
    public bool playerGrounded = false;

    private bool startGame; 

    //Float
    //public Transform[] anchors = new Transform[4];
    //RaycastHit[] hits = new RaycastHit[4];

    //public float multiplier;
    //public float moveForce, turnToque;


    //void ApplyForce(Transform anchor, RaycastHit hit)
    //{
    //    if(Physics.Raycast(anchor.position, -anchor.up, out hit))
    //    {
    //        float force = 0;
    //        force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
    //        rb.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);
    //    }
    //}


    void Start()
    {
      rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void MoveForward()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        
        
    }

    public void MoveLeft()
    {
      rb.AddForce((-movementSpeed - inAirMove) * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    
    public void MoveRight()
    {
      rb.AddForce((movementSpeed + inAirMove) * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }


    void playerJump()
    {
        if (playerPosition.position.y <= 1.55)
        {
            rb.AddForce(0, jump * Time.fixedDeltaTime, 0, ForceMode.VelocityChange);
            playerPosition.Rotate(0, 0, 0);
        }
    }

    void isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.55f))
        {
            playerGrounded = true;
            inAirMove = 0;
        } else
        {
            playerGrounded = false;
            inAirMove = -100;
        }
    }



    // Update is called once per frame. Changed to FixedUpdate
    // because we're using the Physics Engine.


    void FixedUpdate()
    {

        //// Moves Player forward constantly
        if (Input.GetKey(KeyCode.Return))
        {
            startGame = true;
              
        }

        if (startGame == true)
        {
            MoveForward();
            Debug.Log(rb.velocity);
        }




        ////Moves Player Laterally when keys are pressed
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            MoveRight();
        }

        // Check if Player is on the Ground
        isGrounded();

        //Jump!
        if (Input.GetKey(KeyCode.Space) && playerGrounded == true)
        {
            playerJump();
            Debug.Log("JUMP");
        }

        //// Hover
        //for(int i = 0; i < 4; i++)
        //{
        //    ApplyForce(anchors[i], hits[i]);
        //}



        // Game Over if Player Falls Off the Edge
        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().endGame();
        }
    }
}

