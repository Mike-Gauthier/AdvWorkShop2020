using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int baseHealth = 100;
    public int enemyValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Seeker"))
        {
            Debug.Log("Enemy Entered");
            baseHealth -= enemyValue; //put in enemy class
            Destroy(other.gameObject);
        }
    }
}
