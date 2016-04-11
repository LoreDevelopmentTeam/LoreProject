using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class IsometricMovement : MonoBehaviour {

	public GameObject artifactBox;

	float speed = 6.5f;

	void Start () {
		
	}

	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		transform.Translate (Vector2.up * speed * v * Time.deltaTime);
		transform.Translate (Vector2.right * speed * h * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Teleporter") {
			GameObject manager = GameObject.FindWithTag ("GameController");
			manager.GetComponent<GameController> ().playerX = coll.gameObject.GetComponent<TeleporterScript> ().x;
			manager.GetComponent<GameController> ().playerY = coll.gameObject.GetComponent<TeleporterScript> ().y;
			SceneManager.LoadScene (coll.gameObject.GetComponent<TeleporterScript> ().scene);
		}
		else if (coll.gameObject.tag == "Artifact") {
			((ArtifactScript)coll.gameObject.GetComponent<ArtifactScript> ()).setMessage (true);
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Artifact") {
			((ArtifactScript)coll.gameObject.GetComponent<ArtifactScript> ()).setMessage (false);
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Artifact" && Input.GetKey (KeyCode.F)) {
			//pause the game
			Time.timeScale = 0;
			GameObject box = Instantiate (artifactBox);
			box.transform.Find ("img").GetComponent<SpriteRenderer> ().sprite = coll.gameObject.GetComponent<ArtifactScript> ().image;
			box.transform.Find ("name").GetComponent<TextMesh> ().text = coll.gameObject.GetComponent<ArtifactScript> ().artifactName;
			box.transform.Find ("description").GetComponent<TextMesh> ().text = coll.gameObject.GetComponent<ArtifactScript> ().description;
			if (coll.gameObject.GetComponent<ArtifactScript> ().removeable) {
				GameObject manager = GameObject.FindWithTag ("GameController");
				manager.GetComponent<GameController> ().destroyedObjects.Add (coll.gameObject.name);
				manager.GetComponent<GameController> ().save ();
				Destroy (coll.gameObject);
			}
		}
	}
}
