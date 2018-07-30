﻿using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {
        //draws trees on mouse click in region

        // left-click
        /*
        if (Input.GetMouseButton(0))
        {

            //Gets mouse position on plance
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);

            if (hit.point.x > right_bound && hit.point.x < left_bound && hit.point.z < upper_bound && hit.point.z > lower_bound)
            {
                
            }
        }
        */
    }

    public void incrementTreeAges()
    {
        int length = positions.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            swapTreeType(i);
        }

        drawTrees();
    }

    void populatePossibleLocations()
    {
        // set array of all positions on land (increments by 5 cuz 2 trees 1 apart is too much overlap)

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

    public void addTrees(int num)
    {

        // adds positions to the positions list; age of tree added to prefab array

        for (int i = 0; i < num; i++)
        {
            Vector2 randPos = possibleLocations[(int)(Random.Range(0, 0.999f) * possibleLocations.Count)];
            Vector3 pos = new Vector3(randPos.x, 7, randPos.y);
            positions.Add(pos);
            prefabs.Add(new TreeData(0, null));

            possibleLocations.Remove(randPos);
        }

        drawTrees();
    }

    void addTree(Vector2 newPos, int newAge)
    {
        // adds a new tree in the positions of the old one but with age incremented

        Vector3 pos = new Vector3(newPos.x, 7, newPos.y);
        positions.Add(pos);
        prefabs.Add(new TreeData(newAge, null));
    }

    public bool areTrees()
    {
        if (positions.Count > 0)
            return true;

        return false;
    }

    void swapTreeType(int index)
    {
        // swaps old tree with new tree when changing ages

        addTree(new Vector2(positions[index].x, positions[index].z), (prefabs[index].getAge()+1)%10);
        destroyTree(index);

        positions.Remove(positions[index]);
        prefabs.Remove(prefabs[index]);
    }

    void drawTrees(){
        // creates trees in game world and sets them to the prefab array

        for (int i = 0; i < positions.Count; i++)
            prefabs[i].setTreePrefab(Instantiate(agesInOrder[prefabs[i].getAge()], positions[i], Quaternion.identity));
    }

    void destroyTree(int index)
    {
        // destroys trees

        if (prefabs[index].getPrefab() != null)
        {
            Destroy(prefabs[index].getPrefab().gameObject);
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
