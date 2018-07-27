﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerringMovementScript : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 dest;

	// Use this for initialization
	void Start () {
        agent.SetDestination(dest);
        //GameObject.Find("Herring").GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if(transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }

        if (!agent.pathPending)
            transform.Find("Herring").GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}
