using UnityEngine;
using System.Collections;

struct PlayerStats{
	public Robot player;
	public int leftKick, rightKick, leftPunch, rightPunch, rocket;

	public PlayerStats(Robot player){
		this.player = player;
		leftKick = rightKick = leftPunch = rightPunch = rocket = 0;
	}
}

public class StatsManager : MonoBehaviour
{
	private PlayerStats playerOneStats;
	private PlayerStats playerTwoStats;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void incrementLeftKick(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.leftKick;
		}
		else{
			++playerTwoStats.leftKick;
		}
	}

	void incrementRightKick(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.rightKick;
		}
		else{
			++playerTwoStats.rightKick;
		}
	}

	void incrementLeftPunch(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.leftPunch;
		}
		else{
			++playerTwoStats.leftPunch;
		}
	}

	void incrementRightPunch(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.rightPunch;
		}
		else{
			++playerTwoStats.rightPunch;
		}
	}
	void incrementRocket(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.rocket;
		}
		else{
			++playerTwoStats.rocket;
		}
	}

	void reset(Robot player){
		if(player == playerOneStats.player){
			playerOneStats.leftKick = playerOneStats.rightKick = playerOneStats.leftPunch = playerOneStats.rightPunch = 0;
		}
		else{
			playerTwoStats.leftKick = playerTwoStats.rightKick = playerTwoStats.leftPunch = playerTwoStats.rightPunch = 0;
		}
	}

	void writeStatsToFile(){
		string[] placeholder = {""};
		System.IO.File.WriteAllLines(string.Format("stats\\{o}_{1}.txt", System.DateTime.Now.ToString("MM-dd-yyyy"), System.DateTime.Now.ToString ("hh:mm:ss")), placeholder);
	}
}

