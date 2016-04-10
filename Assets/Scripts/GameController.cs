using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

	public GameObject pauseMenu;
	public List<string> destroyedObjects;
	public static GameController instance = null;

	bool unpressed = true;
	GameObject p;

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
		foreach (string s in destroyedObjects) {
			GameObject go = GameObject.Find (s);
			if (go != null) {
				Destroy (go);
			}
		}
	}

	void Start () {
		destroyedObjects = new List<string>();
	}
	
	void Update () {
		foreach(string s in instance.destroyedObjects) {
			print(s);
		}
		if (Input.GetKey (KeyCode.Escape) && !SceneManager.GetActiveScene ().name.Equals("Title")) {
			if (unpressed) {
				if (p == null) {
					Time.timeScale = 0;
					p = Instantiate (pauseMenu);
				} else {
					Destroy (p);
					Time.timeScale = 1;
				}
			}
			unpressed = false;

		} else {
			unpressed = true;
		}
	}

	public void save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/artifacts");
		bf.Serialize(file, instance.destroyedObjects);
		file.Close();
		bf = new BinaryFormatter();
		file = File.Create (Application.persistentDataPath + "/scene");
		bf.Serialize(file, SceneManager.GetActiveScene ().name);
		file.Close();
	}

	public GameController getInstance() {
		return instance;
	}
}
