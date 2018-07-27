using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public int maxZ;
    public int minZ;
    public int speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));

            if (transform.position.z < minZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));

            if (transform.position.z > maxZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward (against z direction... weird, I know)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));   
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backward (with z direction)
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
    }
}
