using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerringMovementScript : MonoBehaviour {

    public float speed;

    float time = Time.time;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - time > 1) {

            float vx = GetComponent<Rigidbody>().velocity.x;
            float vz = GetComponent<Rigidbody>().velocity.z;

            if (vx == 0)
                vx = 0.01f;

            float angle = Mathf.Atan(vx / vz);
            float newAngle = angle + Random.Range(-Mathf.PI / 6, Mathf.PI / 6);
            GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Cos(newAngle) * speed, 0, Mathf.Sin(newAngle) * speed);
            transform.forward = GetComponent<Rigidbody>().velocity;

            Debug.Log(angle + " " + vz + " " + vx);

            time = Time.time;
        }
	}
}
