using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	public GameObject sp;

	void Start () {
		float cameraX = Camera.main.transform.position.x;
		float cameraY = Camera.main.transform.position.y;
		transform.position = new Vector3 (cameraX, cameraY, 90);
	}
}
