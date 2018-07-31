using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShrubGeneratorScript : MonoBehaviour
{

    public Terrain river;
    public Terrain river1;
    public Terrain river2;
    public Terrain river3;
    public Terrain river4;
    public Terrain river5;
    public Terrain river6;
    public Terrain river7;
    public Terrain river8;
    public Terrain river9;
    public Terrain river10;

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

    public GameObject shrub;

    List<Vector3> treePositions = new List<Vector3>();
    List<Vector3> shrubPositions = new List<Vector3>();
    List<Vector2> possibleLocations = new List<Vector2>();
    List<TreeData> treePrefabs = new List<TreeData>();

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
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void populatePossibleLocations()
    {
        // set array of all positions on land (increments by 5 cuz 2 trees 1 apart is too much overlap)

        for(int i = (int)right_bound; i < left_bound; i+=5)
        {
            for (int j = (int)lower_bound; j < upper_bound; j += 5)
            {
                if (river.SampleHeight(new Vector3(i, 0, j)) >= 6.9f && river1.SampleHeight(new Vector3(i, 0, j)) >= 6.9f
                    && river2.SampleHeight(new Vector3(i, 0, j)) >= 6.9f && river3.SampleHeight(new Vector3(i, 0, j)) >= 6.9f
                    && river4.SampleHeight(new Vector3(i, 0, j)) >= 6.9f)
                {
                    possibleLocations.Add(new Vector2(i, j));
                } 
            }
        }
    }

    public void incrementTreeAges()
    {
        if(treePrefabs[0].getAge() != 9)
        {
            int length = treePositions.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                swapTreeType(i);
            }

            drawTrees();
        }
    }

    public void addTrees(int num)
    {

        // adds positions to the positions list; age of tree added to prefab array

        for (int i = 0; i < num; i++)
        {
            Vector2 randPos = possibleLocations[(int)(Random.Range(0, 0.999f) * possibleLocations.Count)];
            Vector3 pos = new Vector3(randPos.x, 7, randPos.y);
            treePositions.Add(pos);
            treePrefabs.Add(new TreeData(0, null));

            possibleLocations.Remove(randPos);
        }

        drawTrees();
    }

    public void addShrubs(int num)
    {

        // adds positions to the positions list; age of tree added to prefab array

        for (int i = 0; i < num; i++)
        {
            Vector2 randPos = possibleLocations[(int)(Random.Range(0, 0.999f) * possibleLocations.Count)];
            Vector3 pos = new Vector3(randPos.x, 7, randPos.y);
            shrubPositions.Add(pos);

            possibleLocations.Remove(randPos);
        }

        drawShrubs();
    }

    void addTree(Vector2 newPos, int newAge)
    {
        // adds a new tree in the positions of the old one but with age incremented

        Vector3 pos = new Vector3(newPos.x, 7, newPos.y);
        treePositions.Add(pos);
        treePrefabs.Add(new TreeData(newAge, null));
    }

    public bool areTrees()
    {
        if (treePositions.Count > 0)
            return true;

        return false;
    }

    void swapTreeType(int index)
    {
        // swaps old tree with new tree when changing ages

        addTree(new Vector2(treePositions[index].x, treePositions[index].z), (treePrefabs[index].getAge()+1)%10);
        destroyTree(index);

        treePositions.Remove(treePositions[index]);
        treePrefabs.Remove(treePrefabs[index]);
    }

    void drawTrees(){
        // creates trees in game world and sets them to the prefab array

        for (int i = 0; i < treePositions.Count; i++)
            treePrefabs[i].setTreePrefab(Instantiate(agesInOrder[treePrefabs[i].getAge()], treePositions[i], Quaternion.identity));
    }

    void drawShrubs()
    {
        // creates trees in game world and sets them to the prefab array

        for (int i = 0; i < shrubPositions.Count; i++)
           Instantiate(shrub, shrubPositions[i], Quaternion.identity);
    }

    void destroyTree(int index)
    {
        // destroys trees

        if (treePrefabs[index].getPrefab() != null)
        {
            Destroy(treePrefabs[index].getPrefab().gameObject);
        }
    }

    public List<Vector3> getTreePositions()
    {
        return treePositions;
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
