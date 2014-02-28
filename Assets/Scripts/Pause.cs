using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public PlayerMovement foo2;
	public UILabel label;
	public bool Paused = false;

	void Awake() {
		label.text = "Pause";
	}

	void OnClick () {

		if (!Paused) {
			Paused = true;
			label.text = "Coutinue";

		} else {
			Paused = false;
			label.text = "Pause";
		}

	}
}
