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

    //Particle System
    public ParticleSystem particles;


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

        //Binds Particle System to player
        particles.transform.position = movement.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 impactForce = collision.relativeVelocity;

        if (collision.collider.tag == "Obstacle" && sideTouch == false)
        {

            movement.enabled = false;
            followplayer.enabled = false;
            movement.rb.constraints = RigidbodyConstraints.None;

            // Sets GAME OVER state
            FindObjectOfType<GameManager>().endGame();

            // Adds Destroyed Version of the player and destroys/removes main player.
            GameObject newObj = Instantiate(playerDestroyed, transform.position, transform.rotation);
            Destroy(gameObject);

            //Adds Velocity to Detroyed Player Children's Rigid Body
            float variationScale = 0.5f;
            float impactForceMag = impactForce.magnitude;

            foreach(Transform child in newObj.transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce((impactForce + (Random.insideUnitSphere * variationScale * impactForceMag)) * -1, ForceMode.Impulse);
                }
            }

            // Starts Particle System
            particles.Play();
        }

        if (collision.collider.tag == "Ground")
        {
            //movement.inAirMove = 0;
            //movement.rb.angularVelocity = new Vector3(0f, 0f, 0f);
            //movement.transform.Rotate(0f, 0f, 0f);

        }
        
    }
}
