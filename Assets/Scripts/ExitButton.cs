using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	public void click() {
		Application.Quit ();
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
