using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int baseHealth = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Seeker"))
        {
            Debug.Log("Enemy Entered");
            baseHealth -= 1;
            Destroy(other.gameObject);
        }
    }
}
