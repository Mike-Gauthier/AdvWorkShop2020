using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonText : MonoBehaviour
{
    public GameObject buttonTextObject;
    Text buttonText;

    Color defaultColor;

    public Color highlightColor;

    public bool movesWhenClicked;

    bool textHighlighted;
    bool mouseHovering;
    bool clicked;

    Vector3 defaultPosition;
    public Vector3 newPositionWhenClicked;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = buttonTextObject.GetComponent<Text>();
        defaultColor = buttonText.color;

        defaultPosition = buttonTextObject.transform.localPosition;
    }

    // Update is called once per frame

    private void Update()
    {
        if (clicked && movesWhenClicked && mouseHovering)
        {
            buttonTextObject.transform.localPosition = newPositionWhenClicked;
        }
        else
        {
            if (!clicked)
            {
                buttonTextObject.transform.localPosition = defaultPosition;
            }
        }

        if (clicked && Input.GetKeyUp(KeyCode.Mouse0))
        {
            clicked = false;
        }

        if (textHighlighted)
        {
            buttonText.color = highlightColor;
        }
        else
        {
            buttonText.color = defaultColor;
        }

        if (mouseHovering)
        {
            textHighlighted = true;
        }
        else
        {
            if (!Input.GetKey(KeyCode.Mouse0) && !clicked)
            {
                textHighlighted = false;
            }
        }
    }

    /*
    private void OnPointerOver(PointerEventData eventData)
    {
        Debug.Log("Mouse cursor detected.");

        if (!Input.GetKey(KeyCode.Mouse0))
        {
            mouseHovering = true;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            clicked = true;
        }
    } */

    public void DetectMouse()
    {
        Debug.Log("Mouse cursor detected.");

        if (!Input.GetKey(KeyCode.Mouse0))
        {
            mouseHovering = true;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            clicked = true;
        }
    }

    public void MouseNoLongerDetected()
    {
        mouseHovering = false;
    }

    public void ClickActive()
    {
        clicked = true;
    }

    public void ClickNotActive()
    {
        clicked = false;
    }

    public void RebootButtonText()
    {
        textHighlighted = false;
        clicked = false;
        mouseHovering = false;
    }
}
