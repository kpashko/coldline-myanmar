using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public bool moving = false;
	float speed = 5.0f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		movement ();
	}

	void movement(){
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.up * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.down * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime, Space.World);
			moving = true;
		}
	}
}
