using UnityEngine;
using System.Collections;

public class GetInfo : MonoBehaviour {

	public UILabel info;

	void Awake() {
	
		if (Controller.Coins != null && Controller.Distance != null) {
		
			info.text = "You crashed!\n" +  Controller.Distance + "\n" + Controller.Coins;
			audio.Play ();

		}

	}

}
