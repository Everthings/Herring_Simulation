using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
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

    // Use this for initialization
    void Start()
    {
        River.gameObject.SetActive(true);
        River_bends_1.gameObject.SetActive(false);
        River_bends_2.gameObject.SetActive(false);
        River_bends_3.gameObject.SetActive(false);
        River_bends_4.gameObject.SetActive(false);
        //feel free to recode this with an arrayList, I'm too lazy to do that
        terrains = new Terrain[]{ River, River_bends_1, River_bends_2,River_bends_3,River_bends_4};
    }

    // Update is called once per frame
    void Update()
    {
        // left-click to toggle river bends
        if (Input.GetMouseButton(0))
        {
            NextTerrain(terrains);
        }

    }
    //watch out for my confusing single letter variables
    void NextTerrain(Terrain[] t)
    {

        t[current].gameObject.SetActive(false);
        current = (current + 1) % t.Length;
        t[current].gameObject.SetActive(true);

        GameObject.Find("Coonamessett").GetComponent<HerringGeneratorScript>().rebakeNavMesh();
    }
}
