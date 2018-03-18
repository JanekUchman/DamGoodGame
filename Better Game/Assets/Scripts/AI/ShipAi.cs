using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipAi : Ai, IKnockable
{

    private Rigidbody rigidBody;
    private Pathfinding.AIPath seeker;
    private SpriteRenderer explosionSprite;
    public bool reachedEnd = false;

    
	// Use this for initialization
	void Start () {
        reachedEnd = false;
        rigidBody = GetComponent<Rigidbody>();
        seeker = GetComponent<Pathfinding.AIPath>();
        explosionSprite = transform.FindChild("Explosion").gameObject.GetComponent<SpriteRenderer>();
        explosionSprite.enabled = false;
        health = 3;
        SetTargets();
	}

    void OnEnable()
    {
        reachedEnd = false;
        rigidBody = GetComponent<Rigidbody>();
        seeker = GetComponent<Pathfinding.AIPath>();
        explosionSprite = transform.FindChild("Explosion").gameObject.GetComponent<SpriteRenderer>();
        explosionSprite.enabled = false;
        health = 3;
        SetTargets();
    }

    void SetTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Dock");
        int DockNr = Random.Range(0, targets.Length);
        GetComponent<Pathfinding.AIDestinationSetter>().target = targets[DockNr].transform;
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
            TakeDamage(2);
            if (gameObject.activeInHierarchy)
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            StartCoroutine(Death());

    }

    private IEnumerator Death()
    {
        
        explosionSprite.enabled = true;
        transform.FindChild("Explosion").gameObject.GetComponent<AnimationEffects>().ActivateAnimator();
        seeker.enabled = false;
        yield return new WaitForSeconds(0.3f);
        seeker.enabled = true;
        explosionSprite.enabled = true;
        health = 3;

        transform.FindChild("Ship").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ReachedEnd()
    {
        Debug.Log("hit");
        reachedEnd = true;
        health = 100000;
        GameController.instance.score++;
        GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = "x" + GameController.instance.score;
        StartCoroutine(DisableShip());
    }

    private IEnumerator DisableShip()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
