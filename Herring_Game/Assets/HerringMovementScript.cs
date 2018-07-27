using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerringMovementScript : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 dest;

	// Use this for initialization
	void Start () {
        agent.SetDestination(dest);
    }

    // Update is called once per frame
    void Update() {

	}
}
