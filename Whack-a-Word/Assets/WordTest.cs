using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//InvokeRepeating("WordSpew", 0, 1.0f);
	}
	
	void WordSpew() {
        Debug.Log("Letter?: " + GetRandomLetter());
    }

    char GetRandomLetter() {
        char kickback = (char) Random.Range(65,90);
        return kickback;
    }
}
