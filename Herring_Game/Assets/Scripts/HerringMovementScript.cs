﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerringMovementScript : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 dest;

	// Use this for initialization
	void Start () {
       
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

    public void generatePath()
    {
        agent.SetDestination(dest);
    }

    public Vector3 getDest()
    {
        return dest;
    }
}
