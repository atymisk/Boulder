using UnityEngine;
using System.Collections;
using System;
using System.IO;

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
	private System.Diagnostics.Stopwatch stopwatch;
	private long matchTime;
	public Robot playerOne;
	public Robot playerTwo;
	
	// Use this for initialization
	void Start ()
	{
		stopwatch = new System.Diagnostics.Stopwatch ();
		matchTime = 0;
		playerOneStats.player = playerOne;
		playerTwoStats.player = playerTwo;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void incrementLeftKick(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.leftKick;
		}
		else{
			++playerTwoStats.leftKick;
		}
	}
	
	public void incrementRightKick(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.rightKick;
		}
		else{
			++playerTwoStats.rightKick;
		}
	}
	
	public void incrementLeftPunch(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.leftPunch;
		}
		else{
			++playerTwoStats.leftPunch;
		}
	}
	
	public void incrementRightPunch(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.rightPunch;
		}
		else{
			++playerTwoStats.rightPunch;
		}
	}
	public void incrementRocket(Robot player){
		if(player == playerOneStats.player){
			++playerOneStats.rocket;
		}
		else{
			++playerTwoStats.rocket;
		}
	}
	
	public void reset(){
		playerOneStats.leftKick = playerOneStats.rightKick = playerOneStats.leftPunch = playerOneStats.rightPunch = playerTwoStats.leftKick = playerTwoStats.rightKick = playerTwoStats.leftPunch = playerTwoStats.rightPunch = 0;
	}
	
	public void matchStart(){
		stopwatch.Start ();
	}
	
	public void matchPause(){
		stopwatch.Stop ();
	}
	
	public void matchEnd(){
		stopwatch.Stop ();
		matchTime = stopwatch.ElapsedMilliseconds;
		stopwatch.Reset ();
	}

	public long getMilliseconds(){
		return stopwatch.ElapsedMilliseconds;
	}

	private string ToReadableString(this TimeSpan span)
	{
		string formatted = string.Format("{0}{1}{2}{3}",
		                                 span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
		                                 span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
		                                 span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
		                                 span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);
		
		if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);
		
		if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";
		
		return formatted;
	}

	public void writeStatsToFile(){
		var timeSpan = TimeSpan.FromMilliseconds (matchTime);
		string[] placeholder = {"Elapsed Time: " + ToReadableString(timeSpan), "", "Statistics", "==========", "Player 1 Left Punch: " + playerOneStats.leftPunch, "Player 1 Right Punch: " + playerOneStats.rightPunch, "Player 1 Left Kick: " + playerOneStats.leftKick, "Player 1 Right Kick: " + playerOneStats.rightKick, "Player 1 Rocket Punches: " + playerOneStats.rocket, "==========", "Player 2 Left Punch: " + playerTwoStats.leftPunch, "Player 2 Right Punch: " + playerTwoStats.rightPunch, "Player 2 Left Kick: " + playerTwoStats.leftKick, "Player 2 Right Kick: " + playerTwoStats.rightKick, "Player 2 Rocket Punches: " + playerTwoStats.rocket};
//		Debug.Log (System.DateTime.Now.ToString("MM-dd-yyyy") + "_" + System.DateTime.Now.ToString ("hhmmss") + ".txt");
		Directory.CreateDirectory ("stats");
//		Debug.Log (Directory.GetCurrentDirectory ());
		System.IO.File.WriteAllLines(@"stats\" + System.DateTime.Now.ToString("MM-dd-yyyy") + "_" + System.DateTime.Now.ToString ("hhmmss") + ".txt", placeholder);
	}
}

