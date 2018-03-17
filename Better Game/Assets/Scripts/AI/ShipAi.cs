using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : Ai, IKnockable
{

    private State state;

	// Use this for initialization
	void Start () {
		
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

    protected override void Moving()
    {
    }

    public void RippleHit()
    {
        
    }

    public void Stunned()
    {
        
    }

    public void UnderAttack()
    {
        
    }
}
