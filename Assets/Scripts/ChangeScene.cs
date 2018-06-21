using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ChangeScene : MonoBehaviour {

	//public GameManager gm;
	//public AudioSource mapSound;

	public void ChangeToMap(string scene) {
		//Awake ();
		//mapSound.Play ();
		Application.LoadLevel (scene);
 	}

	public void changeToBPGame(string scene){
		//mapSound.Play ();
		Application.LoadLevel (scene);
		//setGoal(goal);
	}
}
