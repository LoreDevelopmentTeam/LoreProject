using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {
	
	public void OnMouseDown() {
		SceneManager.LoadScene ("Title");
		Time.timeScale = 1;
		Destroy (gameObject.transform.parent.gameObject);
	}
}
