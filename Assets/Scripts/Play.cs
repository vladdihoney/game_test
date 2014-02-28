using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

	void OnClick() 
	{
		Application.LoadLevelAsync(1);
	}

}