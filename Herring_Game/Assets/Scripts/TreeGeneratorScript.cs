using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneratorScript : MonoBehaviour
{

    public Terrain river;
    public Tree prefab;

    List<Vector3> positions = new List<Vector3>();
    List<Vector2> possibleLocations = new List<Vector2>();

    public float upper_bound;
    public float lower_bound;
    public float right_bound;
    public float left_bound;
    public float bush_level;
    public float tree_level;
    public bool culvert_removed = false;
    public float survival_rate;

    float[,] heightMap;

    // Use this for initialization
    void Start()
    {
        populatePossibleLocations();
        addTrees(100);
        drawTrees();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void populatePossibleLocations()
    {

        for(int i = 0; i < 300; i+=5)
        {
            for (int j = 0; j < 1900; j+=5)
            {
                if (river.SampleHeight(new Vector3(i, 0, j)) >= 6.9f)
                {
                    possibleLocations.Add(new Vector2(i, j));
                } 
            }
        }
    }

    void addTrees(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector2 randPos = possibleLocations[(int)(Random.Range(0, 0.999f) * possibleLocations.Count)];
            Vector3 pos = new Vector3(randPos.x, 7, randPos.y);
            positions.Add(pos);

            possibleLocations.Remove(randPos);
        }
    }

    void drawTrees(){
        for (int i = 0; i < positions.Count; i++)
            Instantiate(prefab, positions[i], Quaternion.identity);
    }
}
