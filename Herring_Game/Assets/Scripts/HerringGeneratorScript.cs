﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HerringGeneratorScript : MonoBehaviour {

    public GameObject herring;
    public GameObject deathMarkerTreeShrub;
    public GameObject deathMarkerCulvert;
    public GameObject deathMarkerRiver;
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

    List<GameObject> deathMarkers = new List<GameObject>();

    int updateEvery = 5;
    int counter = 0;

    // Use this for initialization
    void Start () {
        m.SetFloat("_UnderwaterMode", 0f);
        m.SetFloat("_DepthTransparency", 10f);
        GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;
    }
	
	// Update is called once per frame
	void Update () {

        counter++;

        if (running && counter % updateEvery == 0)
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

            if (herrings.Count <= (float)numHerring * 1f / 10f)
            {
                running = false;
                StartCoroutine(GameObject.Find("Sections").GetComponent<MainScript>().incrementYear(false));
                //----------------------------------------------------------

                for (int i = herrings.Count - 1; i >= 0; i--)
                {
                    Destroy(herrings[i]);
                }

                m.SetFloat("_UnderwaterMode", 0f);
                m.SetFloat("_DepthTransparency", 10f);
                GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;

                //GameObject.Find("Sections").GetComponent<MainScript>().setHerringCount(GameObject.Find("Sections").GetComponent<MainScript>().herringMultiplier * numHerring);
                GameObject.Find("Sections").GetComponent<MainScript>().updateHerringCount();
            }


            //check for herring death in culverts
            List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();
            List<GameObject> culverts = GameObject.Find("Culverts").GetComponent<CulvertCollectionScript>().getCulverts();

            for (int i = 0; i < herrings.Count; i++)
            {
                for (int j = 0; j < culverts.Count; j++)
                {
                    if (Mathf.Abs(herrings[i].transform.position.z - culverts[j].transform.Find("Tube_Line").transform.position.z) < 3 && !herrings[i].GetComponent<HerringMovementScript>().getCulvertsPassed()[j])
                    {
                        
                        if ((!culverts[j].transform.Find("Button").GetComponent<CulvertRemovalScript>().isRemoved() && Random.value > SurvivalData.UnrestoredSurvivalCulverts)
                            || (culverts[j].transform.Find("Button").GetComponent<CulvertRemovalScript>().isRemoved() && Random.value > SurvivalData.RestoredSurvivalCulverts))
                        {
                            GameObject temp = herrings[i];
                            StatisticsData.culvertKills += killHerring(temp, Kill.culvert);
                            GameObject.Find("Canvas").GetComponent<StatisticsScript>().updatePieChart();
                        }
                        else
                        {
                            herrings[i].GetComponent<HerringMovementScript>().getCulvertsPassed()[j] = true;
                        }

                        break;
                    }
                }
            }

            var pieChart = GameObject.Find("Canvas").GetComponent<StatisticsScript>();

            //check for herring tree/shrub/river death in river proper
            for (int i = 0; i < herrings.Count; i++)
            {
                int[] Zs = herrings[i].GetComponent<HerringMovementScript>().getCheckPositions();
                bool[] hasPassed = herrings[i].GetComponent<HerringMovementScript>().getSectionsPassed();

                if (Zs != null)
                { 
                    for (int j = 0; j < 10; j++)
                    {

                        if (!hasPassed[j])
                        {
                            var generator = sections[j].GetComponent<TreeShrubGeneratorScript>();

                            if (Mathf.Abs(herrings[i].transform.position.z - Zs[j]) < 3)
                            {
                                if (Random.value > generator.getTreeShrubSurvivalRate())
                                {
                                    GameObject temp = herrings[i];
                                    StatisticsData.treeShrubKills += killHerring(temp, Kill.treeShrub);
                                    pieChart.updatePieChart();
                                }
                                else if (Random.value > generator.getRiverSurvivalRate())
                                {
                                    GameObject temp = herrings[i];
                                    StatisticsData.riverKills += killHerring(temp, Kill.river);
                                    pieChart.updatePieChart();
                                }
                                else
                                {
                                    hasPassed[j] = true;
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }
	}

    public void removeDeathMarkers()
    {
        for (int i = deathMarkers.Count - 1; i >= 0; i--)
        {
            Destroy(deathMarkers[i]);
        }
    }

    int killHerring(GameObject obj, Kill type)
    {

        GameObject marker = null;
        switch (type)
        {
            case Kill.treeShrub:
                marker = deathMarkerTreeShrub;
                break;
            case Kill.culvert:
                marker = deathMarkerCulvert;
                break;
            case Kill.river:
                marker = deathMarkerRiver;
                break;

        }
        
        deathMarkers.Add(Instantiate(marker, new Vector3(obj.transform.position.x + Random.Range(-5, 5), 10, obj.transform.position.z + Random.Range(-5, 5)), new Quaternion()));
        herrings.Remove(obj);
        Destroy(obj);
        numHerring--;
        return GameObject.Find("Sections").GetComponent<MainScript>().decreaseHerring(GameObject.Find("Sections").GetComponent<MainScript>().herringMultiplier);
    }

    public void spawnHerring(int num)
    {

        StatisticsData.resetKilled();

        m.SetFloat("_UnderwaterMode", 1f);
        m.SetFloat("_DepthTransparency", 200f);
        GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;

        rebakeNavMesh();

        numHerring = num;

        herrings = new List<GameObject>();

        removeDeathMarkers();
        deathMarkers = new List<GameObject>();

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

enum Kill
{
    treeShrub, river, culvert
}
