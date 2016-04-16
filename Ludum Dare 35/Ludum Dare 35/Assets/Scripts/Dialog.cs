using UnityEngine;
using System.Collections;


[System.Serializable]
public class DialogLine {
	[Multiline]
	public string line;
	public DialogActor actor;
}

public enum DialogActor {
	ACTOR_1, ACTOR2
}


public class Dialog : MonoBehaviour {

	public bool nextSceneAfterDialog = false;
	public GameObject dialogWindowPrefab;
	public Color color1;
	public Color color2;
	public DialogLine[] dialogLines;

	private int currentLineIndex;
	private DialogWindow dialogWindow;

	void Start () {
		currentLineIndex = -1;

		if (GetComponent<Collider2D> () == null) {
			StartDialog ();
		}
	}

	void Update () {
		if (currentLineIndex >= 0 && Input.GetKeyDown (KeyCode.Return)) {
			ShowNextLine ();
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Player")) {
			GetComponent<Collider2D> ().enabled = false;
			StartDialog ();
		}
	}

	private void StartDialog() {
		Time.timeScale = 0;
		currentLineIndex = 0;
		Vector3 position = new Vector3 (transform.position.x, transform.position.y, dialogWindowPrefab.transform.position.z);
		GameObject dialogWindowObject = (GameObject) Instantiate (dialogWindowPrefab, position, Quaternion.identity);
		dialogWindow = dialogWindowObject.GetComponent<DialogWindow> ();

		ShowNextLine ();
	}

	private void ShowNextLine() {
		if (currentLineIndex == dialogLines.Length) {
			Time.timeScale = 1;
			Destroy (dialogWindow.gameObject);
			Destroy (gameObject);

			if (nextSceneAfterDialog) {
				Fader.instance.FadeOutAndPlayNextScene ();
			}

			return;
		}

		DialogLine currentLine = dialogLines [currentLineIndex];

		if (currentLine.actor == DialogActor.ACTOR_1) {
			dialogWindow.ShowLeftText (currentLine.line, color1);
		} else {
			dialogWindow.ShowRightText (currentLine.line, color2);
		}

		currentLineIndex++;
	}


}
