using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float sprintSpeedMultiplier;
    public float jumpPower;
    public float gravity;
    private Vector3 direction = Vector3.zero;
    private CharacterController controller;
    public int woodCount;
    public int stoneCount;
    public int mudCount;
    public int bricksCount;
    public GameObject swingHitbox;
    public bool swingOnCD;

    public TextMeshProUGUI woodNumber;
    public TextMeshProUGUI stoneNumber;
    public TextMeshProUGUI mudNumber;
    public TextMeshProUGUI bricksNumber;

    private Animator anim;
    public GameObject animatedModel;

    bool debugMode;

    public GameObject bridgePopUp;
    public GameObject currentBridge;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        swingOnCD = false;
        anim = animatedModel.GetComponent<Animator>();
    }
    void Update()
    {
        if (controller.isGrounded) // Only recieves inputs if the player is grounded.
        {
            direction = (transform.forward) * Input.GetAxisRaw("Vertical") + (transform.right * (Input.GetAxisRaw("Horizontal")));
            direction = direction.normalized * speed;
            if(direction != Vector3.zero)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                direction = direction.normalized * speed * sprintSpeedMultiplier;
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                direction = direction.normalized * speed;
            }
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpPower;
            }
        }
        direction.y -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && swingOnCD == false) // swing method
        {
            StartCoroutine(ClickSwing());
            anim.SetTrigger("Attack");
        }
        mudNumber.SetText(mudCount.ToString());
        woodNumber.SetText(woodCount.ToString());
        stoneNumber.SetText(stoneCount.ToString());
        bricksNumber.SetText(bricksCount.ToString());

        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.U)) // So I don't have to spend time gathering resources whenever testing. - Zachary Simon
            {
                woodCount += 1000;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                stoneCount += 1000;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                mudCount += 1000;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                bricksCount += 1000;
            }
        }

        if (currentBridge == null)
        {
            bridgePopUp.SetActive(false);
        }
        else
        {
            bridgePopUp.SetActive(true);

            if (currentBridge.tag != "bridge") // The idea here is that since the current hitbox of the build bridge area is tagged "bridge", the player script stores a Game Object variable called Current Bridge. Once a bridge is actually built, the tag should be changed to something else. Once that happens the script will no longer store the current bridge variable and the bridge pop up should disappear. - ZS
            {
                currentBridge = null;
            }
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
            woodCount+= 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "stone")
        {
            stoneCount+= 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "mud")
        {
            mudCount+= 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "brick")
        {
            bricksCount += 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyCampScript>().health--;
        }
        if (other.gameObject.tag == "bridge")
        {
            currentBridge = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "bridge")
        {
            if (currentBridge != null)
            {
                currentBridge = null;
            }
        }
    }

    IEnumerator ClickSwing()
    {
        yield return new WaitForSeconds(.3f);
        swingOnCD = true;
        swingHitbox.SetActive(true);
        yield return new WaitForSeconds(.05f); // time the swing hitbox is active
        swingHitbox.SetActive(false);
        yield return new WaitForSeconds(.5f); // cooldown between swings
        swingOnCD = false;
    }

    public void UpdateDebugMode(bool update)
    {
        debugMode = update;
    }
}

