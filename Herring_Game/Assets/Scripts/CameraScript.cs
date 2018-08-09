using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public int ScrollSpeed;
    public int ZoomSpeed;

    Vector3 startPos;

    public int maxZoom;

    void Start()
    {
        startPos = new Vector3(130, 230, 142);
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            transform.Translate(new Vector3(0, ScrollSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            transform.Translate(new Vector3(0, -ScrollSpeed * Time.deltaTime, 0));

        }

        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            transform.Translate(new Vector3(-ScrollSpeed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            transform.Translate(new Vector3(ScrollSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            if (transform.position.y > 30)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Vector3 dir = ray.direction.normalized;
                transform.Translate(new Vector3(-dir.x, -dir.z, dir.y) * -ZoomSpeed);

            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (transform.position.y < maxZoom)
            {
                transform.Translate(new Vector3(0, 0, -1) * ZoomSpeed);
            }
        }

        if (transform.position.z + transform.position.y * Mathf.Tan(Mathf.PI / 12) > 1900)
            transform.position = new Vector3(transform.position.x, transform.position.y, 1900 - transform.position.y * Mathf.Tan(Mathf.PI / 12));
        else if (transform.position.z - transform.position.y * Mathf.Tan(Mathf.PI / 12) < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * Mathf.Tan(Mathf.PI / 12));

        if (transform.position.x < -160)
            transform.position = new Vector3(-160, transform.position.y, transform.position.z);
        else if (transform.position.x > 460)
            transform.position = new Vector3(460, transform.position.y, transform.position.z);
    }
}
