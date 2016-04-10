using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuScript : MonoBehaviour {

	public GameObject gameController; 

	void Start () {
		if (GameController.instance == null) {
			Instantiate (gameController);
		}
	}

	public void newPress()
	{
		SceneManager.LoadScene ("temple");
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
		//play credits movie
		print("credits");
	}

	public void exitPress() {
		Application.Quit ();
	}
}
