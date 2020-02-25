using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{



    
    void Start()
    {
        
    }

    
    void Update()
    {
        TreeHitting();

    }


    public void TreeHitting() 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //RaycastHit hit;
            //if(Physics.Raycast(transform.position,transform.forward, out hit))
            //{
            //    //GameObject[] pieces = MeshCut.Cut(hit.collider.gameObject, transform.position, transform.right, capMaterial);
            //    if (!pieces[1].GetComponent<Rigidbody>())// General Idea of adding a meshcollider to the newly created mesh and making it convex so youre able to hit it
            //    {
            //        pieces[1].AddComponent<Rigidbody>();
            //        pieces[1].AddComponent<MeshCollider>();
            //        MeshCollider meshC = pieces[1].GetComponent<MeshCollider>();
            //        meshC.convex = true;
                    
            //    }
            //}
        }

    } 



}
