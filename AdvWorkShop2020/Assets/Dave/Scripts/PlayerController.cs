using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject swingHitbox;
    public bool swingOnCD;
    public TextMeshProUGUI woodNumber;
    public TextMeshProUGUI stoneNumber;
    public TextMeshProUGUI mudNumber;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        swingOnCD = false;
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && swingOnCD == false) // swing method
        {
            StartCoroutine(ClickSwing());
        }
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

    IEnumerator ClickSwing()
    {
        swingOnCD = true;
        swingHitbox.SetActive(true);
        yield return new WaitForSeconds(.05f); // time the swing hitbox is active
        swingHitbox.SetActive(false);
        yield return new WaitForSeconds(.5f); // cooldown between swings
        swingOnCD = false;
    }

}

