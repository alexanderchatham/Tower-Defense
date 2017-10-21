using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameIsOver = false;

    public GameObject gameOverUI;


	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

		if (PlayerStats.Lives <= 0 && !gameIsOver)
        {
            EndGame();
        }
	}

    void EndGame ()
    {
        Debug.Log("Game Over!");
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

}
