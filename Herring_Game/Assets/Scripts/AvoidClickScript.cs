using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AvoidClickScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Coonamessett").GetComponent<RestorationScript>().setEnable(false);
        Debug.Log("Entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("Coonamessett").GetComponent<RestorationScript>().setEnable(true);
        Debug.Log("Exited");
    }

    // Use this for initialization
    void Start () {
        GameObject.Find("Coonamessett").GetComponent<RestorationScript>().setEnable(true);
    }
	
	// Update is called once per frames
	void Update () {
		
	}
}
