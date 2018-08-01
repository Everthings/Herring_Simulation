using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerringMovementScript : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 dest;

    bool[] culvertsPassed;
    bool[] sectionsPassed;
    int[] checkPositions;

	// Use this for initialization
	void Start () {
        culvertsPassed = new bool[10];
        sectionsPassed = new bool[10];
        checkPositions = new int[10];

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
            checkPositions[i] = (int)Random.Range(sections[i].GetComponent<TreeShrubGeneratorScript>().lower_bound, sections[i].GetComponent<TreeShrubGeneratorScript>().upper_bound);
        }
    }

    public int[] getCheckPositions()
    {
        return checkPositions;
    }

    public bool[] getSectionsPassed()
    {
        return sectionsPassed;
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
