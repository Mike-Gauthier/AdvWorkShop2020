using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnabler : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(Enable());
    }

    IEnumerator Enable()
    {
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SphereCollider>().enabled = true;
    }
}
