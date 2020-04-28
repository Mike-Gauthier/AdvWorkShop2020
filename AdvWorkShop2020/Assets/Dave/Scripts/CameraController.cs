using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform target2; // prevents the player object from flipping upside down
    public Vector3 offset;
    public float sensitivity;

    bool paused;

    private void Start()
    {
        offset = target.position - transform.position;
        target2.position = target.position;
        target2.parent = target;

    }
    void Update()
    {
        if (!paused)
        {
            float vertical = Input.GetAxis("Mouse Y") * -1 * sensitivity;
            target2.Rotate(vertical, 0, 0);

            if (target2.rotation.eulerAngles.x > 45f && target2.rotation.eulerAngles.x < 180f) // clamping
            {
                target2.rotation = Quaternion.Euler(45f, 0, 0);
            }

            if (target2.rotation.eulerAngles.x > 180f && target2.rotation.eulerAngles.x < 315f) // clamping
            {
                target2.rotation = Quaternion.Euler(315f, 0, 0);
            }

            float horizontal = Input.GetAxis("Mouse X") * sensitivity;
            target.Rotate(0, horizontal, 0); // setting the player's rotation

            float targetYAngle = target.eulerAngles.y;
            float targetXAngle = target2.eulerAngles.x;

            Quaternion rotation = Quaternion.Euler(targetXAngle, targetYAngle, 0);
            transform.position = target.position - (rotation * offset);

            transform.LookAt(target);
        }



    }

    public void UpdatePauseStatus(bool update)
    {
        paused = update;
    }
}

