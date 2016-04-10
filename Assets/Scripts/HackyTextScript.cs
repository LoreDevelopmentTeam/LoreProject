using UnityEngine;
using System.Collections;

public class HackyTextScript : MonoBehaviour {

	// Unity sucks
	void Start () {
		this.GetComponent<Renderer>().sortingLayerName = "Foreground 2: Foregrounder";
	}
}
