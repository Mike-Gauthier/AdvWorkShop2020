using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject inventoryPanel;
    public bool open = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!open)
            {
                inventoryPanel.SetActive(true);
                open = false;
                Debug.Log("Menu Open");
            }
            else
            {
                inventoryPanel.SetActive(false);
                open = true;
                Debug.Log("Menu Closed");
            }
            open = !open;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryPanel.SetActive(false);
        }
    }

}
