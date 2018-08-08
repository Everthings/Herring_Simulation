using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphPointMoveScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool pointerDown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (pointerDown)
        {
            //moves the button to the mouse's y (x doesn't move)
            Vector2 mouse = Input.mousePosition;

            Vector3 pos = GetComponent<RectTransform>().position;
            GetComponent<RectTransform>().position = new Vector3(pos.x, mouse.y, pos.z);

            GameObject.Find("Window").GetComponent<GraphScript>().redrawGraph();

            Vector2 pixels = GameObject.Find("Window").GetComponent<GraphScript>().pixelsToDataPoint(pos);

            transform.GetChild(0).GetComponent<Text>().text = pixels.y + "";
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }
}
