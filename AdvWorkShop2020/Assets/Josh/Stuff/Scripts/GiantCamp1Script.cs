using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantCamp1Script : MonoBehaviour
{
    public GameObject enemiesCon;
    public BoxCollider cageCol;

    void Update()
    {
        
        if(enemiesCon.transform.childCount >= 0)
        {
            //Make cage available 
            cageCol.enabled = true;
        }
    }


}
