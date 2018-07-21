using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblingScript : MonoBehaviour {

    //[SerializeField]
    public GameManager gm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void gamblePressed()
    {
        if (gm.getStreak() >= 1)
        {

        }
        //openGambleMenu()
    }

    private void openGambleMenu()
    {

    }
}
