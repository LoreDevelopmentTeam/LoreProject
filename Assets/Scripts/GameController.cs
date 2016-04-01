﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	//public ArrayList destroyedObjects = new ArrayList(); 
	public Dictionary<string, ArrayList> destroyedObjects = new Dictionary<string, ArrayList>();
	public bool destroyed = false;  

	public static GameController instance = null; 

	void Awake(){

		//enforces singleton design pattern 
		if (instance == null) {
			instance = this; 
		} else if (instance != this) {
			Destroy (gameObject); 
		}
		//object persists across scenes
		DontDestroyOnLoad (this);
	}

	void OnLevelWasLoaded(int level){
		string name = SceneManager.GetActiveScene ().name;
		if (destroyedObjects.ContainsKey (name)) {
			ArrayList objectsToRemove = destroyedObjects [name];
			foreach (object obj in objectsToRemove) {
				Destroy (GameObject.Find((string)obj));
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit ();
		}
	}

}
