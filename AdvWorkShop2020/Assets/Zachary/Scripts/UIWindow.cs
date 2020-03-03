using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIWindow : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 windowPosition;

    public GameObject cursorTracker;
    public Transform windowDestination;

    public bool canDrag;
    public bool dragging;
    bool attachedToNewPoint;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        windowPosition = gameObject.transform.position;

        if (cursorTracker == null)
        {
            cursorTracker = GameObject.FindGameObjectWithTag("Cursor Tracker");
        }

        if (Input.GetButton("LeftClick") && canDrag)
        {
            dragging = true;
        }
        else
        {
            dragging = false;
        }

        if (dragging)
        {
            attachedToNewPoint = false;
            Cursor.visible = false;

            gameObject.transform.SetParent(cursorTracker.transform);
            windowDestination.position = gameObject.transform.position;
            
        }
        else
        {
            Cursor.visible = true;

            if (!attachedToNewPoint)
            {
                cursorTracker.transform.DetachChildren();
                gameObject.transform.SetParent(windowDestination);
                transform.localPosition = Vector3.zero;
                attachedToNewPoint = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cursor Tracker")
        {
            canDrag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!dragging && collision.gameObject.tag == "Cursor Tracker")
        {
            canDrag = false;
        }
    }
}

