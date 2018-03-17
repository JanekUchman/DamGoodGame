using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AStarObject : MonoBehaviour {
    [SerializeField] private float timeBetweenScans = 0.1f;
	
    private Collider coll;
	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(ScanTimer());
	    coll = gameObject.GetComponent<Collider>();
	}

    private IEnumerator ScanTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenScans);
            AstarPath.active.UpdateGraphs(coll.bounds);
        }
    }
}
