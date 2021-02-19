using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float collideBounce = 20.0f;
    public LevelManager level;

    private Rigidbody rigidBody;
    private Vector3 direction;
    private Vector3 facing;
    private Vector3 velocity;
    private bool isBouncing = false;
    private int lives;
    public bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        setWaiting(500f);
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    private void FixedUpdate()
    {
        if (!isBouncing && !isWaiting) { 
            //move the player
            rigidBody.position += velocity * Time.deltaTime;
            //update rotation
            transform.rotation = Quaternion.LookRotation(facing.normalized);
        }
    }

    private void handleInput()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        // save our last non-zero input for our rotation
        Vector3 direction = input.normalized;
        if (!input.Equals(Vector3.zero))
        {
            facing = direction;
        }
        velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy") { 

            rigidBody.AddForce(collision.contacts[0].normal * 3*collideBounce);
            Vector3 hitDir = Vector3.Lerp(Vector3.up, Vector3.left, 0.5f);
            rigidBody.AddForce(hitDir * 3 * collideBounce);
            rigidBody.AddTorque(Vector3.left * 50f);
            isBouncing = true;

            Invoke("StopBounce", 0.6f);
            level.Invoke("playerDied", 0.6f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            level.GoalReached();
        }
    }

    private void StopBounce()
    {
        isBouncing = false;
    }

    public void setWaiting(float delay)
    {
        isWaiting = true;
        Invoke("stopWaiting", delay);
    }

    public void stopWaiting()
    {
        isWaiting = false;
    }
}
