using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	GameObject player;
	bool followPlayer = true;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (followPlayer == true) {
			canFollowPlayer ();
		}
	}

	public void setFollowPlayer (bool val){
		followPlayer = val;
	}
	void canFollowPlayer(){
		Vector3 newPos = new Vector3 (player.transform.position.x, player.transform.position.y, this.transform.position.z);
		this.transform.position = newPos;
	}
}
