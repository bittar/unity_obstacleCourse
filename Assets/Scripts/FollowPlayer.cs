using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;

    private float camBankingSpeed = 0.5f;
    private float current = 0.5f;
    private float target = 0.5f;

    [SerializeField] private AnimationCurve curve;
    Quaternion cameraBankingLeft = Quaternion.Euler(0, 0, -4);
    Quaternion cameraBankingRight = Quaternion.Euler(0, 0, 4);
    Quaternion cameraNeutral = Quaternion.Euler(0, 0, 0);


    // Update is called once per frame
    void Update()
    {
        // Bank Camera
        current = Mathf.MoveTowards(current, target, camBankingSpeed * Time.deltaTime);
        //Debug.Log("current: " + current + " & " + "target: " + target);
        transform.rotation = Quaternion.Lerp(cameraBankingLeft, cameraBankingRight, curve.Evaluate(current));

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            target = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            target = 0f;
        }

        if (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false)
        {
            target = 0.5f;
        }

    }

    private void LateUpdate()
    {
        if (gameObject != null) { 
        // Follow Player
        Vector3 targetPostition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPostition, ref currentVelocity, smoothTime);
        }
    }
}
