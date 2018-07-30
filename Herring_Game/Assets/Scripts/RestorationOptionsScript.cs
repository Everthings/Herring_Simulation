using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestorationOptionsScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Coonamessett").GetComponent<MainScript>().setEnable(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("Coonamessett").GetComponent<MainScript>().setEnable(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
