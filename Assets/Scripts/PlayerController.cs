using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float respawnHeight = -10f;

    Vector2 direction = Vector2.zero;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < respawnHeight)
            Respawn();
        Move(direction.x, direction.y);
    }

    void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();
        this.direction = direction;
    }

    private void Move(float x, float z)
    {
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
    }

    void OnJump()
    {
        if (isGrounded)
            Jump();
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
    }

    void OnCollisionStay(Collision collision)
    {
        //check if angle between normal vector of object of contact and up vector is less than 45 degrees
        //AKA if-statement is true if player is touching another object that is 0 to 45 degrees slope
        if (Vector3.Angle(collision.GetContact(0).normal, Vector3.up) < 45f)
            isGrounded = true;
        else
            isGrounded = false;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool isFlattened = false;
    void OnFlatten()
    {
        if (!isFlattened)
        {
            transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y / 2, transform.localScale.z * 2);
            isFlattened = true;
        }
        
    }
}