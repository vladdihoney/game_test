using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float smoothSpeed = 0.02f;
	public GameObject Left;
	public GameObject Right;
	public GameObject Towards;
	public int PipeScale = 2;
	public float rotateSpeed = 0.1f;
	public UILabel Distance;
	public UILabel Coins;

	public UIButton pauseButton;

	public AudioClip collectCoin;

	CreatePlatform createPlatform;
	Pause pause;
    Animator animator;
	int Direction = 0;
	bool rotating = false;    
    Quaternion left;
    Quaternion right;
    Quaternion towards;

	Vector3 other;
	float startTime;
	Vector2 startPos;
	bool couldBeSwipe;
	float minSwipeDist = 0.001f;
	float maxSwipeTime = 10000f;

	int distanceCounter = 0;
	int coinCounter = 0;

	float timeStartJump;

    void Awake()
	{
		pause = pauseButton.GetComponent<Pause>();

		createPlatform = gameObject.GetComponent<CreatePlatform>();

        left = Left.transform.rotation;
        right = Right.transform.rotation;
        towards = Towards.transform.rotation;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
		if (Time.time - timeStartJump > 0.2f)  
		{
			audio.Pause();
		}

		if (Time.time - timeStartJump > 1.3f) 
		{
			audio.Play();
		}

		Distance.text = "Distance  " + distanceCounter.ToString ();
		Coins.text = "Coins  " + coinCounter.ToString ();

		Controller.Distance = Distance.text;
		Controller.Coins = Coins.text;

		animator.SetBool("Air", false);

		animator.SetBool ("Paused", pause.Paused);

		if (animator.GetBool ("Paused")) {
			audio.Pause();
		}
		

		if (Input.touchCount > 0)
		{
			var touch = Input.touches[0];

			switch(touch.phase)
			{
			case TouchPhase.Began: 
				couldBeSwipe = true;
				startPos = touch.position;
				startTime = Time.time;
				break;

			case TouchPhase.Stationary: 
				couldBeSwipe = false;
				break;

			case TouchPhase.Ended: 
				var swipeTime = Time.time - startTime;
				var swipeDist = (touch.position - startPos).magnitude;

				if (couldBeSwipe  && !pause.Paused) {

					if (Mathf.Abs(touch.position.x - startPos.x) > Mathf.Abs(touch.position.y - startPos.y))

						if (touch.position.x - startPos.x < 0)
							TurnLeft();
						else 
							TurnRight();
					else
					{
						animator.SetBool("Air", true);
						startJump();
					}

				}
				
				break;

			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Air", true);
			startJump();
        }

        if (Input.GetKeyDown(KeyCode.A) && !pause.Paused)
        {
            TurnLeft ();
        }

		if (Input.GetKeyDown(KeyCode.D) && !pause.Paused)
        {
            TurnRight ();
        }

    }

	void FixedUpdate ()
	{
		if (rotating) {
			switch (Direction) {
			case 0: {
				RotateTo (towards);
				break;
			}
			case 1: {
				RotateTo (right);
				break;
			}
			case -1: {
				RotateTo (left);
				break;
			}
			}
		}
		else {
			other = transform.position;
			if (Direction == 0) {
				transform.position = Vector3.Lerp (transform.position, new Vector3 ((float)System.Math.Round (other.x / PipeScale) * PipeScale, other.y, other.z), smoothSpeed);
			}
			else
				if (Direction == 1 || Direction == -1) {
					transform.position = Vector3.Lerp (transform.position, new Vector3 (other.x, other.y, (float)System.Math.Round (other.z / PipeScale) * PipeScale), smoothSpeed);
				}
		}
	}

	void TurnLeft()
	{
		if (transform.rotation == right) {
			rotating = true;
			Direction = 0;
		}
		else if (transform.rotation == towards)
		{
			rotating = true;
			Direction = -1;
		}
	}

	void TurnRight ()
	{
		if (transform.rotation == left) {
			rotating = true;
			Direction = 0;
		}
		else if (transform.rotation == towards)
		{
			rotating = true;
			Direction = 1;
		}
	}

	void RotateTo (Quaternion To)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, To, rotateSpeed);
		if (Mathf.Abs(transform.rotation.y - To.y) < 0.01)
		{
			transform.rotation = To;
			rotating = false;            
		}
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Coin")
		{
			GameObject.Destroy(other.gameObject);
			coinCounter++;
			audio.PlayOneShot(collectCoin);
			return;
		}
		if (other.gameObject.tag == "DestroyPipe")
		{
			GameObject.Destroy(createPlatform.listPlane.First.Value);
			createPlatform.listPlane.RemoveFirst();
			createPlatform.CreatePipe();

			distanceCounter++;

			return;
		} 
		if (other.gameObject.tag == "Stone") 
		{
			if (!(Time.time - timeStartJump > 0.2f && Time.time - timeStartJump < 1f)) 
			{
				GameOver();
				return;
			}

			GameObject.Destroy(createPlatform.listPlane.First.Value);
			createPlatform.listPlane.RemoveFirst();
			createPlatform.CreatePipe();
			return;

		}
		GameOver();

	}

	void GameOver() 
	{
		Application.LoadLevel(0);
	}

	void startJump() 
	{
		timeStartJump = Time.time;
	}



}
