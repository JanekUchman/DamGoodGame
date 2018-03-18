using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public int score = 0;
    public GameObject splash;
    public GameObject splashText;
	// Use this for initialization
	void Start ()
    {
        instance = this;

        StartCoroutine(GameTimer());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(5);
        splash.SetActive(true);
        splashText.SetActive(true);
        splashText.GetComponent<Text>().text = "You saved a total of " + score + " squirrels on jet skis";
        yield return new WaitForSeconds(5);
        Application.LoadLevel(1);
    }
	

}
