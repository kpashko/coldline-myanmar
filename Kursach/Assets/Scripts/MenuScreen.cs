using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {
	float originalWidth = 1920.0f;
	float originalHeight = 1080.0f;
	Vector3 scale;

	public GUIStyle text, titleText, titleShadow;
	public Texture2D bg;

	bool playSelect = true, exitSelect = false, play = false, menu = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		inputController ();
	}

	void inputController()
	{
		if (menu == true) {
			if (Input.GetKeyDown (KeyCode.S) && playSelect == true || Input.GetKeyDown (KeyCode.W) && playSelect == true) {
				exitSelect = true;
				playSelect = false;
			} else if (Input.GetKeyDown (KeyCode.S) && exitSelect == true || Input.GetKeyDown (KeyCode.W) && exitSelect == true) {
				exitSelect = false;
				playSelect = true;
			}

			if (Input.GetKeyDown (KeyCode.Return) && playSelect == true) {
				menu = false;
				play = true;
			} else if (Input.GetKeyDown (KeyCode.Return) && exitSelect == true) {
				Application.Quit ();
			}
		} else if (play == true) {
				SceneManager.LoadScene ("MainScene");
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
		Rect titlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight, 800, 300);
		if (menu == true) {
			play = false;
			titlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight, 800, 300);
			GUI.Box (titlePos, "Kursova robota", titleShadow);

			titlePos = new Rect (originalWidth / 2 - 405, originalHeight - originalHeight - 5, 800, 300);
			GUI.Box (titlePos, "Kursova robota", titleText);

			Rect menuPos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight + 400, 800, 200);
			if (playSelect == true) {
				GUI.DrawTexture (menuPos, bg);
				GUI.Box (menuPos, "Play", text);
			} else if (playSelect == false) {
				GUI.Box (menuPos, "Play", text);
			}

			Rect exitPos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight + 700, 800, 200);
			if (exitSelect == true) {
				GUI.DrawTexture (exitPos, bg);
				GUI.Box (exitPos, "Exit", text);
			} else if (exitSelect == false) {
				GUI.Box (exitPos, "Exit", text);
			}
		}
		GUI.matrix = svMat;
	}
}
