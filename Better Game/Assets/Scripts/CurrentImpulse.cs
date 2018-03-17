using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentImpulse : MonoBehaviour {

    public float ImpulseRadius = 10.0f;
    public float ImpulseForce = 10.0f;
    public float ResetTime = 3.0f;
    public LayerMask ForceLayer;
    private bool onCooldown = false;
    public float CooldownTime = 2.0f;

	// Use this for initialization
	void Start () {
        onCooldown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !onCooldown)
        {
            // Update position
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Apply new impulse
            TriggerImpulse();
            StartCoroutine(SetCooldown());
        }
    }

    IEnumerator SetCooldown()
    {
        onCooldown = true;
        // Wait for cooldown...
        yield return new WaitForSeconds(CooldownTime);
        // ...reset the cooldown
        onCooldown = false;
    }


    void TriggerImpulse()
    {
        // Create circle collider that interacts with all objects near the impulse
        Collider[] ImpulseCollisions = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 0), ImpulseRadius, ForceLayer);

        for (int i = 0; i < ImpulseCollisions.Length; i++)
        {

            // Janek doing weird "abstract" shit
            MonoBehaviour[] list = ImpulseCollisions[i].gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is IKnockable)
                {
                    var knockable = (IKnockable)mb;
                    knockable.RippleHit();
                    Debug.Log("Ripple hit");
                }
            }

            // Disable any projectiles hit by the impulse
            if (ImpulseCollisions[i].GetComponent<Projectile>())
            {
                ImpulseCollisions[i].GetComponent<Projectile>().DisableProjectile();
            }
            // Check if the object has a rigidbody attached to it - needs force applied to it
            if (ImpulseCollisions[i].GetComponent<Rigidbody>())
            {
                // Direction that the force should be applied in = position of force - position of the object
                Vector2 ForceVector = (ImpulseCollisions[i].transform.position - transform.position);
                // Get distance between the impulse and the object that it is applying force to
                float Distance = ForceVector.magnitude;
                // Divide the force vector by the distance to get values between 1 and 0 (therefore meaning that everytime force is applied, it will be the same for all objects)
                ForceVector /= Distance;
                // Multiply by the impulse force
                ForceVector *= ImpulseForce;
                // Set velocity of the object being effected by the current, to get "arcade" physics feel
                ImpulseCollisions[i].GetComponent<Rigidbody>().velocity = ForceVector;
            }
        }
    }
}
