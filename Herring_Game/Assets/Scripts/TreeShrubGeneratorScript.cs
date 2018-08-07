using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShrubGeneratorScript : MonoBehaviour
{

    public Terrain river;

    Terrain currentTerrain;

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
    List<GameObject> shrubPrefabs = new List<GameObject>();

    List<Tree> agesInOrder = new List<Tree>();

    public float upper_bound;
    public float lower_bound;
    public float right_bound;
    public float left_bound;
    public bool river_winding = false;

    // Use this for initialization
    void Start()
    {
        currentTerrain = river;

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

    public float getTreeSurvivalRate()
    {
        float rate;

        if (areTrees())
            rate = SurvivalData.RestoredSurvivalTrees[treePrefabs[0].getAge()];
        else
            rate = SurvivalData.UnrestoredSurvivalTrees;

        return rate;
    }

    public float getShrubSurvivalRate()
    {
        float rate;

        if (areShrubs())
            rate = SurvivalData.RestoredSurvivalShrubs;
        else
            rate = SurvivalData.UnrestoredSurvivalShrubs;

        return rate;
    }

    public float getRiverSurvivalRate()
    {
        float rate;

        if (river_winding)
            rate = SurvivalData.RestoredSurvivalRiver;
        else
            rate = SurvivalData.UnrestoredSurvivalRiver;

        return rate;
    }

    public float treeSurvivalBenefitByAge(int age)
    {
        float baseAge = 0.5f;

        baseAge += age * 0.1f;

        return baseAge;
    }

    public void regenerateAll(Terrain t)
    {
        currentTerrain = t;

        int numTrees = 0;
        int tAge = 0;
        int numShrubs = 0;

        if (areTrees())
        {
            for(int i = 0; i < treePrefabs.Count; i++)
            {
                Destroy(treePrefabs[i].getPrefab().gameObject);
            }

            tAge = treePrefabs[0].getAge();
            numTrees = treePositions.Count;
        }

        if (areShrubs())
        {
            for (int i = 0; i < shrubPrefabs.Count; i++)
            {
                Destroy(shrubPrefabs[i].gameObject);
            }

            numShrubs = shrubPositions.Count;
        }

        treePositions = new List<Vector3>();
        shrubPositions = new List<Vector3>();
        possibleLocations = new List<Vector2>();
        treePrefabs = new List<TreeData>();
        shrubPrefabs = new List<GameObject>();

        populatePossibleLocations();

        addTrees(numTrees, tAge);
        addShrubs(numShrubs);
    }

    void populatePossibleLocations()
    {

        for (float i = right_bound; i < left_bound; i += Random.Range(0.6f, 1.5f))
        {
            for (float j = lower_bound; j < upper_bound; j += Random.Range(0.4f, 1.3f))
            {
                if (currentTerrain.SampleHeight(new Vector3(i, 0, j)) >= 6f && currentTerrain.SampleHeight(new Vector3(i, 0, j)) <= 6.9)
                {
                    possibleLocations.Add(new Vector2(i, j));
                }
            }
        }
        for(float i = right_bound+60; i < left_bound-60; i += Random.Range(2, 4))
        {
            for (float j = lower_bound; j < upper_bound; j += Random.Range(2, 4))
            {
                if (currentTerrain.SampleHeight(new Vector3(i, 0, j)) >= 6.9f)
                {
                    possibleLocations.Add(new Vector2(i, j));
                } 
            }
        }
        for (float i = 240; i < 280; i += Random.Range(5, 10))
        {
            for (float j = lower_bound; j < upper_bound; j += Random.Range(5, 10))
            {
                if (currentTerrain.SampleHeight(new Vector3(i, 0, j)) >= 6.9f)
                {
                    possibleLocations.Add(new Vector2(i, j));
                }
            }
        }
        for (float i = 20; i < 60; i += Random.Range(5, 10))
        {
            for (float j = lower_bound; j < upper_bound; j += Random.Range(5, 10))
            {
                if (currentTerrain.SampleHeight(new Vector3(i, 0, j)) >= 6.9f)
                {
                    possibleLocations.Add(new Vector2(i, j));
                }
            }
        }
        for (float i = right_bound; i < 20; i += Random.Range(8, 20))
        {
            for (float j = lower_bound; j < upper_bound; j += Random.Range(8, 20))
            {
                if (currentTerrain.SampleHeight(new Vector3(i, 0, j)) >= 6.9f)
                {
                    possibleLocations.Add(new Vector2(i, j));
                }
            }
        }
        for (float i = 280; i < left_bound; i += Random.Range(8, 20))
        {
            for (float j = lower_bound; j < upper_bound; j += Random.Range(8, 20))
            {
                if (currentTerrain.SampleHeight(new Vector3(i, 0, j)) >= 6.9f)
                {
                    possibleLocations.Add(new Vector2(i, j));
                }
            }
        }
    }

    public void incrementTreeAges()
    {
        if(treePrefabs.Count != 0 && treePrefabs[0].getAge() != 9)
        {
            int length = treePositions.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                swapTreeType(i);
            }

            drawTrees();
        }
    }

    public void addTrees(int num, int age)
    {

        // adds positions to the positions list; age of tree added to prefab array

        for (int i = 0; i < num; i++)
        {
            Vector2 randPos = possibleLocations[(int)(Random.Range(0, 0.999f) * possibleLocations.Count)];
            Vector3 pos = new Vector3(randPos.x, 7, randPos.y);
            treePositions.Add(pos);
            treePrefabs.Add(new TreeData(age, null));

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

    public bool areShrubs()
    {
        if (shrubPositions.Count > 0)
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
        {
            treePrefabs[i].setTreePrefab(Instantiate(agesInOrder[treePrefabs[i].getAge()], treePositions[i], Quaternion.identity));
            treePrefabs[i].getPrefab().gameObject.isStatic = true;
        }
    }

    void drawShrubs()
    {
        // creates trees in game world and sets them to the prefab array

        for (int i = 0; i < shrubPositions.Count; i++)
        {
            shrubPrefabs.Add(Instantiate(shrub, shrubPositions[i], Quaternion.identity));
            shrubPrefabs[i].gameObject.isStatic = true;
        }
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
