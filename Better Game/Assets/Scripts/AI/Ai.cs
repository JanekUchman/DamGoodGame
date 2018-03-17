using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ai : MonoBehaviour {
    protected State state;

    [SerializeField]
    protected float rockStunTimer = 3.5f;

    [SerializeField]
    protected float stunCooldownTimer =2.0f;

    [SerializeField]
    protected int health = 10;

    protected bool canHitIntoRock = true;
    protected enum State
    {
        Moving,
        Stunned,
        Attacking,
        UnderAttack
    }


    protected abstract void Moving();
    public abstract void SetStateStunned(float stunTimer);

}
