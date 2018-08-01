﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerringGeneratorScript : MonoBehaviour {

    public GameObject herring;
    public NavMeshSurface surface;

    public int maxX;
    public int minX;
    public int maxZ;
    public int minZ;
    public float maxY;
    public float minY;

    int numHerring = 0;

    List<GameObject> herrings;

    bool running = false;

    public Material m;

    List<Vector3> culvertPositions = new List<Vector3>();

    // Use this for initialization
    void Start () {
        m.SetFloat("_UnderwaterMode", 0f);
        m.SetFloat("_DepthTransparency", 10f);
        GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;

        List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

        for (int i = 0; i < sections.Count; i++)
        {
            culvertPositions.Add(sections[i].transform.Find("Culvert").transform.Find("Tube_Line").transform.position);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (running)
        {
            for (int i = herrings.Count - 1; i >= 0; i--)
            {
                if (Vector3.Distance(herrings[i].transform.position, herrings[i].GetComponent<HerringMovementScript>().getDest()) < 5)
                {
                    GameObject temp = herrings[i];
                    herrings.Remove(temp);
                    Destroy(temp);
                }
            }

            //Debug.Log(herrings.Count + " vs " + numHerring);

            if (herrings.Count <= (float)numHerring * 1f / 3f)
            {
                running = false;
                GameObject.Find("Sections").GetComponent<MainScript>().incrementYear();
                

                for (int i = herrings.Count - 1; i >= 0; i--)
                {
                    Destroy(herrings[i]);
                }

                m.SetFloat("_UnderwaterMode", 0f);
                m.SetFloat("_DepthTransparency", 10f);
                GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;
            }


            //check for herring death
            List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

            for(int i = 0; i < herrings.Count; i++)
            {
                for (int j = 0; j < culvertPositions.Count; j++)
                {
                    if (Mathf.Abs(herrings[i].transform.position.z - culvertPositions[j].z) < 10 && !herrings[i].GetComponent<HerringMovementScript>().getCulvertsPassed()[j])
                    {
                        if(Random.value > sections[j].transform.Find("Culvert").GetComponent<KillScript>().getSurvivalRate())
                        {
                            GameObject temp = herrings[i];
                            herrings.Remove(temp);
                            Destroy(temp);

                            GameObject.Find("Coonamessett").GetComponent<HerringGeneratorScript>().decrementNumHerring();
                        }
                        else
                        {
                            herrings[i].GetComponent<HerringMovementScript>().getCulvertsPassed()[j] = true;
                        }

                        break;
                    }
                }
            }
        }
	}

    public void decrementNumHerring()
    {
        numHerring--;
    }

    public void spawnHerring(int num)
    {

        m.SetFloat("_UnderwaterMode", 1f);
        m.SetFloat("_DepthTransparency", 50f);
        GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;

        rebakeNavMesh();

        numHerring = num;

        herrings = new List<GameObject>();


        for (int i = 0; i < numHerring; i++)
        {
            GameObject temp = Instantiate(herring, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ)), new Quaternion());
            temp.transform.GetComponent<HerringMovementScript>().generatePath();
            herrings.Add(temp);
        }

        running = true;
    }

    public void rebakeNavMesh()
    {
        surface.BuildNavMesh();
    }
}
