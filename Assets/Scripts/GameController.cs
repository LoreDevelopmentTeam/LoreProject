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
	public float playerX = -11.6f;
	public float playerY = -5.5f;

	public AudioClip clickClip;
	public AudioClip closeClip;
	public AudioClip openClip;
	public AudioClip startClip;
	public AudioClip artifactClip;
	public AudioSource click;
	public AudioSource close;
	public AudioSource open;
	public AudioSource start;
	public AudioSource musicSource;
	public AudioSource artifactSfx;
	public AudioSource artifactTune;

	public AudioClip titleMusic;
	public AudioClip templeMusic;
	public AudioClip bazaarMusic;
	public AudioClip fontMusic;

	bool unpressed = true;
	GameObject p;

	void Start() {
		click = gameObject.AddComponent<AudioSource>();
		click.clip = clickClip;
		close = gameObject.AddComponent<AudioSource>();
		close.clip = closeClip;
		open = gameObject.AddComponent<AudioSource>();
		open.clip = openClip;
		start = gameObject.AddComponent<AudioSource>();
		start.clip = startClip;
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = titleMusic;
		artifactSfx = gameObject.AddComponent<AudioSource>();
		artifactSfx.clip = artifactClip;
		artifactTune = gameObject.AddComponent<AudioSource>();

		musicSource.Play ();
		destroyedObjects = new List<string>();
	}

	void Awake() {

		//enforces singleton design pattern 
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject); 
		}
		//object persists across scenes
		DontDestroyOnLoad (this);
	}

	void OnLevelWasLoaded(int level) {
		GameObject player = GameObject.Find ("player");
		if (player != null) {
			player.transform.position = new Vector3 (playerX, playerY, 0);
		}

		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Artifact");
		foreach (GameObject go in gameObjects) {
			if(destroyedObjects.Contains(go.GetComponent<ArtifactScript>().artifactName)) {
				Destroy (go);
			}
		}

		AudioClip music = null;
		if (musicSource != null) {
			music = musicSource.clip;
		}
		string name = SceneManager.GetActiveScene ().name;
		if (name.Equals ("0_Title")) {
			music = titleMusic;
		} else if (name.Equals ("1Area_Entrance")) {
			music = templeMusic;
		} else if (name.Equals ("2aArea_TempleExterior")) {
			music = templeMusic;
		} else if (name.Equals ("2bArea_TempleInterior")) {
			music = templeMusic;
		} else if (name.Equals ("3Area_SlaveBazaar")) {
			music = bazaarMusic;
		} else if (name.Equals ("4Area_SlaveLiving")) {
			music = bazaarMusic;
		} else if (name.Equals ("5Area_SlaveTemple")) {
			music = bazaarMusic;
		} else if (name.Equals ("6Area_Vista")) {
			//music = bazaarMusic;
		} else if (name.Equals ("7aArea_Library")) {
			//music = bazaarMusic;
		} else if (name.Equals ("7bArea_Library")) {
			//music = bazaarMusic;
		} else if (name.Equals ("8aArea_PalaceExterior")) {
			//music = bazaarMusic;
		} else if (name.Equals ("8bArea_PalaceInterior")) {
			//music = bazaarMusic;
		} else if (name.Equals ("9Area_Font")) {
			music = fontMusic;
		}

		if (musicSource != null && music != musicSource.clip) {
			musicSource.clip = music;
			musicSource.Play ();
		}
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Escape) && !SceneManager.GetActiveScene ().name.Equals("0_Title")) {
			GameObject artifactBox = GameObject.FindGameObjectWithTag ("ArtifactBox");
			if (artifactBox != null) {
				Destroy (artifactBox);
				Time.timeScale = 1;
			} else {
				if (unpressed) {
					if (p == null) {
						open.Play ();
						Time.timeScale = 0;
						p = Instantiate (pauseMenu);
					} else {
						close.Play ();
						Destroy (p);
						Time.timeScale = 1;
					}
				}
				unpressed = false;
			}

		} else {
			unpressed = true;
		}

		if (start.isPlaying || artifactSfx.isPlaying || artifactTune.isPlaying) {
			musicSource.Pause ();
		} else {
			musicSource.UnPause ();
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
