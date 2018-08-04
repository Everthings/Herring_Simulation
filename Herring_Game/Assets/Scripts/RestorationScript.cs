using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorationScript : MonoBehaviour
{

    static bool ShownTreeInfo = false;
    static bool ShownShrubInfo = false;
    static bool ShownBendsInfo = false;

    static bool enable = true; //don't update terrain if exterior button is clicked

    // Use this for initialization
    void Start()
    {
        GameObject.Find("Canvas").transform.Find("TreeInfo").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("ShrubInfo").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("BendsInfo").gameObject.SetActive(false);
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
        
    }

    public void handleClick()
    {
        if (enable && GameObject.Find("Restoration_Options").GetComponent<Dropdown>().enabled && GameObject.Find("Sections").GetComponent<MainScript>().getChangesLeft() > 0)
        {
            int value = GameObject.Find("Restoration_Options").GetComponent<Dropdown>().value;

            switch (value)
            {
                case 0: //Trees
                    {
                        if (!transform.parent.GetComponent<TreeShrubGeneratorScript>().areTrees())
                        {
                            transform.parent.GetComponent<TreeShrubGeneratorScript>().addTrees(400, 0);
                            updateChanges();
                            StartCoroutine(GameObject.Find("Click_Plane").GetComponent<SectionShade>().Clicked(0.2f, transform.parent.gameObject));
                            //Debug.Log(ShownTreeInfo);

                            if (ShownTreeInfo == false)
                            {
                                GameObject.Find("Canvas").transform.Find("TreeInfo").gameObject.SetActive(true);
                                ShownTreeInfo = true;
                            }
                        }

                        break;
                    }
                case 1: //Shrubs
                    {
                        if (!transform.parent.GetComponent<TreeShrubGeneratorScript>().areShrubs())
                        {
                            transform.parent.GetComponent<TreeShrubGeneratorScript>().addShrubs(500);
                            StartCoroutine(GameObject.Find("Click_Plane").GetComponent<SectionShade>().Clicked(0.2f, transform.parent.gameObject));
                            updateChanges();
                            if (ShownShrubInfo == false)
                            {
                                GameObject.Find("Canvas").transform.Find("ShrubInfo").gameObject.SetActive(true);
                                ShownShrubInfo = true;
                            }
                        }
                        break;
                    }
                case 2: //River
                    {
                        GameObject.Find("Coonamessett").GetComponent<RiverBendGeneratorScript>().NextTerrain();
                        updateChanges();
                        //StartCoroutine(GameObject.Find("Click_Plane").GetComponent<SectionShade>().Clicked(0.2f, sections[i]));
                        if (ShownBendsInfo == false)
                        {
                            GameObject.Find("Canvas").transform.Find("BendsInfo").gameObject.SetActive(true);
                            ShownBendsInfo = true;
                        }
                        break;
                    }
               // Culvert removal handled in culvert removal script
            }
        }
    }
}

