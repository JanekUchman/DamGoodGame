using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ai : MonoBehaviour {

    protected enum State
    {
        Moving,
        Stunned,
        Attacking,
        UnderAttack
    } 
	
    protected abstract void Moving();

}
