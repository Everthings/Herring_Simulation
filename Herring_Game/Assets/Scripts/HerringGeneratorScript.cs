using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HerringGeneratorScript : MonoBehaviour {

    public GameObject herring;
    public GameObject deathMarker;
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

    List<GameObject> deathMarkers = new List<GameObject>();

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

            if (herrings.Count <= (float)numHerring * 1f / 10f)
            {
                running = false;
                GameObject.Find("Sections").GetComponent<MainScript>().incrementYear();
                //----------------------------------------------------------

                for (int i = herrings.Count - 1; i >= 0; i--)
                {
                    Destroy(herrings[i]);
                }

                m.SetFloat("_UnderwaterMode", 0f);
                m.SetFloat("_DepthTransparency", 10f);
                GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;

                GameObject.Find("Sections").GetComponent<MainScript>().setHerringCount(GameObject.Find("Sections").GetComponent<MainScript>().herringMultiplier * numHerring);
                GameObject.Find("Sections").GetComponent<MainScript>().updateHerringCount();
            }


            //check for herring death in culverts
            List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

            for (int i = 0; i < herrings.Count; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Mathf.Abs(herrings[i].transform.position.z - culvertPositions[j].z) < 3 && !herrings[i].GetComponent<HerringMovementScript>().getCulvertsPassed()[j])
                    {
                        if (Random.value > sections[j].transform.Find("Culvert").GetComponent<KillScript>().getSurvivalRate())
                        {
                            GameObject temp = herrings[i];
                            killHerring(temp);
                            
                        }
                        else
                        {
                            herrings[i].GetComponent<HerringMovementScript>().getCulvertsPassed()[j] = true;
                        }

                        break;
                    }
                }
            }

            //check for herring death in river proper
            for (int i = 0; i < herrings.Count; i++)
            {

                int[] Zs = herrings[i].GetComponent<HerringMovementScript>().getCheckPositions();
                bool[] hasPassed = herrings[i].GetComponent<HerringMovementScript>().getSectionsPassed();

                if (Zs != null)
                { 
                    for (int j = 0; j < 10; j++)
                    {
                        if (Mathf.Abs(herrings[i].transform.position.z - Zs[j]) < 3 && !hasPassed[j])
                        {
                            if (Random.value > sections[j].GetComponent<KillScript>().getSurvivalRate())
                            {
                                GameObject temp = herrings[i];
                                killHerring(temp);
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

    public void removeDeathMarkers()
    {
        for (int i = deathMarkers.Count - 1; i >= 0; i--)
        {
            Destroy(deathMarkers[i]);
        }
    }

    void killHerring(GameObject obj)
    {
        deathMarkers.Add(Instantiate(deathMarker, new Vector3(obj.transform.position.x + Random.Range(-5, 5), 10, obj.transform.position.z + Random.Range(-5, 5)), new Quaternion()));
        herrings.Remove(obj);
        Destroy(obj);
        numHerring--;
        GameObject.Find("Herring_Text").GetComponent<Text>().text = "Herring Alive: " + GameObject.Find("Sections").GetComponent<MainScript>().herringMultiplier * numHerring;
    }

    public void spawnHerring(int num)
    {

        m.SetFloat("_UnderwaterMode", 1f);
        m.SetFloat("_DepthTransparency", 50f);
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
