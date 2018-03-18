using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffects : MonoBehaviour {

    public List<Sprite> AnimationSprites;
    public float FrameTime;
    public bool shouldExpire = false;

    private SpriteRenderer renderer;
    private int curFrame = 0;
    private float Timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        Debug.Log("starting game!");
        renderer = GetComponent<SpriteRenderer>();
        curFrame = 0;
        renderer.sprite = AnimationSprites[curFrame];
        // StartCoroutine(LoadNextFrame());
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= FrameTime)
        {
            curFrame++;
            if (curFrame == AnimationSprites.Count)
            {
                curFrame = 0;
                if (shouldExpire)
                {
                    renderer.enabled = false;
                }
            }
            renderer.sprite = AnimationSprites[curFrame];
            Timer = 0;
        }
    }

    public void ActivateAnimator()
    {
        renderer.sprite = AnimationSprites[0];
        curFrame = 0;
        renderer.enabled = true;

    }
}
