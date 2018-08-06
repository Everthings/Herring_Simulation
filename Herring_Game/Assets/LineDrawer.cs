using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// © 2018 TheFlyingKeyboard and released under MIT License 
// theflyingkeyboard.net 
public class LineDrawer : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private bool simplifyLine = false;
    [SerializeField] private float simplifyTolerance = 0.02f;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos.y = Screen.height - mousePos.y;

            Rect bounds = GameObject.Find("Window").GetComponent<ExampleClass>().windowRect;

            if (mousePos.x > bounds.x && mousePos.x < bounds.x + bounds.width && mousePos.y > bounds.y && mousePos.y < bounds.y + bounds.height)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, hit.point);
                }
            }
        }
        */
    }

    public void drawLines(Vector2[] points)
    {

        line = GetComponent<LineRenderer>();

        line.positionCount = 0;

        for (int i = 0; i < points.Length; i++)
        {

            points[i].y = Screen.height - points[i].y;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(points[i]);
            if (Physics.Raycast(ray, out hit))
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, hit.point);
            }
        }
    }
}