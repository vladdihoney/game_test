using UnityEngine;
using System.Collections;

public class CoinRotation : MonoBehaviour {

	void FixedUpdate () {
		transform.Rotate (new Vector3 (0, 0, 5));
	}
}
