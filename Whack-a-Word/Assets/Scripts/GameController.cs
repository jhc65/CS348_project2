using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    #region Variables and Declarations
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject inGame;
    [SerializeField] Text currentScoreCounter;
    [SerializeField] Text hiScoreCounter;
    [SerializeField] Text endingHiScoreCounter;
    [SerializeField] GameObject[] moles;
    [SerializeField] Text wordDisplay;

    private int currentScore;
    private int hiScore;
    private string[] wordList;
    private int currentWord = 0;
    private int currentPosInWord = 0;
    private bool isReadyForNewWord = true;

    private static GameController instance = null;

    #region Getters and Setters
    public static GameController Instance {
        get { return instance; }
    }
    #endregion
    #endregion

    #region GameController Functions
    void GameOver()
    {
        //Set up game score and the like here.
        inGame.SetActive(false);
        endMenu.SetActive(true);
    }

    private void HideDisplayedWord() {
        isReadyForNewWord = false;
        wordDisplay.text = "";
    }

    private void ShowAndHideMole() {
        // loop thru all moles and give them a random letter
        for (int i = 0; i < moles.Length; i++) {
            moles[i].GetComponent<Mole>().SetText(Constants.Functions.RandomLetter());
        }
    }

    public void ReceiveLetter(string letterIn) {
        if (wordList[currentWord][currentPosInWord] == Convert.ToChar(letterIn)) {
            wordDisplay.text = (wordDisplay.text + Convert.ToString(letterIn));
            currentPosInWord++;
            if (wordDisplay.text == wordList[currentWord]) {
                currentWord++;
                currentPosInWord = 0;
                isReadyForNewWord = true;
                Invoke("HideDisplayedWord", 4f);
                for (int i = 0; i < moles.Length; i++) {
                    moles[i].GetComponent<Mole>().SetText("");
                }
            }
        }
        else {
            GameOver();
        }
    }
    #endregion

    #region GameController UI Methods
    //Everything here is called by buttons on the UI. Add to here if needed.
    public void StartGame()
    {
        mainMenu.SetActive(false);
        // Set Mole Colliders to Active
        for (int i = 0; i < moles.Length; i++) {
            moles[i].GetComponent<BoxCollider>().enabled = true;
        }
        InvokeRepeating("ShowAndHideMole", 0f, 1.5f/*Constants.Functions.RandomNumber(1, 3)*/);
        Invoke("HideDisplayedWord", 4f);
        inGame.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        endMenu.SetActive(false);
        //Reset game state here.
        inGame.SetActive(true);
    }
    #endregion

    #region Unity Overrides
    // Awake
    private void Awake()
    {
        if (!instance) {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        //Ensure MainMenu is up first when we run.
        mainMenu.SetActive(true);
        endMenu.SetActive(false);
        inGame.SetActive(false);
        currentScore = 0;
        hiScore = 0; // Might need to read from a file or PlayerPreferences here.
        wordList = (string[])Constants.Functions.ShuffleStringArray(Constants.Words.wordListOne).Clone();

        //InvokeRepeating("ShowAndHideMole", 0f, 1.5f/*Constants.Functions.RandomNumber(1, 3)*/);
        //Invoke("HideDisplayedWord", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame.activeSelf) {
            if (isReadyForNewWord) {
                if (currentWord == wordList.Length) {
                    // end the game, you fucking won (for now just restart)
                    currentWord = 0;
                }
                else {
                    wordDisplay.text = wordList[currentWord];
                    //Invoke("HideDisplayedWord", 4f);
                }
            }
            else {
                // idk it was being really weird
            }
        }
    }
    #endregion

}
