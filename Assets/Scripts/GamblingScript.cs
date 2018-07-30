using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamblingScript : MonoBehaviour {

    //[SerializeField]
    public GameManager gm;

    public GameObject gambleMenu;
    public GameObject gambleWarning;

    public Text betTxt;
    public Text gameSummaryTxt;
    public Button increaseBetBtn;
    public Button decreaseBetBtn;
    public Button placeBetBtn;

    public int bet;

    // Use this for initialization
    void Start()
    {
        bet = 100;
    }
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        betTxt.text = "$" + bet.ToString();
        gameSummaryTxt.text = "In this game, the higher your streak, the more you make.  Your current bet will earn you the following prizes: \n" +
            "0 Streak: $"+ (bet*0) + "\n" +
            "1 Streak: $" + (bet * 0.5) + "\n" +
            "2 Streak: $" + (bet * 1) + "\n" +
            "3 Streak: $" + (bet * 1.5) + "\n" +
            "4 Streak: $" + (bet * 2) + "\n" +
            "5 Streak: $" + (bet * 2.5) + "\n" +
            "etc.";
		
	}

    public void gamblePressed()
    {
        if (gm.getStreak() >= 1) {
            gambleWarning.SetActive(true);
        } else {
            openGambleMenu();
        }
    }

    public void openGambleMenu()
    {
        gambleMenu.SetActive(!gambleMenu.activeSelf);
        if (gambleWarning.activeSelf)
        {
            gambleWarning.SetActive(!gambleWarning.activeSelf);
        }
    }

    public void openCloseWarning()
    {
        gambleWarning.SetActive(!gambleWarning.activeSelf);
    }

    private void increaseBet(int increment)
    {
        bet += increment;
    }

    private void decreaseBet(int increment)
    {
        if (bet - increment > 0)
        {
            bet -= increment;
        }
    }

    public void increasePressed()
    {
        increaseBet(100);
    }

    public void decreasePressed()
    {
        decreaseBet(100);
    }

    private void startGamblingGame(int bet)
    {
        gm.startGamblingGame(bet);
    }

    public void placeBetPressed()
    {
        openGambleMenu();
        startGamblingGame(bet);
    }
}
