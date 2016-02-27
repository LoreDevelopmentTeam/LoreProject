using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public GameObject artifactBox;

	float speed = 5.0f;
	float moveX = 0;
	float moveY = 0;
	float acceleration = 0.1f;

	void Start () {
	
	}

	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			//probably not the best place to put this...
			Application.Quit ();
		}
		bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
		bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
		bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
		bool up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
		if(left == right)
		{
			moveX = 0;
		}
		else if (left)
		{
			moveX = Mathf.Max(moveX - acceleration, -1);
		}
		else if (right)
		{
			moveX = Mathf.Min(moveX + acceleration, 1);
		}

		if(down == up)
		{
			moveY = 0;
		}
		else if (down)
		{
			moveY = Mathf.Max(moveY - acceleration, -1);
		}
		else if (up)
		{
			moveY = Mathf.Min(moveY + acceleration, 1);
		}
	}

	void FixedUpdate(){
		GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * speed, moveY * speed);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Teleporter") {
			Application.LoadLevel (coll.gameObject.GetComponent<TeleporterScript> ().scene);
		} else if (coll.gameObject.tag == "Artifact") {
			GameObject box = Instantiate (artifactBox);
			box.transform.Find("img").GetComponent<SpriteRenderer>().sprite = coll.gameObject.GetComponent<ArtifactScript> ().image;
			box.transform.Find("name").GetComponent<TextMesh>().text = coll.gameObject.GetComponent<ArtifactScript> ().name;
			box.transform.Find("description").GetComponent<TextMesh>().text = coll.gameObject.GetComponent<ArtifactScript> ().description;
			if (coll.gameObject.GetComponent<ArtifactScript> ().removeable) {
				Destroy (coll.gameObject);
			}
			//pause the game
			Time.timeScale = 0;
		}
	}
}
