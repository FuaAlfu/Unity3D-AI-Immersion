using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    float speed = 1f;
    float mass = 10;
    float force = 1000;
    float acceleration;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        acceleration = force / mass;
        speed += acceleration * 1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // acceleration = force / mass;
       // speed += acceleration;
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
