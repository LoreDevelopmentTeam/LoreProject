using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {
	
	public void click() {
		GameObject.Find ("GameController").GetComponent<GameController> ().getInstance().click.Play ();
		SceneManager.LoadScene ("0_Title");
		Time.timeScale = 1;
		Destroy (gameObject.transform.parent.gameObject);
	}

	public void Update() {
		//http://answers.unity3d.com/answers/742034/view.html
		if (Input.GetMouseButtonDown(0)) {
			Vector2 p = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D[] hits = Physics2D.OverlapPointAll(p);
			foreach(Collider2D hit in hits) {
				if (hit.Equals (gameObject.GetComponent<BoxCollider2D> ())) {
					click ();
				}
			}
		}
	}
}
