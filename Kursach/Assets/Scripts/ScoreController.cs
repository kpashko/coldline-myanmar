using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


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
	public GameObject[] enemies;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		comboCountdown ();
		if (allEnemiesDead() == true) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
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
			Instantiate (thou, spawnPos, thou.transform.rotation); //thou.transform?
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
		
	
	public bool allEnemiesDead()
	{
		for (int i = 0; i < enemies.Length; i++) {
			if (enemies [i].tag != "Dead") {
				return false;
			}
		}
		return true;
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
		Rect positionForRestart = new Rect(100, originalHeight - 200, 720, 100);
        GUI.DrawTexture(bgPos, bg);
		if (allEnemiesDead () == true) {
			GUI.Box (scorePos, "Level cleared!", text);
			GUI.Box (multiPos, "Your score: " + score, text);
			GUI.DrawTexture(positionForRestart, bg);
			GUI.Box(positionForRestart, "Press 'R' to restart", text);
		}
		else if (PlayerHealth.dead == false) {
			GUI.Box (scorePos, "Score:  " + score, text);
			GUI.Box (multiPos, "Combo:  " + currentMultiplier + " * " + tempScoreHold + " - " + (int)comboTimer, text);
		} 

        else
        {
            GUI.Box(scorePos, "You Died", text);
        }
		GUI.matrix = svMat;
	}
}
