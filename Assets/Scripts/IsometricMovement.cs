using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IsometricMovement : MonoBehaviour {

	public GameObject artifactBox;

	float speed = 5.0f;

	// Use this for initialization
	void Start () {

	
	}
	// Update is called once per frame
	void Update () {

		
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		transform.Translate (Vector2.up * speed * v * Time.deltaTime);
		transform.Translate (Vector2.right * speed * h * Time.deltaTime);
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Teleporter") {
			SceneManager.LoadScene (coll.gameObject.GetComponent<TeleporterScript> ().scene);
		} else if (coll.gameObject.tag == "Artifact") {
			//pause the game

			Time.timeScale = 0;
			GameObject box = Instantiate (artifactBox);
			box.transform.Find("img").GetComponent<SpriteRenderer>().sprite = coll.gameObject.GetComponent<ArtifactScript> ().image;
			box.transform.Find("name").GetComponent<TextMesh>().text = coll.gameObject.GetComponent<ArtifactScript> ().name;
			box.transform.Find("description").GetComponent<TextMesh>().text = coll.gameObject.GetComponent<ArtifactScript> ().description;
			if (coll.gameObject.GetComponent<ArtifactScript> ().removeable) {
				Destroy (coll.gameObject);
				GameObject manager = GameObject.FindWithTag ("GameController");
				//manager.GetComponent<GameController> ().destroyed = true;
				string name = SceneManager.GetActiveScene ().name;
				if (!manager.GetComponent<GameController> ().destroyedObjects.ContainsKey (name)) {
					manager.GetComponent<GameController> ().destroyedObjects.Add (name, new ArrayList ());
				}
				manager.GetComponent<GameController> ().destroyedObjects[name].Add (coll.gameObject.name);
			}

		}
	}
}
