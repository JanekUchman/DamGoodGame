using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaverai : Ai, IKnockable
{
    private Rigidbody rigidBody;
    [SerializeField]
    private Turret turret;
    private Pathfinding.AIPath seeker;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        seeker = GetComponent<Pathfinding.AIPath>();
        StartCoroutine(UpdateTargets());

    }


    private IEnumerator RemoveForce()
    {
        yield return new WaitForSeconds(Functions.HitTimer);

        rigidBody.velocity = Vector3.zero;
    }

   

    protected override void Moving()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void RippleHit()
    {

        SetStateStunned(Functions.HitTimer);
        state = State.Stunned;
    }

    public override void SetStateStunned(float stunTimer)
    {
        if (gameObject.activeInHierarchy)
        {
            Debug.Log("Stunned");
            state = State.Stunned;
            StopCoroutine(UnStunTimer(0f));
            StartCoroutine(UnStunTimer(stunTimer));
            Stunned();
            seeker.enabled = false;
        }
    }
    private IEnumerator UnStunTimer(float stunLength)
    {
        yield return new WaitForSeconds(stunLength);
        state = State.Moving;
        seeker.enabled = true;
        turret.ToggleKnockOut(false);
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
            turret.ToggleKnockOut(true);
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

    IEnumerator UpdateTargets()
    {
        while (true)
        {
            Debug.Log("detected ships");
            yield return new WaitForSeconds(0.5f);
            GameObject[] Ships = GameObject.FindGameObjectsWithTag("Ship");
            float minDistance = Mathf.Infinity;
            if (Ships.Length > 0)
            {
                Debug.Log("detected ships");
                for (int i = 0; i < Ships.Length; i++)
                {
                    
                    if (Ships[i].activeInHierarchy == true && !Ships[i].GetComponent<ShipAi>().reachedEnd)
                    {
                        if (Vector3.Distance(transform.position, Ships[i].transform.position) < minDistance)
                        {
                            GetComponent<Pathfinding.AIDestinationSetter>().target = Ships[i].transform;
                        }
                    }
                }
            }
        }
    }
}
