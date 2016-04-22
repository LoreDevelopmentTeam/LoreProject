﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuScript : MonoBehaviour {

	public GameObject gameController; 
	public MovieTexture credits;

	void Start () {
		if (GameController.instance == null) {
			Instantiate (gameController);
		}
	}

	public void newPress()
	{
		SceneManager.LoadScene ("1Area_Entrance");
		gameController.GetComponent<GameController>().getInstance().destroyedObjects = new List<string>();
	}

	public void continuePress()
	{
		if(File.Exists(Application.persistentDataPath + "/artifacts")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/artifacts", FileMode.Open);
			gameController.GetComponent<GameController>().getInstance().destroyedObjects = (List<string>)bf.Deserialize(file);
			file.Close();
		}
		if(File.Exists(Application.persistentDataPath + "/scene")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/scene", FileMode.Open);
			string scene = (string)bf.Deserialize(file);
			file.Close();
			SceneManager.LoadScene (scene);
		}
	}

	public void creditsPress() {
		credits.Stop ();
		credits.Play ();
	}

	public void exitPress() {
		Application.Quit ();
	}

	public void OnGUI() {
		if (credits.isPlaying) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), credits, ScaleMode.StretchToFill);
		}
	}
}
