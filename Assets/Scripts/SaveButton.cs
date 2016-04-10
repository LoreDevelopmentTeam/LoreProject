using UnityEngine;
using System.Collections;

public class SaveButton : MonoBehaviour {

	public GameObject gameController;

	public void OnMouseDown() {
		gameController.GetComponent<GameController>().save ();
	}
}
