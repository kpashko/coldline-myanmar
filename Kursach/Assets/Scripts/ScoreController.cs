using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	int score = 0;
	int currentMultiplier = 0;
	float comboTimer = 0.0f;
	int tempScoreHold = 1;

	float originalWidth = 1920.0f;
	float originalHeight = 1080.0f;
	Vector3 scale;
	public GUIStyle text;
	public Texture2D bg;
	public GameObject fiveHun, thou;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		comboCountdown ();
	}

	public void AddScore(int val, Vector3 position)
	{
		tempScoreHold += val;
		//Debug.Log ("added score " + val);
		Vector3 spawnPos = position;
		spawnPos.y += 2;

		if (val == 500) {
			Instantiate (fiveHun, spawnPos, fiveHun.transform.rotation);
		} else if (val == 1000) {
			Instantiate (thou, spawnPos, fiveHun.transform.rotation); //thou.transform?
		}
	}

	public void increaseMultiplier()
	{
		Debug.Log ("Increased multiplier to " + currentMultiplier);
		currentMultiplier++;
		comboTimer = 3.5f;
	}

	void comboCountdown()
	{
		if (tempScoreHold > 0) {
			comboTimer -= Time.deltaTime;

			if (comboTimer <= 0) {
				score += (tempScoreHold * currentMultiplier);
				//Debug.Log ("Increased score to :" + score);
				tempScoreHold = 0;
				currentMultiplier = 1;
			}
		}
	}

	void OnGUI()
	{
		GUI.depth = 0;
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);

		Rect scorePos = new Rect (originalWidth - 500, (originalHeight - originalHeight) + 50, 200, 100);
		Rect multiPos = new Rect (originalWidth - 500, (originalHeight - originalHeight) + 100, 200, 100);
		Rect bgPos = new Rect (originalWidth - 750, (originalHeight - originalHeight) + 50, 700, 150);
		GUI.DrawTexture (bgPos, bg);
		GUI.Box (scorePos, "Score:  " + score, text);
		GUI.Box (multiPos, "Combo:  " + currentMultiplier + " * " + tempScoreHold + " - " + (int)comboTimer, text);
		GUI.matrix = svMat;
	}
}
