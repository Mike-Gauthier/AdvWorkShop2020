using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //finite state machine
    enum EnemyStates
    {
        March,
        Approach,
        Attack,
        Die
    }

    private void Start()
    {
        //FSMUpdate();
    }


    //private void Update()
    //{
        //check for player position using magnitude
        //while player.magnitude 
        //SpotPlayer
    //}

    /*void FSMUpdate()
    {
        EnemyStates currentState;
        currentState = EnemyStates.March;
        
        switch(currentState) 
        case 
    }*/
}
