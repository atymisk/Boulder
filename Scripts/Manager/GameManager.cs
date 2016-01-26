using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	/*
    This script should be managing the state of the game. ex: CharacterSelection, Match, Pause, etc
    Should also handle who is player 1 and who is player 2
    Managing the death and respawns of the players
    Managing the stocks and checking win condition
    */
	public static GameManager instance = null;
	
	public Robot P1;//possible issue, make a prefab of the player object maybe
	public Robot P2;
	
	private Robot p1_origin;
	private Robot p2_origin;
	
	public Transform P1spawnPoint;
	public Transform P2spawnPoint;
	
	public bool isPaused = false;
	
	//needs to tell the input and camera manager that the player has died and respawned
	GameObject inptmng;
	KeyInputManager keyinpt;
	ControllerInputManager continpt;
	InputManager primaryINPT;
	StatsManager stats;
	public CameraManager cmrmng;
	public GameObject pauseMenu;
	
	private int p1_stocks = 4;
	private int p2_stocks = 4;
	
	public Text p1Left;
	public Text p2Left;
	
	//private System.Timers.Timer countdown;
	private static float countdown = 120;
	public Text timer;
	
	private bool gameover = false;
	private float respawntimer = 1.25f;
	private bool keys = true;
	//private string winner;
	
	public void thisPlayerDied(string tag)
	{
		if(tag == "P1")
		{
			p1death();
		}
		else if(tag == "P2")
		{
			p2death();
		}
	}
	
	void p1death()//called twice
	{
		//tell inputmanager and camera manager that both are dead
		//destroy instance of the player and create a new one
		
		//ADDED HERE TEMPORARILY
		GameObject deathEffect = (GameObject) Resources.Load ("Particles/DeathEffect");
		var deathEffectClone = Instantiate (deathEffect, P1.transform.position, Quaternion.identity);
		SpecialEffects.instance.SlowMo(0.1f, 0.1f);
		SpecialEffects.instance.ShakeScreen(0.3f);
		
		Destroy(P1.gameObject);
		p1_stocks--;
		p1Left.text = p1_stocks.ToString ();
		primaryINPT.lockp1control();
		cmrmng.p1_is_dead();
		
		if(p1_stocks == 0)
		{
			gameover = true;	
		}
		else
		{
			//wait before respawn
			StartCoroutine(p1respawn());
		}
	}
	
	void p2death()
	{
		//tell inputmanager and camera manager that both are dead
		//destroy instance of the player and create a new one
		
		//ADDED HERE TEMPORARILY
		GameObject deathEffect = (GameObject) Resources.Load ("Particles/DeathEffect");
		var deathEffectClone = Instantiate (deathEffect, P2.transform.position, Quaternion.identity);
		SpecialEffects.instance.SlowMo(0.1f, 0.1f);
		SpecialEffects.instance.ShakeScreen(0.3f);
		
		Destroy(P2.gameObject);
		p2_stocks--;
		p2Left.text = p2_stocks.ToString ();
		primaryINPT.lockp2control();
		cmrmng.p2_is_dead();
		
		if (p2_stocks == 0)
		{
			gameover = true;
		}
		else
		{
			//wait before respawn
			StartCoroutine(p2respawn());
		}
	}
	
	IEnumerator p1respawn()
	{
		//create new Player1 object
		yield return new WaitForSeconds(respawntimer);
		P1 = Instantiate(p1_origin);
		P1.transform.position = P1spawnPoint.position;
		P1.enabled = true;
		//give inputmanager and camera manager the new one
		primaryINPT.unlockp1control(P1);
		cmrmng.p1_respawn(P1);
	}
	
	IEnumerator p2respawn()
	{
		//create new Player2 object
		yield return new WaitForSeconds(respawntimer);
		P2 = Instantiate(p2_origin);
		P2.transform.position = P2spawnPoint.position;
		P2.enabled = true;
		//give inputmanager and camera manager the new one
		primaryINPT.unlockp2control(P2);
		cmrmng.p2_respawn(P2);
	}

    void Awake ()
    {
        instance = this;

        countdown = MatchSettingsData.match_time;
        p1_stocks = MatchSettingsData.stock_total;
        p2_stocks = MatchSettingsData.stock_total;
        p1Left.text = p1_stocks.ToString();
        p2Left.text = p2_stocks.ToString();
		
		inptmng = GameObject.Find("InputManager");
		keyinpt = inptmng.GetComponent<KeyInputManager>();
		continpt = inptmng.GetComponent<ControllerInputManager>();
		continpt.enabled = false;
		keyinpt.enabled = true;
		primaryINPT = keyinpt;
		stats = GameObject.Find ("StatsManager").GetComponent<StatsManager>();
		
		P1.setTag("P1");
		P1.transform.position = P1spawnPoint.position;
		p1_origin = Instantiate(P1);//clone P1
		p1_origin.enabled = false;
		p1_origin.transform.position = new Vector3(-5,245,0);
		//p1_origin.rend.enabled = false;
		
		P2.setTag("P2");
		P2.transform.position = P2spawnPoint.position;
		p2_origin = Instantiate(P2);//clone P2
		p2_origin.enabled = false;
		p2_origin.transform.position = new Vector3(5, 245, 0);
	}

	private void toggleInputs()
	{
		if(!keys)
		{
			keyinpt.enabled = false;
			continpt.unlockp1control(P1);
			continpt.unlockp2control(P2);
			continpt.enabled = true;
			primaryINPT = continpt;
			print("Primary: CONTROLLER");
		}
		else
		{
			continpt.enabled = false;
			keyinpt.unlockp1control(P1);
			keyinpt.unlockp2control(P2);
			keyinpt.enabled = true;
			primaryINPT = keyinpt;
			print("Primary: KEYS");
		}
		keys = !keys;
	}
	
	void DisplayCountdown()
	{
		countdown -= Time.deltaTime;
		if (countdown <= 0)
        {
			countdown = 0;
            gameover = true;
        }
		int minutes = (int)(countdown/60);
		int seconds = (int)(countdown % 60);
		timer.text = string.Format("{0:00}:{1:00}",minutes,seconds);
	}

    void checkgameover()
    {
        if(gameover)
        {
            stats.matchEnd();
            stats.writeStatsToFile();
            ChangeScene.instance.ChangetoScene("MainUI");
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
		DisplayCountdown();
        checkgameover();

		//for testing input purposes
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			print("Toggled");
			toggleInputs();
		}
		if (isPaused)
        {
			stats.matchPause();
			Time.timeScale = 0;
			//			Cursor.visible = true;
			pauseMenu.SetActive(true);
		}
        else
        {
			stats.matchStart();
			Time.timeScale = 1;
			//			Cursor.visible = false;
			pauseMenu.SetActive(false);
		}
//		Debug.Log(stats.getMilliseconds ());
	}
}
