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

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void spawnHerring(int num)
    {

        numHerring = num;

        herrings = new List<GameObject>();

        for (int i = 0; i < numHerring; i++)
        {
            GameObject temp = Instantiate(herring, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ)), new Quaternion());
            temp.transform.GetComponent<HerringMovementScript>().generatePath();
            herrings.Add(temp);
        }
    }

    public void rebakeNavMesh()
    {
        surface.BuildNavMesh();
    }
}
