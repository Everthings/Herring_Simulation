using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBendGeneratorScript : MonoBehaviour
{

    public Terrain River;
    public Terrain River_bends_1;
    public Terrain River_bends_2;
    public Terrain River_bends_3;
    public Terrain River_bends_4;
    /* public Terrain River_bends_5;
     public Terrain River_bends_6;
     public Terrain River_bends_7;
     public Terrain River_bends_8;
     public Terrain River_bends_9;
     public Terrain River_bends_10;*/ //not added yet

    Terrain[] terrains;

    int current = 0;

    List<GameObject> sections;

    // Use this for initialization
    void Start()
    {

        sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

        River.gameObject.SetActive(true);
        River_bends_1.gameObject.SetActive(false);
        River_bends_2.gameObject.SetActive(false);
        River_bends_3.gameObject.SetActive(false);
        River_bends_4.gameObject.SetActive(false);
        //feel free to recode this with an arrayList, I'm too lazy to do that
        terrains = new Terrain[] { River, River_bends_1, River_bends_2, River_bends_3, River_bends_4 };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextTerrain()
    {
        if (current + 1 < terrains.Length)
        {
            terrains[current].gameObject.SetActive(false);
            current = (current + 1);
            terrains[current].gameObject.SetActive(true);
        }

        if (current > 0)
            sections[current - 1].GetComponent<TreeShrubGeneratorScript>().river_winding = true;


        sections[current - 1].GetComponent<TreeShrubGeneratorScript>().regenerateAll(terrains[current]);
    }
}
