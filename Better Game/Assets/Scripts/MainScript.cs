using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    public Sprite pressedSprite;
    Image SplashImage;

	// Use this for initialization
	void Start () {
        SplashImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel(1);
            SplashImage.sprite = pressedSprite;
        }

        
	}
}
