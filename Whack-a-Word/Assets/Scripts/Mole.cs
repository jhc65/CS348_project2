using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mole : MonoBehaviour
{
    #region Variables and Delcarations
    [SerializeField] private Text displayText;

    private GameController gc;
    #endregion

    #region Mole Functions
    public void SetText(string letterIn)
    {
        displayText.text = letterIn;
    }

    public string GetText() {
        return displayText.text;
    }
    #endregion

    #region Unity Overrides
    private void Awake() {
        displayText.text = Constants.Functions.RandomLetter();
    }

    // Use this for initialization
    void Start() {
        gc = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnMouseOver()
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            gc.ReceiveLetter(displayText.text);
        }
    }
    #endregion
}
