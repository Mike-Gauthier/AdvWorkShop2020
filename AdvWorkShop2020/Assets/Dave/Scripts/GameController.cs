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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
                        currentPlaceable = Instantiate(placeablePrefabs[i]);
                        currentPlaceable.transform.parent = holder.transform; //parenting
                        currentPrefabIndex = i;
                        //pScript.woodCount = pScript.woodCount - 500;
                        //pScript.stoneCount = pScript.stoneCount - 500;
                        //pScript.mudCount = pScript.mudCount - 500;
                        Debug.Log("Mud tower placed. Counts -200");
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
        return currentPrefabIndex == i && currentPlaceable != null;
    }
}

