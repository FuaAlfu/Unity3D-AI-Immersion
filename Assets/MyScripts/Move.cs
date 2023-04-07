using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject goal;
    Vector3 direction;
    float speed = 2f; //old 0.05f before Time delta..
    float threshold = 2f;
    void Start() 
    {
        //direction = goal.transform.position - this.transform.position;
       // this.transform.Translate(goal.transform.position);
    }

    private void LateUpdate() 
    {
        direction = goal.transform.position - this.transform.position;
        transform.LookAt(goal.transform.position);
        if (direction.magnitude > threshold)
        {
            Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            this.transform.position = this.transform.position + velocity;
        }
    }
}
