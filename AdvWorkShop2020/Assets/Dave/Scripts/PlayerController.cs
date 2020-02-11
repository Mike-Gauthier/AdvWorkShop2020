using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float gravity;
    private Vector3 direction = Vector3.zero;
    private CharacterController controller;
    public float woodCount;
    public float stoneCount;
    public float mudCount;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (controller.isGrounded) // Only recieves inputs if the player is grounded.
        {
            direction = (transform.forward) * Input.GetAxisRaw("Vertical") + (transform.right * (Input.GetAxisRaw("Horizontal")));
            direction = direction.normalized * speed;
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpPower;
            }
        }
        direction.y -= gravity * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wood")
        {
            woodCount++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "stone")
        {
            stoneCount++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "mud")
        {
            mudCount++;
            Destroy(other.gameObject);
        }
    }

}

