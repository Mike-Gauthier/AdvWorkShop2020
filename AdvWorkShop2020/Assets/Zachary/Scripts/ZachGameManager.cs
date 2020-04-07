using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZachGameManager : MonoBehaviour
{
    public bool testBuild;

    bool paused;

    bool gameOver;

    bool expandBlackBackground;
    public GameObject blackBackground;
    public float blackBackgroundSize = 1.0f;
    [Range (10.0f, 100.0f)]
    public float blackBackgroundSizeLimit;
    public float blackBackgroundExpandRate;

    public Text gameOverText;
    bool gameOverTextAppearing;
    public float gameOverTextAppearRate;
    float gameOverTextAlpha = 0.0f;

    public Image gameOverImage;
    bool gameOverImageAppearing;
    public float gameOverImageAppearRate;
    float gameOverImageAlpha = 0.0f;

    public GameObject gameOverButtons;

    public GameObject pauseUI;

    public Transform mouseTracker;

    public Vector3 cursorLocation;

    public GameObject activeTowerUI;
    public GameObject newTowerUI;

    public bool isActiveTowerUICurrent;

    public GameObject objectToClose;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        mouseTracker.position = Input.mousePosition;

        if (isActiveTowerUICurrent && newTowerUI.activeInHierarchy)
        {
            newTowerUI.SetActive(false);
        }

        if (!isActiveTowerUICurrent && activeTowerUI.activeInHierarchy)
        {
            activeTowerUI.SetActive(false);
        }

        if (blackBackgroundExpandRate <= 0.0f)
        {
            blackBackgroundExpandRate = 0.1f;
        }

        UpdateGameOverImageAlpha();
        UpdateGameOverTextAlpha();
        gameOverButtons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cursorLocation = Input.mousePosition;
        mouseTracker.position = cursorLocation;
        // cursorLocation = Camera.main.ScreenToWorldPoint(cursorLocation);

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

        if (Input.GetKeyDown(KeyCode.Delete) && testBuild)
        {
            LossCondition();
        }

        if (gameOver)
        {
            if (paused)
            {
                paused = false;
            }

            blackBackground.SetActive(true);
            gameOverText.gameObject.SetActive(true);

            if (expandBlackBackground)
            {
                blackBackgroundSize += (blackBackgroundExpandRate * Time.deltaTime);
                blackBackground.transform.localScale = new Vector3(transform.localScale.x * blackBackgroundSize, transform.localScale.y * blackBackgroundSize, 1.0f);

                if (blackBackgroundSize >= blackBackgroundSizeLimit)
                {
                    blackBackgroundSize = blackBackgroundSizeLimit;
                    gameOverTextAppearing = true;
                    expandBlackBackground = false;
                }
            }

            if (gameOverTextAppearing)
            {
                gameOverTextAlpha += (gameOverTextAppearRate * Time.deltaTime);
                UpdateGameOverTextAlpha();

                if (gameOverTextAlpha >= 1.0f)
                {
                    gameOverTextAlpha = 1.0f;
                    gameOverImageAppearing = true;
                    gameOverTextAppearing = false;
                }
            }

            if (gameOverImageAppearing)
            {
                gameOverImageAlpha += (gameOverImageAppearRate * Time.deltaTime);
                UpdateGameOverImageAlpha();

                if (gameOverImageAlpha >= 1.0f)
                {
                    gameOverImageAlpha = 1.0f;
                    gameOverButtons.SetActive(true);
                    gameOverImageAppearing = false;
                }
            }

            if (Input.anyKey)
            {
                if (gameOverImageAppearing || gameOverTextAppearing)
                {
                    gameOverTextAppearRate = 50.0f;
                    gameOverImageAppearRate = 50.0f;
                }
            }

        }
        else
        {
            blackBackground.SetActive(false);
            gameOverText.gameObject.SetActive(false);
        }
    }

    public void SetPause()
    {
        if (!gameOver)
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

    public void CloseUIWindow(GameObject objectToClose)
    {
        objectToClose.SetActive(false);

        ReactivateCursor();
    }

    public void OpenTowerUI()
    {
        if (newTowerUI.activeInHierarchy)
        {
            CloseUIWindow(newTowerUI);
        }
        else if (activeTowerUI.activeInHierarchy)
        {
            CloseUIWindow(activeTowerUI);
        }
        else
        {
            if (isActiveTowerUICurrent)
            {
                OpenActiveTowerUI();
            }
            else
            {
                OpenNewTowerUI();
            }
        }
    }

    public void OpenActiveTowerUI()
    {
        activeTowerUI.transform.position = newTowerUI.transform.position;

        if(newTowerUI.activeInHierarchy)
        {
            newTowerUI.SetActive(false);
        }

        activeTowerUI.SetActive(true);

        isActiveTowerUICurrent = true;

        ReactivateCursor();
    }

    public void OpenNewTowerUI()
    {
        newTowerUI.transform.position = activeTowerUI.transform.position;

        if (activeTowerUI.activeInHierarchy)
        {
            activeTowerUI.SetActive(false);
        }

        newTowerUI.SetActive(true);

        isActiveTowerUICurrent = false;

        ReactivateCursor();
    }

    void ReactivateCursor()
    {
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }

    public void LossCondition()
    {
        if (!gameOver)
        {
            gameOver = true;
            expandBlackBackground = true;
        }
    }

    void UpdateGameOverTextAlpha()
    {
        gameOverText.color = new Vector4(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, gameOverTextAlpha);
    }

    void UpdateGameOverImageAlpha()
    {
        gameOverImage.color = new Vector4(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, gameOverImageAlpha);
    }
}
