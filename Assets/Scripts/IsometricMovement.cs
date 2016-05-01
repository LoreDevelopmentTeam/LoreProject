using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class IsometricMovement : MonoBehaviour {

	public GameObject artifactBox;

	float speed = 1.5f;
	int animTime;

	public GameObject message;

	public Sprite lu_stand;
	public Sprite lu_walk1;
	public Sprite lu_walk2;
	public Sprite ru_stand;
	public Sprite ru_walk1;
	public Sprite ru_walk2;
	public Sprite ld_stand;
	public Sprite ld_walk1;
	public Sprite ld_walk2;
	public Sprite rd_stand;
	public Sprite rd_walk1;
	public Sprite rd_walk2;

	bool leftFacing = false;
	bool upFacing = false;
	bool step = true;
	bool fUnpressed = true;

	void Start () {
		animTime = 0;
	}

	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if (h == 0) {
			transform.Translate (Vector2.up * speed * v * Time.deltaTime);
		} else {
			transform.Translate (Vector2.up * speed * v * Time.deltaTime * 0.5f);
		}
		transform.Translate (Vector2.right * speed * h * Time.deltaTime);
	}

	void Update () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		//ANIMATION STUFF
		bool moving = false;
		if (h != 0) {
			leftFacing = (h < 0);
			moving = true;
		}
		if (v != 0) {
			upFacing = (v > 0);
			moving = true;
		}

		if (moving) {
			animTime++;
			if (animTime >= 20) {
				step = !step;
				animTime = 0;
			}
		} else {
			animTime = 0;
		}

		if (leftFacing && upFacing) {
			if (moving) {
				if (step) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = lu_walk1;
				} else {
					gameObject.GetComponent<SpriteRenderer> ().sprite = lu_walk2;
				}
			} else {
				gameObject.GetComponent<SpriteRenderer>().sprite = lu_stand;
			}
		}
		if (leftFacing && !upFacing) {
			if (moving) {
				if (step) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = ld_walk1;
				} else {
					gameObject.GetComponent<SpriteRenderer> ().sprite = ld_walk2;
				}
			} else {
				gameObject.GetComponent<SpriteRenderer>().sprite = ld_stand;
			}
		}
		if (!leftFacing && upFacing) {
			if (moving) {
				if (step) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = ru_walk1;
				} else {
					gameObject.GetComponent<SpriteRenderer> ().sprite = ru_walk2;
				}
			} else {
				gameObject.GetComponent<SpriteRenderer>().sprite = ru_stand;
			}
		}
		if (!leftFacing && !upFacing) {
			if (moving) {
				if (step) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = rd_walk1;
				} else {
					gameObject.GetComponent<SpriteRenderer> ().sprite = rd_walk2;
				}
			} else {
				gameObject.GetComponent<SpriteRenderer>().sprite = rd_stand;
			}
		}
		//END ANIMATION STUFF

		//move the camera
		GameObject camera = GameObject.FindWithTag("MainCamera");
		GameObject background = GameObject.FindWithTag("Background");
		Vector3 backgroundSize = background.GetComponent<SpriteRenderer>().bounds.size;
		Vector3 backgroundPos = background.transform.position;
		float xMin = backgroundPos.x - (backgroundSize.x / 2);
		float xMax = backgroundPos.x + (backgroundSize.x / 2);
		float yMin = backgroundPos.y - (backgroundSize.y / 2);
		float yMax = backgroundPos.y + (backgroundSize.y / 2);
		float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		float screenHeight = Camera.main.orthographicSize;

		float camX = Mathf.Clamp(transform.position.x, xMin + screenWidth, xMax - screenWidth);
		float camY = Mathf.Clamp(transform.position.y, yMin + screenHeight, yMax - screenHeight);

		camera.transform.position = new Vector3 (camX, camY, -10);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Teleporter") {
			GameObject manager = GameObject.FindWithTag ("GameController");
			manager.GetComponent<GameController> ().playerX = coll.gameObject.GetComponent<TeleporterScript> ().x;
			manager.GetComponent<GameController> ().playerY = coll.gameObject.GetComponent<TeleporterScript> ().y;
			SceneManager.LoadScene (coll.gameObject.GetComponent<TeleporterScript> ().scene);
		}
		else if (coll.gameObject.tag == "Artifact") {
			message.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Artifact") {
			message.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (Input.GetKey (KeyCode.F)) {
			if (coll.gameObject.tag == "Artifact" && fUnpressed) {
				//pause the game
				Time.timeScale = 0;
				GameObject box = Instantiate (artifactBox);
				box.transform.Find ("img").GetComponent<SpriteRenderer> ().sprite = coll.gameObject.GetComponent<ArtifactScript> ().image;
				box.transform.Find ("name").GetComponent<TextMesh> ().text = coll.gameObject.GetComponent<ArtifactScript> ().artifactName;
				setText (box.transform.Find ("description").GetComponent<TextMesh> (), coll.gameObject.GetComponent<ArtifactScript> ().description);

				GameController manager = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
				manager.artifactTune.clip = coll.gameObject.GetComponent<ArtifactScript> ().tune;
				manager.artifactSfx.Play ();
				manager.artifactTune.PlayDelayed (2.2f);
				if (coll.gameObject.GetComponent<ArtifactScript> ().removeable) {
					manager.destroyedObjects.Add (coll.gameObject.GetComponent<ArtifactScript> ().artifactName);
					manager.save ();
					Destroy (coll.gameObject);
					message.GetComponent<SpriteRenderer> ().enabled = false;
				}
				fUnpressed = false;
			}
		} else {
			fUnpressed = true;
		}
	}

	void setText(TextMesh tMesh, string text) {
		//http://answers.unity3d.com/answers/778195/view.html
		string builder = "";
		tMesh.text = "";
		float rowLimit = 9f;
		string[] parts = text.Split(' ');
		for (int i = 0; i < parts.Length; i++) {
			tMesh.text += parts[i] + " ";
			if (tMesh.GetComponent<Renderer>().bounds.extents.x > rowLimit) {
				tMesh.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
			}
			builder = tMesh.text;
		}
	}
}
