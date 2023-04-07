using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2023.4.5
/// </summary>

public class MoveShell : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  transform.Translate(0, Time.deltaTime * 0.5f, Time.deltaTime * speed);
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    public float GetSpeed()
    {
        return speed;
    }
}
