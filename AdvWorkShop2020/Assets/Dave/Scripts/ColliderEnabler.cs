using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnabler : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Enable());
    }

    IEnumerator Enable()
    {
        yield return new WaitForSeconds(.4f);
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
