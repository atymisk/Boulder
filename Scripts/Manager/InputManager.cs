﻿using UnityEngine;
using System.Collections;

public abstract class InputManager : MonoBehaviour 
{
	
	// Use this for initialization
	public Character playerOne;
	public Character playerTwo;
	
	public bool p1lock = false;
	public bool p2lock = false;

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
	
	//respawned
	public void unlockp1control(Character newp1)
	{
		playerOne = newp1;
		p1lock = false;
	}
	
	public void unlockp2control(Character newp2)
	{
		playerTwo = newp2;
		p2lock = false;
	}
	
	
	// Update is called once per frame
	void Update()
    {

    }
}