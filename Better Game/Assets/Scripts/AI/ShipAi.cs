using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : Ai, IKnockable
{

    private Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(rigidBody.velocity);
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
        StartCoroutine(RemoveForce());
        state = State.Stunned;
    }

    private IEnumerator RemoveForce()
    {
        yield return new WaitForSeconds(Functions.HitTimer);

        rigidBody.velocity = Vector3.zero;
    }

    protected override void Moving()
    {
    }

    

    public void Stunned()
    {
        
    }

    public void UnderAttack()
    {
        
    }
}
