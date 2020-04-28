
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public void Targeter(Transform _target)
    {
        target = _target;
    }


    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distancePerFrame, Space.World);
    }

    void HitTarget()
    {
        Debug.Log("We Hit");
        Destroy(gameObject);
    }
}
