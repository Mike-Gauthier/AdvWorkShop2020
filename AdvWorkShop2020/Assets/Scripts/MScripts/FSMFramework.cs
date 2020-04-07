using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMFramework : MonoBehaviour
{
    public enum EnemyStates
    {
        Approach,
        Attack,
        Death
    }

    public EnemyStates currentState;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.Approach:
                if(Vector3.Distance(player.position, transform.position) < 5.0f)
                {
                    transform.rotation = Quaternion.LookRotation(player.position);
                    transform.position = Vector3.MoveTowards(transform.position, player.position, 10);
                }
                if(Vector3.Distance(player.position, transform.position) < 2.0)
                {
                    currentState = EnemyStates.Attack;
                }
                else
                {
                    return;
                }
                break;


        }
    }
}
