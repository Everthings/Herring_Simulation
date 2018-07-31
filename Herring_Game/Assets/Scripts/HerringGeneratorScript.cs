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

	// Use this for initialization
	void Start () {

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

            if (herrings.Count <= (float)numHerring * 2f / 3f)
            {
                GameObject.Find("Sections").GetComponent<MainScript>().incrementYear();
                running = false;

                for (int i = herrings.Count - 1; i >= 0; i--)
                {
                    Destroy(herrings[i]);
                }
            }
        }
	}

    public void spawnHerring(int num)
    {

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
