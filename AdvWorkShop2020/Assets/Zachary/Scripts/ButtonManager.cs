using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // This code was added to allow this script to execute scene related codes like loading and exiting scenes. - Zachary Simon

public class ButtonManager : MonoBehaviour
{
    public int currentSceneNumber; // Type the number of the current scene in the inspector under the field labeled "Current Scene Number". To view the scene numbers, click File > Build Settings in the Unity Editor. New scenes may be added by opening the scene you want to add and clicking "Add Open Scenes" in the Build Settings window. - Zachary Simon

    public int sceneNumberOfFirstGameLevel; // Enter the scene number for the first level that will be loaded by the Start Game button.

    public GameObject[] pages;

    public int currentPage;

    public bool isInstructionScene;

    public bool integratesMultiplePageUI;

    // Start is called before the first frame update
    void Start()
    {
        if (isInstructionScene) // This is to allow scrolling during the instructions scene. Not intended for any gameplay scenes. - Zachary Simon
        {
            Time.timeScale = 0.0f; 
        }
        else
        {
            Time.timeScale = 1.0f; // Only sets the initial timescale to one. This does not interfere with pausing as of writing (2/3/20 at 12:12 AM) - Zachary Simon
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("The timescale of the game is " + Time.timeScale + ".");
        }

        if (Input.GetButtonDown("Right") && integratesMultiplePageUI && Time.timeScale < 0.1f) // Right along with Left are two new Axes I made for this project. They are similar to the Horizontal Axis but use only one arrow key. - Zachary Simon
        {
            int newPage;
            if (currentPage > pages.Length)
            {
                newPage = 0;
            }
            else
            {
                newPage = currentPage + 1;
            }
            TurnPages(newPage);
        }

        if (Input.GetButtonDown("Left") && integratesMultiplePageUI && Time.timeScale < 0.1f)
        {
            int newPage;
            if (currentPage <= 1)
            {
                newPage = pages.Length;
            }
            else
            {
                newPage = currentPage - 1;
            }
            TurnPages(newPage);
        }

        if (Input.GetButtonDown("Cancel") && isInstructionScene)
        {
            AccessMenu();
        }
    }

    public void AccessMenu()
    {
        SceneManager.LoadScene(0); // When this line is executed, the scene that is numbered zero is loaded. - Zachary Simon
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneNumberOfFirstGameLevel);
    }

    public void OpenNextScene()
    {
        SceneManager.LoadScene(currentSceneNumber + 1);
    }

    public void OpenPreviousScene()
    {
        SceneManager.LoadScene(currentSceneNumber - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("In a built version of the game, this button will close the game window.");
    }

    public void TurnPages(int nextPage)
    {
        foreach (GameObject individualPage in pages)
        {
            individualPage.SetActive(false);
        }

        if (nextPage > pages.Length)
        {
            nextPage = 1;
        }
        pages[nextPage - 1].SetActive(true);

        currentPage = nextPage;
    }
}
