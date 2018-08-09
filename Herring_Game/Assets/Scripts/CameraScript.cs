using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public int ScrollSpeed;
    public int ZoomSpeed;

    Vector3 startPos;

    public int maxZoom;

    public Texture2D cursorImage;

    void Start()
    {
        startPos = new Vector3(130, 230, 142);
        transform.position = startPos;

        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            transform.Translate(new Vector3(0, ScrollSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0));
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(new Vector3(0, ScrollSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0));

        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(new Vector3(ScrollSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0));
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(new Vector3(ScrollSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            if (transform.position.y > 30)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Vector3 dir = ray.direction.normalized;
                transform.Translate(new Vector3(-dir.x, -dir.z, dir.y) * -ZoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime);

            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (transform.position.y < maxZoom)
            {
                transform.Translate(new Vector3(0, 0, -1) * -ZoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime);
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
