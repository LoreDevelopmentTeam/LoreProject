using UnityEngine;
using System.Collections;

public class ArtifactScript : MonoBehaviour {

	public string artifactName;
	public string description;
	public Sprite image;
	public bool removeable = true;
	public GameObject message;

	public void setMessage(bool enabled) {
		message.GetComponent<SpriteRenderer>().enabled = enabled;
	}

}
