using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverAway : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<Beaverai>())
            coll.GetComponent<Pathfinding.AIPath>().isStopped = true;
    }
}
