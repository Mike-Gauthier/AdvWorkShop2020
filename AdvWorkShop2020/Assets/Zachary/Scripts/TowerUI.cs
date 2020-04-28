using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public int woodCost;
    public int stoneCost;
    public int mudCost;
    public int brickCost;

    public int woodBaseCount;
    public int stoneBaseCount;
    public int mudBaseCount;
    public int brickBaseCount;

    public Text woodNumber;
    public Text stoneNumber;
    public Text mudNumber;
    public Text brickNumber;

    public Color sufficientResourcesColor;
    public Color insufficientResourcesColor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (woodNumber != null)
        {
            if (woodBaseCount >= woodCost)
            {
                woodNumber.color = sufficientResourcesColor;
            }
            else
            {
                woodNumber.color = insufficientResourcesColor;
            }
        }
        if (stoneNumber != null)
        {
            if (stoneBaseCount >= stoneCost)
            {
                stoneNumber.color = sufficientResourcesColor;
            }
            else
            {
                stoneNumber.color = insufficientResourcesColor;
            }
        }
        if (mudNumber != null)
        {
            if (mudBaseCount >= mudCost)
            {
                mudNumber.color = sufficientResourcesColor;
            }
            else
            {
                mudNumber.color = insufficientResourcesColor;
            }
        }
        if (brickNumber != null)
        {
            if (brickBaseCount >= brickCost)
            {
                brickNumber.color = sufficientResourcesColor;
            }
            else
            {
                brickNumber.color = insufficientResourcesColor;
            }
        }
    }

    public void UpdateWoodCount(int woodUpdate)
    {
        woodBaseCount = woodUpdate;
    }
    public void UpdateStoneCount(int stoneUpdate)
    {
        stoneBaseCount = stoneUpdate;
    }
    public void UpdateMudCount(int mudUpdate)
    {
        mudBaseCount = mudUpdate;
    }
    public void UpdateBrickCount(int brickUpdate)
    {
        brickBaseCount = brickUpdate;
    }
}
