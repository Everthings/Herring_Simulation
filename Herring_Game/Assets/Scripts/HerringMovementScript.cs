using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerringMovementScript : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 dest;

    bool[] culvertsPassed;
    bool[] sectionsTreePassed;
    int[] checkTreePositions;
    bool[] sectionsShrubPassed;
    int[] checkShrubPositions;
    bool[] sectionsRiverPassed;
    int[] checkRiverPositions;

    // Use this for initialization
    void Start () {
        culvertsPassed = new bool[10];
        sectionsTreePassed = new bool[10];
        checkTreePositions = new int[10];
        sectionsShrubPassed = new bool[10];
        checkShrubPositions = new int[10];
        sectionsRiverPassed = new bool[10];
        checkRiverPositions = new int[10];

        initPositions();
    }

    // Update is called once per frame
    void Update() {
        if (!agent.pathPending)
        {
            transform.Find("Herring").GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        else
        {
            transform.Find("Herring").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }

    void initPositions()
    {
        List<GameObject> sections = GameObject.Find("Sections").GetComponent<SectionCollectionScript>().getSections();

        for(int i = 0; i < 10; i++)
        {
            checkTreePositions[i] = (int)Random.Range(sections[i].GetComponent<TreeShrubGeneratorScript>().lower_bound, sections[i].GetComponent<TreeShrubGeneratorScript>().upper_bound);
            checkShrubPositions[i] = (int)Random.Range(sections[i].GetComponent<TreeShrubGeneratorScript>().lower_bound, sections[i].GetComponent<TreeShrubGeneratorScript>().upper_bound);
            checkRiverPositions[i] = (int)Random.Range(sections[i].GetComponent<TreeShrubGeneratorScript>().lower_bound, sections[i].GetComponent<TreeShrubGeneratorScript>().upper_bound);
        }
    }

    public int[] getCheckTreePositions()
    {
        return checkTreePositions;
    }

    public bool[] getSectionsTreePassed()
    {
        return sectionsTreePassed;
    }

    public int[] getCheckShrubPositions()
    {
        return checkShrubPositions;
    }

    public bool[] getSectionsShrubPassed()
    {
        return sectionsShrubPassed;
    }

    public int[] getCheckRiverPositions()
    {
        return checkRiverPositions;
    }

    public bool[] getSectionsRiverPassed()
    {
        return sectionsRiverPassed;
    }

    public void generatePath()
    {
        agent.SetDestination(dest);
    }

    public Vector3 getDest()
    {
        return dest;
    }

    public bool[] getCulvertsPassed()
    {
        return culvertsPassed;
    }
}
