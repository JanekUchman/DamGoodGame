using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed = 20.0f;

    private SpriteRenderer spriteRenderer;
    private bool projectileActive = true;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (ProjectileSpeed * transform.up * Time.deltaTime);
	}

    public void DisableProjectile()
    {
        if (projectileActive == true)
        {
            // Disable sprite renderer
            spriteRenderer.enabled = false;
            projectileActive = false;
        }
    }

    public void EnableProjectile()
    {
        if (projectileActive == false)
        {
            // Enable sprite renderer
            spriteRenderer.enabled = true;
            projectileActive = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
