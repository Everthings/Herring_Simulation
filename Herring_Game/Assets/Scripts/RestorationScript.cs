using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorationScript : MonoBehaviour
{

    bool enable = true; //don't update terrain if exterior button is clicked
    float time = Time.time;

    // Use this for initialization
    void Start()
    {

    }

    public void setEnable(bool b)
    {
        enable = b;
    }

    void updateChanges()
    {
        GameObject.Find("Sections").GetComponent<MainScript>().decrementNumChanges();
    }

    // Update is called once per frame
    void Update()
    {
        if (enable && GameObject.Find("Restoration_Options").GetComponent<Dropdown>().enabled && GameObject.Find("Sections").GetComponent<MainScript>().getChangesLeft() > 0)
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
    }

    void handleClick(RaycastHit hit)
    {
        int value = GameObject.Find("Restoration_Options").GetComponent<Dropdown>().value;

        switch (value)
        {
            case 0: //Trees
                {
                    List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

                    for (int i = 0; i < sections.Count; i++)
                    {

                        if (hit.point.x > sections[i].GetComponent<TreeShrubGeneratorScript>().right_bound && hit.point.x < sections[i].GetComponent<TreeShrubGeneratorScript>().left_bound && hit.point.z < sections[i].GetComponent<TreeShrubGeneratorScript>().upper_bound && hit.point.z > sections[i].GetComponent<TreeShrubGeneratorScript>().lower_bound)
                        {

                            sections[i].GetComponent<TreeShrubGeneratorScript>().addTrees(10);
                            updateChanges();

                            break;
                        }
                    }

                    break;
                }
            case 1: //Shrubs
                {
                    List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

                    for (int i = 0; i < sections.Count; i++)
                    {

                        if (hit.point.x > sections[i].GetComponent<TreeShrubGeneratorScript>().right_bound && hit.point.x < sections[i].GetComponent<TreeShrubGeneratorScript>().left_bound && hit.point.z < sections[i].GetComponent<TreeShrubGeneratorScript>().upper_bound && hit.point.z > sections[i].GetComponent<TreeShrubGeneratorScript>().lower_bound)
                        {
                            sections[i].GetComponent<TreeShrubGeneratorScript>().addShrubs(5);
                            updateChanges();
                            break;
                        }
                    }
                    break;
                }
            case 2: //River
                {
                    GameObject.Find("Coonamessett").GetComponent<RiverBendGeneratorScript>().NextTerrain();
                    updateChanges();
                    break;
                }
            case 3: //Culverts
                {
                    List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

                    for (int i = 0; i < sections.Count; i++)
                    {

                        if (hit.point.x > sections[i].GetComponent<TreeShrubGeneratorScript>().right_bound && hit.point.x < sections[i].GetComponent<TreeShrubGeneratorScript>().left_bound && hit.point.z < sections[i].GetComponent<TreeShrubGeneratorScript>().upper_bound && hit.point.z > sections[i].GetComponent<TreeShrubGeneratorScript>().lower_bound)
                        {
                            if(sections[i].GetComponent<CulvertRemovalScript>().removeCulvert())
                                updateChanges();
                            break;
                        }
                    }

                    break;
                }
        }
    }
}

