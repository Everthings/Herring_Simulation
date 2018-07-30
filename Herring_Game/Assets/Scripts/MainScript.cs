using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{

    bool buttonClicked = false; //don't update terrain if exterior button is clicked
    float time = Time.time;

    // Use this for initialization
    void Start()
    {

    }

    public void setButtonClicked(bool b)
    {
        buttonClicked = b;
    }

    // Update is called once per frame
    void Update()
    {
        if (!buttonClicked)
        {
            if (Input.GetMouseButton(0) && Time.time - time > 0.1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Casts the ray and get the first game object hit
                Physics.Raycast(ray, out hit);

                handleClick(hit);

                time = Time.time;
            }
        }

        buttonClicked = false;
    }

    void handleClick(RaycastHit hit)
    {
        int value = GameObject.Find("Restoration_Options").GetComponent<Dropdown>().value;

        switch (value)
        {
            case 0: //Trees
                {
                    List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

                    for(int i = 0; i < sections.Count; i++)
                    {

                        if (hit.point.x > sections[i].GetComponent<TreeGeneratorScript>().right_bound && hit.point.x < sections[i].GetComponent<TreeGeneratorScript>().left_bound && hit.point.z < sections[i].GetComponent<TreeGeneratorScript>().upper_bound && hit.point.z > sections[i].GetComponent<TreeGeneratorScript>().lower_bound)
                        {
                            if (sections[i].GetComponent<TreeGeneratorScript>().areTrees())
                                sections[i].GetComponent<TreeGeneratorScript>().incrementTreeAges();
                            else
                                sections[i].GetComponent<TreeGeneratorScript>().addTrees(10);

                            break;
                        }
                    }

                    break;
                }
            case 1: //Shrubs
                {
                    break;
                }
            case 2: //River
                {
                    GameObject.Find("Coonamessett").GetComponent<RiverBendGeneratorScript>().NextTerrain();
                    break;
                }
        }
    }
}
