using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed = 20.0f;


    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool projectileActive = true;

	// Use this for initialization
	void Start () {
        collider = GetComponent<Collider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (ProjectileSpeed * transform.up * Time.deltaTime);
	}

    public void DisableProjectile()
    {
        gameObject.SetActive(false);
    }

    public void EnableProjectile()
    {
        gameObject.SetActive(true);

        StartCoroutine(ProjectileTimeout());
    }

    private IEnumerator ProjectileTimeout()
    {
        yield return new WaitForSeconds(1.5f);
        DisableProjectile();

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<ShipAi>())
        {
            other.GetComponent<ShipAi>().TakeDamage(1);

            StopCoroutine(ProjectileTimeout());
            DisableProjectile();

        }

        
    }
}
