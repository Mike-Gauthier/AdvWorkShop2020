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

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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
        
    }

    private void OnMouseOver() // Void name from proandrius. URL: https://forum.unity.com/threads/how-to-make-object-detect-mouse-is-over-it.223741/#post-1491314
    {
        canDrag = true;
    }
}

