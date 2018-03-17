using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : Ai, IKnockable
{

    private Rigidbody rigidBody;
    private Pathfinding.AIPath seeker;

    
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        seeker = GetComponent<Pathfinding.AIPath>();
	}

    void ToggleShip(bool toggle)
    {
        MonoBehaviour[] list = gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {

            mb.enabled = toggle;

        }
    }
	
	// Update is called once per frame
	void Update () {
	    switch (state)
	    {
	        case State.Moving:
                Moving();
	            break;
	        case State.Stunned:
                Stunned();
	            break;
	        case State.UnderAttack:
                UnderAttack();
	            break;
        }
	}


    public void RippleHit()
    {

        SetStateStunned(Functions.HitTimer);
        state = State.Stunned;
    }


    protected override void Moving()
    {
    }

    public override void SetStateStunned(float stunTimer)
    {
        Debug.Log("Stunned");
        state = State.Stunned;
        StopCoroutine(UnStunTimer(0f));
        StartCoroutine(UnStunTimer(stunTimer));
        Stunned();
        seeker.enabled = false;
    }
    private IEnumerator UnStunTimer(float stunLength)
    {
        yield return new WaitForSeconds(stunLength);
        state = State.Moving;
        seeker.enabled = true;
    }

    public void Stunned()
    {
        
    }

    public void UnderAttack()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log(rigidBody.velocity != Vector3.zero);
        if (coll.gameObject.layer == Functions.CollisionLayer && rigidBody.velocity != Vector3.zero)
        {
            rigidBody.velocity = Vector3.zero;
            SetStateStunned(rockStunTimer);
            StartCoroutine(HitIntoRock(rockStunTimer));
        }
    }

    private IEnumerator HitIntoRock(float timer)
    {
        Debug.Log("HIT INTO ROCK");
        canHitIntoRock = false;
        yield return new WaitForSeconds(timer + stunCooldownTimer);
        canHitIntoRock = true;

    }
}
