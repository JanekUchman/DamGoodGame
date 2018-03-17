﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float DetectionDistance = 10.0f;
    public float RotationRate = 10.0f;
    public float FiringRate = 2.0f;
    public float MinFiringAngle = 10.0f;
    public float KnockOutTime = 2.0f;
    public LayerMask DetectionLayer;
    public GameObject Projectile;
    public Transform ProjectileSpawn;
    public int MaxInstantiatedProjectiles = 8;

    public List<GameObject> InstantiatedProjectiles = new List<GameObject>();
    private bool knockedOut = false;
    private float fireTimer = 0.0f;
    private float knockOutTimer = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Check if our turrets has been knocked out - otherwise, detect targets
        if (!knockedOut)
        {
            DetectTarget();
        }
        else
        {
            if (knockOutTimer > 0)
            {
                // Reset knock out with timer
                knockOutTimer -= Time.deltaTime;
            }
            else
            {
                knockedOut = false;
            }
        }
	}

    void DetectTarget()
    {
        // Create circle collider that finds all the objects within the radius
        Collider2D[] DetectedColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), DetectionDistance, DetectionLayer, -Mathf.Infinity, Mathf.Infinity);
        // Check if the collider has detected a ship that is nearby
        Debug.Log(DetectedColliders.Length);
        if (DetectedColliders.Length > 0)
        {
            // Create variable to determine what ship is closest to the turret
            float closestDistance = Mathf.Infinity;
            for (int i = 0; i < DetectedColliders.Length; i++)
            {
				// Determine the distance between the collider and the turret
				float curDistance = Vector2.Distance(transform.position, DetectedColliders[i].transform.position);
                // Check if the current collider is closer to the previous
                if(curDistance < closestDistance)
                {
                    // Set this to be the closest distance
                    closestDistance = curDistance;
                    // Create vector that determines the new direction our object wants to be facing
                    Vector2 lookDirection = (DetectedColliders[i].transform.position - transform.position).normalized;
                    // Get the angle that we need to rotate to face the object
                    float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                    // Convert that to quaternion for rotation
                    Quaternion lookRotation = Quaternion.AngleAxis(lookAngle, transform.forward);
                    // Rotate the object over time
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * RotationRate);
                    // Check if the object is facing the target
                    float compAngle = Quaternion.Angle(transform.rotation, lookRotation);
                    // Check that the absolute value between the target rotation 
                    if(Mathf.Abs(compAngle) < MinFiringAngle)
                    {
                        // Increment firing timer
                        fireTimer += Time.deltaTime;
                        if(fireTimer > FiringRate)
                        {
                            // Fire projectile - if there are still projectiles we need to instantiate...
                            if(InstantiatedProjectiles.Count < MaxInstantiatedProjectiles)
                            {
                                GameObject newProjectile = Instantiate(Projectile, ProjectileSpawn.position, ProjectileSpawn.rotation);
                                newProjectile.GetComponent<Projectile>().EnableProjectile();
                                // Add new projectile to the list of instantiated projectiles
                                InstantiatedProjectiles.Add(newProjectile);
                            }
                            else
                            {
                                ResetProjectile();
                            }
                            // Reset timer
                            fireTimer = 0.0f;
                        }
                    }
                }
            }
        }
    }

    void ResetProjectile()
    {
        // Variables used to store comparison
        float furthestDistance = -Mathf.Infinity;
        int furthestDistanceObjID = 0;
        for(int i = 0; i < InstantiatedProjectiles.Count; i++)
        {
            // Find the projectile furthest away from the player
            float curDistance = Vector2.Distance(InstantiatedProjectiles[i].transform.position, transform.position);
            if(curDistance > furthestDistance)
            {
                // this is the furthest distance object right now
                furthestDistance = curDistance;
                furthestDistanceObjID = i;
            }
        }
        // Set the transform to be that of the turret
        InstantiatedProjectiles[furthestDistanceObjID].transform.position = ProjectileSpawn.position;
        InstantiatedProjectiles[furthestDistanceObjID].transform.rotation = ProjectileSpawn.rotation;
    }

    public void ToggleKnockOut()
    {
        if(!knockedOut)
        {
            knockedOut = true;
        }
    }
}
