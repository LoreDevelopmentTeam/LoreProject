using UnityEngine;
using System.Collections;

public class ArtifactBoxScript : MonoBehaviour {

	bool unpressed;

	void Start () {
		unpressed = false;

		//resize to fit screen- from http://answers.unity3d.com/questions/620699/scaling-my-background-sprite-to-fill-screen-2d-1.html
		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(worldScreenWidth / sr.sprite.bounds.size.x,
										   worldScreenHeight / sr.sprite.bounds.size.y, 1);

		float cameraX = Camera.main.transform.position.x;
		float cameraY = Camera.main.transform.position.y;
		transform.position = new Vector3 (cameraX, cameraY, 1);
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
