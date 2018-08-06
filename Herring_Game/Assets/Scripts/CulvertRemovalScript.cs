using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CulvertRemovalScript : MonoBehaviour {

    static bool ShownCulvertInfo = false;

    // Use this for initialization
    void Start () {
        GameObject.Find("GameUI").transform.Find("CulvertInfo").gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void updateChanges()
    {
        GameObject.Find("Sections").GetComponent<MainScript>().decrementNumChanges();
    }

    public void removeCulvert()
    {
        //StartCoroutine(GameObject.Find("Click_Plane").GetComponent<SectionShade>().Clicked(0.2f, transform.parent.gameObject));
        //Debug.Log("briv");
        int value = GameObject.Find("Restoration_Options").GetComponent<Dropdown>().value;
        if (value == 3)
        {
            transform.parent.transform.position = new Vector3(10, -100, 10);
            updateChanges();

            if (ShownCulvertInfo == false)
            {
                GameObject.Find("GameUI").transform.Find("CulvertInfo").gameObject.SetActive(true);
                ShownCulvertInfo = true;
            }
        }
    }

    public bool isRemoved()
    {
        if (transform.parent.transform.position.y == -100)
            return true;

        return false;
    }
}
