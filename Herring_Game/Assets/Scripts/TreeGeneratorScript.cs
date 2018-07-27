using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneratorScript : MonoBehaviour
{

    public Terrain river;
    public Tree prefab;

    List<Vector3> positions = new List<Vector3>();

    public float upper_bound;
    public float lower_bound;
    public float right_bound;
    public float left_bound;
    public float bush_level;
    public float tree_level;
    public bool culvert_removed = false;
    public float survival_rate;

    // Use this for initialization
    void Start()
    {
        addTrees(100);
        drawTrees();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addTrees(int num)
    {
        TreeInstance[] temp = river.terrainData.treeInstances;

        for (int i = 0; i < num; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, 300), 7, Random.Range(0, 1900));
            positions.Add(pos);
        }
    }

    void drawTrees(){
        for (int i = 0; i < positions.Count; i++)
            Instantiate(prefab, positions[i], Quaternion.identity);
    }
}
