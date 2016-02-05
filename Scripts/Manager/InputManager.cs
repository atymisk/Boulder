using UnityEngine;
using System.Collections;

public abstract class InputManager : MonoBehaviour 
{
	
	// Use this for initialization
	public Robot playerOne;
	public Robot playerTwo;
	
	public bool p1lock = false;
	public bool p2lock = false;

	public GameManager gameManager;

	public float previousP1X = 0;
	public float previousP1Y = 0;
	public float previousP1DPadY = 0;
	public float previousP2X = 0;
	public float previousP2Y = 0;
	public float previousP2DPadY = 0;

	//p1 controls need to be disabled temporarily until p1 respawns
	public void lockp1control()
	{
		p1lock = true;//p1 is dead
	}
	
	//p2 controls need to be disabled temporarily until p2 respawns
	public void lockp2control()
	{
		p2lock = true;//p2 is dead
	}

    public void lockcontrols()
    {
        p1lock = true;
        p2lock = true;
    }

    public void unlockcontrols()
    {
        p1lock = false;
        p2lock = false;
    }
	
	//respawned
	public void unlockp1control(Robot newp1)
	{
		playerOne = newp1;
		p1lock = false;
	}
	
	public void unlockp2control(Robot newp2)
	{
		playerTwo = newp2;
		p2lock = false;
	}



	// Update is called once per frame
	void Update()
    {

    }
}