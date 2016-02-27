using UnityEngine;
using System.Collections;

public class HackyTextScript : MonoBehaviour {

	// Unity sucks
	void Start () {
		this.GetComponent<Renderer>().sortingLayerID = this.transform.parent.GetComponent<Renderer>().sortingLayerID;
	}
}
