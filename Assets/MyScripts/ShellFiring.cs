using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2023.4.7
/// </summary>

public class ShellFiring : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject turret;

    [SerializeField]
    GameObject enemy;

    // Start is called before the first frame update
    void CreateBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }

    Vector3 CalculateTrajectory()
    {
        Vector3 p = enemy.transform.position - this.transform.position;
        Vector3 v = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
        float s = bullet.GetComponent<MoveShell>().GetSpeed();

        //[quadratic formula]
        float a = Vector3.Dot(v, v) - s * s;
        float b = Vector3.Dot(p, v);
        float c = Vector3.Dot(p, p);
        float d = b * b - a * c;
        if (d < 0.1f) return Vector3.zero;

        float sqrt = Mathf.Sqrt(d);
        float t1 = (-b - sqrt) / c;
        float t2 = (-b + sqrt) / c;

        float t = 0;
        if (t1 < 0 && t2 < 0) return Vector3.zero;  //if (t1 < 0 && t2 < 0) t = 0;
        else if (t1 < 0) t = t2;
        else if (t2 < 0) t = t1;
        else
        {
            t = Mathf.Max(new float[] { t1, t2 });
        }
        return t * p + v;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 aimAt = CalculateTrajectory();
            if(aimAt != Vector3.zero)
                //this.transform.forward = CalculateTrajectory();
                this.transform.forward = aimAt;
            CreateBullet();
        }
    }
}
