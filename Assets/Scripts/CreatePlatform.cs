using UnityEngine;
using System.Collections;

public class CreatePlatform : MonoBehaviour 
{

	public GameObject pipe;
    public GameObject pipeOnTurn;
    public GameObject planeStone;
    public GameObject player;
	public GameObject coin;

	public GameObject Left;
	public GameObject Right;
	public GameObject Towards;
	public GameObject Back;
	public int FirstCreate = 10;
	public int planeScale = 4;

	public System.Collections.Generic.LinkedList<Object> listPlane = new System.Collections.Generic.LinkedList<Object>();

	Quaternion left;
	Quaternion right;
	Quaternion towards;
	Quaternion back;
	
	int direction = 0;
    Object tempPlane;
	int canBeStone = 0;
    int directCounter = 0;
    int randRange = 10;
    int minDirect = 4;
    int maxDirect = 5;

	int Z = 0;
	int X = 0;
	int Y = 0;
    
    System.Random rand = new System.Random();

    void Awake()
    {
		left = Left.transform.rotation;
		right = Right.transform.rotation;
		towards = Towards.transform.rotation;
		back = Back.transform.rotation;

		for (int i = 0; i < 10; i++)
		{
			tempPlane = Instantiate(pipe, new Vector3(X, Y, ++Z) * planeScale, towards);
			listPlane.AddLast(tempPlane);
		}

		for (int i = 0; i < FirstCreate; i++)
        {
			CreatePipe();
		}
    }
	public void CreatePipe()
	{
		if (direction == 0)
		{
			if ( directCounter > minDirect && (rand.Next(randRange) == 1 || directCounter == maxDirect ))
			{
				if (rand.Next(2) == 1)
				{
					tempPlane = Instantiate(pipeOnTurn, new Vector3(X, Y, ++Z) * planeScale, right);
					listPlane.AddLast(tempPlane);
					direction = 1;
				}
				else
				{
					tempPlane = Instantiate(pipeOnTurn, new Vector3(X, Y, ++Z) * planeScale, back);
					listPlane.AddLast(tempPlane);
					direction = -1;
				}
				directCounter = 0;
			}
			else
			{
				if (rand.Next(5) == 1)
					Instantiate(coin, new Vector3(X, 0.5f, Z) * planeScale, new Quaternion());

				
				if (rand.Next(20) == 1 && canBeStone > 5)
				{
					tempPlane = Instantiate(planeStone, new Vector3(X, Y, ++Z) * planeScale, towards);
					listPlane.AddLast(tempPlane);
					canBeStone = 0;
				}
				else 
				{
					tempPlane = Instantiate(pipe, new Vector3(X, Y, ++Z) * planeScale, towards);
					listPlane.AddLast(tempPlane);
					canBeStone++;
				}

				directCounter++;
				
			}
			return;
		}
		
		// рух вправо
		if (direction == 1)
		{
			if ( directCounter > minDirect && (rand.Next(randRange) == 1 || directCounter == maxDirect ))
			{
				tempPlane = Instantiate(pipeOnTurn, new Vector3(++X, Y, Z) * planeScale, left);
				listPlane.AddLast(tempPlane);
				direction = 0;

				directCounter = 0;
			}
			else
			{
				if (rand.Next(5) == 1)
					Instantiate(coin, new Vector3(X, 0.5f, Z) * planeScale, new Quaternion());

				if (rand.Next(20) == 1 && canBeStone > 5)
				{
					tempPlane = Instantiate(planeStone, new Vector3(++X, Y, Z) * planeScale, left);
					listPlane.AddLast(tempPlane);
					canBeStone = 0;
				}
				else 
				{
					tempPlane = Instantiate(pipe, new Vector3(++X, Y, Z) * planeScale, left);
					listPlane.AddLast(tempPlane); 
					canBeStone++;
				}
				directCounter++;
			}
			return;
		}
		
		// рух вліво
		if (direction == -1)
		{
			if ( directCounter > minDirect && (rand.Next(randRange) == 1 || directCounter == maxDirect ))
			{
				tempPlane = Instantiate(pipeOnTurn, new Vector3(--X, Y, Z) * planeScale, towards);
				listPlane.AddLast(tempPlane);
				direction = 0;
				directCounter = 0;
			}
			else
			{
				if (rand.Next(5) == 1)
					Instantiate(coin, new Vector3(X, 0.5f, Z) * planeScale, new Quaternion());

				if (rand.Next(20) == 1 && canBeStone > 5)	
				{
					tempPlane = Instantiate(planeStone, new Vector3(--X, Y, Z) * planeScale, left);
					listPlane.AddLast(tempPlane);
					canBeStone = 0;
				}
				else 
				{
					tempPlane = Instantiate(pipe, new Vector3(--X, Y, Z) * planeScale, left);
					listPlane.AddLast(tempPlane); 
					canBeStone++;
				}
				directCounter++;
			}
			return;
		}
	}

}
