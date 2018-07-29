using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneratorScript : MonoBehaviour
{

    public Terrain river;
    public Tree age1;
    public Tree age2;
    public Tree age3;
    public Tree age4;
    public Tree age5;
    public Tree age6;
    public Tree age7;
    public Tree age8;
    public Tree age9;
    public Tree age10;

    List<Vector3> positions = new List<Vector3>();
    List<Vector2> possibleLocations = new List<Vector2>();
    List<TreeData> prefabs = new List<TreeData>();

    List<Tree> agesInOrder = new List<Tree>();

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
        agesInOrder.Add(age1);
        agesInOrder.Add(age2);
        agesInOrder.Add(age3);
        agesInOrder.Add(age4);
        agesInOrder.Add(age5);
        agesInOrder.Add(age6);
        agesInOrder.Add(age7);
        agesInOrder.Add(age8);
        agesInOrder.Add(age9);
        agesInOrder.Add(age10);

        populatePossibleLocations();
        addTrees(5);
        drawTrees();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            int length = positions.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                swapTreeType(i);
            }
                

            drawTrees();
        }
    }

    void populatePossibleLocations()
    {
        for(int i = (int)right_bound; i < left_bound; i+=5)
        {
            for (int j = (int)lower_bound; j < upper_bound; j += 5)
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
            prefabs.Add(new TreeData(0, null));

            possibleLocations.Remove(randPos);
        }
    }

    void addTree(Vector2 newPos, int newAge)
    {
        Vector3 pos = new Vector3(newPos.x, 7, newPos.y);
        positions.Add(pos);
        prefabs.Add(new TreeData(newAge, null));
    }

    void swapTreeType(int index)
    {
        addTree(new Vector2(positions[index].x, positions[index].z), (prefabs[index].getAge()+1)%10);
        destroyTree(index);

        positions.Remove(positions[index]);
        prefabs.Remove(prefabs[index]);
    }

    void drawTrees(){
        for (int i = 0; i < positions.Count; i++)
            prefabs[i].setTreePrefab(Instantiate(agesInOrder[prefabs[i].getAge()], positions[i], Quaternion.identity));
    }

    void destroyTree(int index)
    {
        if (prefabs[index].getPrefab() != null)
        {
            Destroy(prefabs[index].getPrefab());
        }
    }

    public List<Vector3> getTreePositions()
    {
        return positions;
    }
}

class TreeData
{

    int age;
    Tree prefab;

    public TreeData(int age, Tree prefab)
    {
        this.age = age;
        this.prefab = prefab;
    }

    public int getAge()
    {
        return age;
    }

    public Tree getPrefab()
    {
        return prefab;
    }

    public void setTreePrefab(Tree t)
    {
        this.prefab = t;
    }
}
