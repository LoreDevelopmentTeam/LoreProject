using UnityEngine;
using System.Collections;

public class ArtifactBoxScript : MonoBehaviour {

	public string name;
	public string description;
	public Sprite image;

	bool unpressed;

	void Start () {
		unpressed = false;
	}

	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			if (unpressed) {
				Time.timeScale = 1;
				Destroy (gameObject);
			}
		} else {
			unpressed = true;
		}
	}

}
