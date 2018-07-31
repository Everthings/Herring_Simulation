using System.Collections;
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

	// Use this for initialization
	void Start () {
        m.SetFloat("_UnderwaterMode", 0f);
        m.SetFloat("_DepthTransparency", 10f);
        GameObject.Find("WaterPlane").GetComponent<MeshRenderer>().sharedMaterial = m;
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
        }
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
