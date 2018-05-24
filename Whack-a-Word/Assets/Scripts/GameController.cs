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
    [SerializeField] Text correctLetterDisplay;
    [SerializeField] Text timerDisplay;

    private int currentScore;
    private int hiScore;
    private string[] wordList;
    private int currentWord = 0;
    private int currentPosInWord = 0;
    private bool isReadyForNewWord = true;
    private int timer;

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
        CancelInvoke();

        //Display high scores.
        //Won't save over multiple sessions.
        currentScoreCounter.text = currentScore + "";
        if (currentScore > hiScore) {
            hiScore = currentScore;
            hiScoreCounter.text = endingHiScoreCounter.text = hiScore + "";
        }
        inGame.SetActive(false);
        endMenu.SetActive(true);
    }

    private void HideDisplayedWord() {
        isReadyForNewWord = false;
        wordDisplay.text = "";
    }

    private void ShowAndHideMole() {
        //animation will go here? (lerp)
        InjectCorrectLetter();
        //animation makes it pop back up.
    }

    private void InjectCorrectLetter() {
        char[] letters = new char[moles.Length];
        //add correct letter to the list.
        letters[0] = wordList[currentWord][currentPosInWord];

        //add dummy letters (and no dupes of the correct letter)
        for (int i = 1; i < letters.Length; i++) {
            bool ok = false;
            while (!ok) {
                char toAdd = Convert.ToChar(Constants.Functions.RandomLetter());
                if (toAdd != wordList[currentWord][currentPosInWord]) {
                    letters[i] = toAdd;
                    ok = true;
                }
            }
        }

        //shuffle...
        for (int i = 0; i < letters.Length; i++) {
            char tmp1 = letters[i];
            int j = UnityEngine.Random.Range(i, letters.Length);
            letters[i] = letters[j];
            letters[j] = tmp1;
        }

        //...and display
        for (int i = 0; i < moles.Length; i++) {
            moles[i].GetComponent<Mole>().SetText(letters[i]+"");
        }
    }

    public void ReceiveLetter(string letterIn) {
        if (wordList[currentWord][currentPosInWord] == Convert.ToChar(letterIn)) {
            correctLetterDisplay.text = (correctLetterDisplay.text + Convert.ToString(letterIn));
            currentPosInWord++;
            if (correctLetterDisplay.text == wordList[currentWord]) {
                currentWord++;
                currentPosInWord = 0;
                isReadyForNewWord = true;

                currentScore++;
                currentScoreCounter.text = currentScore + "";
                for (int i = 0; i < moles.Length; i++) {
                    moles[i].GetComponent<Mole>().SetText("");
                }
            }
        }
        else {
            GameOver();
        }
    }

    public void TimerTick() {
        timer--;
        timerDisplay.text = timer + "";
        if (timer <= 0) {
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
        InvokeRepeating("ShowAndHideMole", 0f, 2.5f/*Constants.Functions.RandomNumber(1, 3)*/);
        //Invoke("HideDisplayedWord", 4f);
        inGame.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        endMenu.SetActive(false);

        timer = 60;
        currentScore = 0;
        currentScoreCounter.text = "0";
        timerDisplay.text = "60";

        currentWord = 0;
        currentPosInWord = 0;
        correctLetterDisplay.text = "";
        wordList = (string[])Constants.Functions.ShuffleStringArray(Constants.Words.wordListOne).Clone();
        isReadyForNewWord = true;

        inGame.SetActive(true);
        InvokeRepeating("TimerTick", 0f, 1f);
        InvokeRepeating("ShowAndHideMole", 0f, 2.5f/*Constants.Functions.RandomNumber(1, 3)*/);
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
        timer = 60;
        wordList = (string[])Constants.Functions.ShuffleStringArray(Constants.Words.wordListOne).Clone();
        InvokeRepeating("TimerTick", 0f, 1f);

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
                    correctLetterDisplay.text = "";
                    isReadyForNewWord = false;
                }
            }
            else {
                // idk it was being really weird
            }
        }
    }
    #endregion

}
