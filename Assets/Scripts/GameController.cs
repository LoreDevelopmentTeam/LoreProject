using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public ArrayList destroyedObjects = new ArrayList(); 
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

		//kind of hacky. It would be better if destroyedObjects held strings corresponding to artifacts
		//but I'll do that later
		if (level == 3) {
			if (destroyedObjects.Contains(3)) {
				//artifacts will be named differently later I suppose 
				Destroy (GameObject.Find ("artifact"));
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
