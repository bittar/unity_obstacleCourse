using UnityEngine;

public class PlayerCollision : MonoBehaviour { 

    public PlayerMovement movement;
    public Collider playerCollider;
    public GameObject playerDestroyed;

    [SerializeField]
    private bool sideTouch;

    // Trying to find the CAMERA SCRIPT
    public GameObject cam;
    public FollowPlayer followplayer;

    private void Start()
    {
        followplayer = cam.GetComponent<FollowPlayer>();
    }

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
            followplayer.enabled = false;
            movement.rb.constraints = RigidbodyConstraints.None;
            FindObjectOfType<GameManager>().endGame();
            Destroy(gameObject);
            Instantiate(playerDestroyed, transform.position, transform.rotation);
        }

        if (collision.collider.tag == "Ground")
        {
            //movement.inAirMove = 0;
            movement.rb.angularVelocity = new Vector3(0f, 0f, 0f);
            movement.transform.Rotate(0f, 0f, 0f);

        }
        
    }
}
