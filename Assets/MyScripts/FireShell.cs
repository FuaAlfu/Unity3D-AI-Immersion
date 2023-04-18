using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject turret;

    [SerializeField]
    Transform turretBase;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float speed = 15f;

    [SerializeField]
    private float rotSpeed = 5f; //old: 2f

    [SerializeField]
    float moveSpeed = 1f;

    // Start is called before the first frame update
    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * turretBase.forward;
    }

    float? RotateTurret()
    {
        float? angle = CaclulateAngle(true);
        if(angle != null)
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        return angle;
    }

    float? CaclulateAngle(bool low)
    {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude - 1;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else
            return null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = enemy.transform.position - this.transform.position.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,lookRotation,Time.deltaTime * rotSpeed);
        float? angle = RotateTurret();  //old: RotateTurret();
        if (angle != null)  //old: (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet();
        }
        else
        {
            this.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        }
    }
}
