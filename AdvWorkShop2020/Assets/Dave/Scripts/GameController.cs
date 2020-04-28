using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;
    private GameObject currentPlaceable;
    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;
    public PlayerController pScript;
    public GameObject holder;
    public BaseBehavior bScript;

    public bool debugMode; // Determines a debug mode. If active, allows for easily manipulating game states such as quickly gathering enough resources to test tower building or instantly activating win/loss conditions while saving time. - Zachary Simon
    bool updatedDebug; // Checks if player script is informed of debug status - ZS
    public GameObject debugTextHoldingObject; // Game Object with Debug text and shadow text as children - ZS

    ZachGameManager zachGameManager;

    bool paused;

    bool mudPlaceableActive; // A boolean that measures if the mud tower placable is active. - ZS
    bool stonePlaceableActive;
    bool woodPlaceableActive;
    public GameObject mudTowerUI;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        zachGameManager = GetComponent<ZachGameManager>();
    }


    void Update()
    {
        PlacementStart(); 
        if (currentPlaceable != null)
        {
            currentMove();
            currentRotate();
            ReleaseIfClicked();
        }

        if (debugMode)
        {
            debugTextHoldingObject.SetActive(true);

            if (!updatedDebug)
            {
                SendDebugUpdates();
                updatedDebug = true;
            }
        }
        else
        {
            debugTextHoldingObject.SetActive(false);

            if (updatedDebug)
            {
                SendDebugUpdates();
                updatedDebug = false;
            }
        }

        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if(Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (mudPlaceableActive)
        {
            mudTowerUI.SetActive(true);
        }
        else
        {
            mudTowerUI.SetActive(false);
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButton(0))
        {
            if (currentPlaceable.tag == "wood")
            {
                bScript.baseWoodCount = bScript.baseWoodCount - 15;
            }
            if (currentPlaceable.tag == "stone")
            {
                bScript.baseWoodCount = bScript.baseWoodCount - 10;
                bScript.baseStoneCount = bScript.baseStoneCount - 20;
            }
            if (currentPlaceable.tag == "mud")
            {
                bScript.baseWoodCount = bScript.baseWoodCount - 20;
                bScript.baseStoneCount = bScript.baseStoneCount - 20;
                bScript.baseBricksCount = bScript.baseBricksCount - 20;
            }

            currentPlaceable = null;
            if (mudPlaceableActive)
            {
                mudPlaceableActive = false;
            }
        }
    }

    private void currentRotate()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceable.transform.Rotate(Vector3.up, mouseWheelRotation * 10);
    }

    private void currentMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceable.transform.position = hitInfo.point;
            currentPlaceable.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal); // prevents floating
        }
    }

    private void PlacementStart()
    {
        for (int i = 0; i < placeablePrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (PressedKeyOfCurrentPrefab(i))
                {
                    Destroy(currentPlaceable);
                    currentPrefabIndex = -1;
                    //if (i == 0)
                    //{
                    //    pScript.woodCount = pScript.woodCount + 250;
                    //    Debug.Log("Wood tower materials refunded.");
                    //}
                    //if (i == 1)
                    //{
                    //    pScript.woodCount = pScript.woodCount + 100;
                    //    pScript.stoneCount = pScript.stoneCount + 500;
                    //    Debug.Log("Stone tower materials refunded.");
                    //}
                    //if (i == 2)
                    //{
                    //    pScript.woodCount = pScript.woodCount + 500;
                    //    pScript.stoneCount = pScript.stoneCount + 500;
                    //    pScript.mudCount = pScript.mudCount + 500;
                    //    Debug.Log("Mud tower materials refunded.");
                    //}
                }
                else
                {
                    if (currentPlaceable != null)
                    {
                        DeactivateTowerBooleans();
                        Destroy(currentPlaceable);
                    }

                    if (i == 0 && bScript.baseWoodCount >= 15)
                    {
                        currentPlaceable = Instantiate(placeablePrefabs[i]);
                        currentPlaceable.transform.parent = holder.transform; //parenting
                        currentPrefabIndex = i;
                        //pScript.woodCount = pScript.woodCount - 250;
                        Debug.Log("Wood tower placed. Counts -50");
                    }
                    if (i == 1 && bScript.baseWoodCount >= 10 && bScript.baseStoneCount >= 20)
                    {
                        currentPlaceable = Instantiate(placeablePrefabs[i]);
                        currentPlaceable.transform.parent = holder.transform; //parenting
                        currentPrefabIndex = i;
                        //pScript.woodCount = pScript.woodCount - 100;
                        //pScript.stoneCount = pScript.stoneCount - 500;
                        Debug.Log("Stone tower placed. Counts - 100");
                    }
                    if (i == 2 && bScript.baseWoodCount >= 20 && bScript.baseStoneCount >= 20 && bScript.baseMudCount >= 20)
                    {
                        mudPlaceableActive = true;
                        currentPlaceable = Instantiate(placeablePrefabs[i]);
                        currentPlaceable.transform.parent = holder.transform; //parenting
                        currentPrefabIndex = i;
                        //pScript.woodCount = pScript.woodCount - 500;
                        //pScript.stoneCount = pScript.stoneCount - 500;
                        //pScript.mudCount = pScript.mudCount - 500;
                        Debug.Log("Mud tower placed. Counts -200");
                    }
                    if (i == 2 && bScript.baseWoodCount < 20 || bScript.baseStoneCount < 20 || bScript.baseMudCount < 20) // Instantiates a translucent red tower that indicates the tower is not placeable. - ZS
                    {
                        mudPlaceableActive = true;
                        currentPlaceable = Instantiate(placeablePrefabs[i + 1]);
                        currentPlaceable.transform.parent = holder.transform; //parenting
                        currentPrefabIndex = i;
                        //pScript.woodCount = pScript.woodCount - 500;
                        //pScript.stoneCount = pScript.stoneCount - 500;
                        //pScript.mudCount = pScript.mudCount - 500;
                        Debug.Log("Insufficient Resources for Mud Tower.");
                    }
                    else
                    {
                        Debug.Log("Can't Afford to Place");
                    }

                }
                break;
            }
        }
    }
    private bool PressedKeyOfCurrentPrefab(int i) //refund materials here
    {
        if (mudPlaceableActive)
        {
            mudPlaceableActive = false;
        }
        return currentPrefabIndex == i && currentPlaceable != null;
    }

    void SendDebugUpdates()
    {
        zachGameManager.UpdateDebugMode(debugMode);
        pScript.UpdateDebugMode(debugMode);
        bScript.UpdateDebugMode(debugMode);
    }

    public void UpdatePauseStatus(bool update)
    {
        paused = update;
    }

    void DeactivateTowerBooleans()
    {
        if (mudPlaceableActive)
        {
            mudPlaceableActive = false;
        }
        if (stonePlaceableActive)
        {
            stonePlaceableActive = false;
        }
        if (woodPlaceableActive)
        {
            woodPlaceableActive = false;
        }
    }
}

