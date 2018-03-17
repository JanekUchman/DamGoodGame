using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject Boat;
    public float SpawnTime = 10.0f;
    public int MaxBoatsToSpawn = 10;

    private GameObject[] spawnPoints;
    private List<GameObject> boatList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
        StartCoroutine(StartSpawnCooldown());
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator StartSpawnCooldown()
    {
        while (true)
        {
            Debug.Log("spawning...");
            // Wait for cooldown...
            yield return new WaitForSeconds(SpawnTime);
            // Spawn the boat
            SpawnBoat();
        }
    }

    void SpawnBoat()
    {
        // Determine a random spawn point
        int randSpawn = Random.Range(0, spawnPoints.Length);
        // Check if we need to instantiate new boats
        if(boatList.Count < MaxBoatsToSpawn)
        {
            // Instantiate new boat object
            GameObject newBoat = Instantiate(Boat, spawnPoints[randSpawn].transform.position, spawnPoints[randSpawn].transform.rotation);
            // Add to list of boat objects
            boatList.Add(newBoat);
        }
        else
        {
            // iterate through all the boats and respawn any that are disabled
            for(int i = 0; i < boatList.Count; i++)
            {
                /* 
                if(BoatList[i].IsDisabled())
                {
                    BoatList[i].transform.position = randSpawn.transform.position;
                    BoatList[i].enbale();
                }
                 */
            }
        }
    }
}
