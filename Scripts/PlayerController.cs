using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;

    private float movementX;
    private float movementY;
    //public float jumpSpeed = 10;
    //float jumpSpeed;
    private bool OnGround = true;
    private const int MAX_JUMP = 2;
    private int currentJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
/*
        if (Input.GetKeyDown(KeyCode.Space) && (onGround || MAX_JUMP > currentJump))
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            //Vector3 jump = new Vector3(0.0f, jumpSpeed, 0.0f);
            //rb.AddForce(jump);
            onGround = false; 
            currentJump++;
        }
*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();

        }
    }

    void OnJump()
    {
        if (OnGround || MAX_JUMP > currentJump)
        {
            rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
            OnGround = false;
            currentJump++;
        }
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12) 
        {
            winTextObject.SetActive(true);
        }
    }

    //void OnCollisionStay(){}

    void OnCollisionEnter(Collision collision)
    {
        OnGround = true;
        currentJump = 0;
    }

}


