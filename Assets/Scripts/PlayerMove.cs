using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	float speed = 5.0f;
	float moveX = 0;
	float moveY = 0;
	float acceleration = 0.1f;

	void Start () {
	
	}

	void Update () {
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
}
