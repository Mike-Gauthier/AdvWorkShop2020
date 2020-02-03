using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool paused;

    public GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0.0f; // This stops the flow of time in game completely allowing the game to pause. Also allows other scripts to detect if the game is paused. Sounds can still play so more code may have to be written to deal with sound when the game is paused. On a plus side, in my last project pausing the game did not stop my weapon changing code from working so switching tools is very possible even with the time scale at zero. - Zachary Simon
            pauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f; // In case we ever add any slow motion effects to the game, this code may need to be adjusted to allow alternate timescales other than 1.0 while the game is unpaused. - Zachary Simon
            pauseUI.SetActive(false);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            SetPause();
        }
    }

    public void SetPause()
    {
        if (!paused)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }
}
