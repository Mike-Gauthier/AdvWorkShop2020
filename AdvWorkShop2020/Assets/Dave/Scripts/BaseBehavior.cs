using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseBehavior : MonoBehaviour
{
    public PlayerController pScript;
    public int baseWoodCount;
    public int baseStoneCount;
    public int baseMudCount;
    public int baseBricksCount;
    //public TextMeshProUGUI popUp;

    public TextMeshProUGUI baseWoodNumber;
    public TextMeshProUGUI baseStoneNumber;
    public TextMeshProUGUI baseMudNumber;
    public TextMeshProUGUI baseBricksNumber;

    bool debugMode;


    private void Update()
    {
        baseMudNumber.SetText(baseMudCount.ToString());
        baseWoodNumber.SetText(baseWoodCount.ToString());
        baseStoneNumber.SetText(baseStoneCount.ToString());
        baseBricksNumber.SetText(baseBricksCount.ToString());

        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                baseWoodCount += 1000;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                baseStoneCount += 1000;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                baseMudCount += 1000;
            }

            if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                baseBricksCount += 1000;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && this.tag == "chest")
        {
            baseWoodCount = baseWoodCount + pScript.woodCount;
            baseStoneCount = baseStoneCount + pScript.stoneCount;
            baseMudCount = baseMudCount + pScript.mudCount;
            baseBricksCount = baseBricksCount + pScript.bricksCount;
            pScript.woodCount = 0;
            pScript.stoneCount = 0;
            pScript.mudCount = 0;
            pScript.bricksCount = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //popUp.text = "";
    }

    public void UpdateDebugMode(bool update)
    {
        debugMode = update;
    }


}
