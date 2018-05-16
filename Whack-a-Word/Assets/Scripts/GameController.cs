using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject inGame;

    [SerializeField] Text currentScoreCounter;
    [SerializeField] Text hiScoreCounter;
    [SerializeField] Text endingHiScoreCounter;

    private int currentScore;
    private int hiScore;

	// Use this for initialization
	void Start () {
        //Ensure MainMenu is up first when we run.
		mainMenu.SetActive(true);
        endMenu.SetActive(false);
        inGame.SetActive(false);
        currentScore = 0;
        hiScore = 0; // Might need to read from a file or PlayerPreferences here.
	}
	
	// Update is called once per frame
	void Update () {
		if (inGame.activeSelf) {
            // Do game logic stuff if needed.
        }
	}

    void GameOver() {
        //Set up game score and the like here.
        inGame.SetActive(false);
        endMenu.SetActive(true);
    }

    #region UI Methods
    //Everything here is called by buttons on the UI. Add to here if needed.
    public void StartGame() {
        mainMenu.SetActive(false);
        inGame.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void RestartGame() {
        endMenu.SetActive(false);
        //Reset game state here.
        inGame.SetActive(true);
    }
    #endregion

}
