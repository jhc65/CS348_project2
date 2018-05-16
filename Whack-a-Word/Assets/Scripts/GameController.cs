using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    #region Variables and Declarations
    [SerializeField]
    private GameObject[] moles;
    [SerializeField]
    private Text[] moleTextBox;
    [SerializeField]
    private Text[] textBoxes;
    private Text currentTextBox;
    private int currentTextBoxIndex;

    private static GameController instance = null;

    #region Getters and Setters
    public static GameController Instance {
        get { return instance; }
    }
    #endregion
    #endregion

    #region GameController Functions
    private void HideDisplayedWord()
    {
        foreach (Text textBox in textBoxes) {
            textBox.gameObject.SetActive(false);
        }
    }

    IEnumerator ShowAndHideMole() {
        //yield return new WaitForSeconds(4);
        while (true) {
            int moleToShow = 0;
            do {
                moleToShow = Constants.Functions.RandomNumber(0, (moles.Length - 1));
            }
            while (moles[moleToShow].transform.position.y == 9.2);

            Vector3 currPos = moles[moleToShow].transform.position;

            // First set the letter, then turn it on
            string charForMole = Constants.Functions.RandomLetter();
            moleTextBox[moleToShow].text = charForMole;
            moleTextBox[moleToShow].gameObject.SetActive(true);

            // Then move the mole
            moles[moleToShow].transform.position = new Vector3(currPos.x, 9.2f, currPos.z);

            // Wait for 2 seconds, then hide this shit
            yield return new WaitForSeconds(2);

            moles[moleToShow].transform.position = new Vector3(currPos.x, -6f, currPos.z);

            moleTextBox[moleToShow].gameObject.SetActive(false);
        }
    }

    IEnumerator StartMultipleCoroutines() {
        IEnumerator coroutine = ShowAndHideMole();
        yield return new WaitForSeconds(4);
        int seconds = 0;

        StartCoroutine(coroutine);
        seconds = Constants.Functions.RandomNumber(1, 4);
        yield return new WaitForSeconds(seconds);
        StartCoroutine(coroutine);
        seconds = Constants.Functions.RandomNumber(1, 4);
        yield return new WaitForSeconds(seconds);
        StartCoroutine(coroutine);

        yield return null;
    }
    #endregion

    #region Unity Overrides
    // Awake shit
    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        Invoke("HideDisplayedWord", 4f);
        //InvokeRepeating("ShowAndHideMole", Constants.Functions.RandomNumber(1, 4), 4f);
        IEnumerator coroutine = StartMultipleCoroutines();
        StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion
}
