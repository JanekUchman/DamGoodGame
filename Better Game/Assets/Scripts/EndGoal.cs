using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Test");
        if (coll.GetComponent<ShipAi>())
            coll.GetComponent<ShipAi>().ReachedEnd();
    }
}
