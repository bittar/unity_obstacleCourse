using UnityEngine;

public class PlayerCollision : MonoBehaviour { 

    public PlayerMovement movement;
    public Collider playerCollider;
    public GameObject playerDestroyed;
    public Rigidbody destroyedRB;

    [SerializeField]
    private bool sideTouch;

    public Rigidbody[] brokenRbs;


    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit, 0.51f))
        {
            sideTouch = true;
            Debug.Log("Hit Left");
        }
        else if (Physics.Raycast(transform.position, transform.right, out hit, 0.51f))

        {
            sideTouch = true;
            Debug.Log("Hit Right");
        } else
        {
            sideTouch = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Obstacle" && sideTouch == false)
        {
            movement.enabled = false;
            movement.rb.constraints = RigidbodyConstraints.None;
            FindObjectOfType<GameManager>().endGame();
            Destroy(gameObject);
            Instantiate(playerDestroyed, transform.position, transform.rotation);
           
            //for (int i = 0; i < 21; i++)
            //{
            //    destroyedRB.velocity = movement.rb.velocity;
            //}

            


        }

        if (collision.collider.tag == "Ground")
        {
            //movement.inAirMove = 0;
            movement.rb.angularVelocity = new Vector3(0f, 0f, 0f);
            movement.transform.Rotate(0f, 0f, 0f);

        }
        
    }
}
