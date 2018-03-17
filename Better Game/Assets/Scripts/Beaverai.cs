using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaverai : Ai, IKnockable
{
    private Rigidbody rigidBody;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void RippleHit()
    {
        StopCoroutine(RemoveForce());
        StartCoroutine(RemoveForce());
        state = State.Stunned;
    }

    private IEnumerator RemoveForce()
    {
        yield return new WaitForSeconds(Functions.HitTimer);
        rigidBody.velocity = Vector3.zero;
    }

    public override void SetStateStunned(float stunTimer)
    {

    }

    protected override void Moving()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
