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

    private bool gamestart = false;
    private float startimer = 4;
    public Text start;

    public Text winner;

	private bool gameover = false;
	private float respawntimer = 1.25f;
	private bool keys = true;
	//private string winner;
	
	public void thisPlayerDied(string tag)
	{
		if(tag == "P1")
			p1death();
		else if(tag == "P2")
			p2death();
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
            player2wins();
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
            player1wins();
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
    
    void checkgameover()
    {
        if(gameover)
        {
            stats.matchEnd();
            stats.writeStatsToFile();
            //primaryINPT.lockcontrols();
            StartCoroutine(endmatch());
        }
    }

    public void togglePause()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
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
		int seconds = (int)(countdown % 60);
        if (countdown <= 0)
        {
            countdown = 0;
            gameover = true;
        }
        else if (countdown > 10)
        {
		    int minutes = (int)(countdown/60);
		    timer.text = string.Format("{0:00}:{1:00}",minutes,seconds);
        }
        else
        {
            timer.color = Color.red;
            timer.text = seconds.ToString();
            timer.fontSize = 50;
        }
	}

    IEnumerator startmatch()
    {
        int mcountdown = (int)startimer;
        start.text = mcountdown.ToString();
        startimer -= Time.deltaTime;
        if(startimer <= 1)
        {
            start.fontStyle = FontStyle.Italic;
            start.fontSize = 50;
            start.text = "START!";
            gamestart = true;
            primaryINPT.unlockcontrols();
        }
        if (gamestart)
        {
            yield return new WaitForSeconds(0.9f);
            start.text = "";
        }
    }

    IEnumerator endmatch()
    {
        declarewinner();
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.75f);
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        ChangeScene.instance.ChangetoScene("MainUI");//change to EndOfMatch scene
    }

    void player1wins()
    {
        winner.text = "Player 1 Wins!";
        //winner.color = ;
    }

    void player2wins()
    {
        winner.text = "Player 2 Wins!";
        //winner.color = Color.red;
    }

    void declarewinner()
    {
        if(winner.text.Length == 0)//if both players be alive at the end
        {
            //who has more lives > who has more health > who has more parts > distance away from the center
            bool distcompare = (Mathf.Abs(P1.transform.position.x) - Mathf.Abs(P2.transform.position.x))<0;

            if ((p1_stocks - p2_stocks) > 0)
                player1wins();
            else if ((p1_stocks - p2_stocks) < 0)
                player2wins();
            else if ((P1.currentHealth - P2.currentHealth) > 0)
                player1wins();
            else if ((P1.currentHealth - P2.currentHealth) < 0)
                player2wins();
            else if ((P1.currnumparts - P2.currnumparts) > 0)
                player1wins();
            else if ((P1.currnumparts - P2.currnumparts) > 0)
                player2wins();
            else if (distcompare)
                player1wins();
            else
                player2wins();
        }
    }

    void Awake()
    {
        instance = this;

        countdown = MatchSettingsData.match_time + 1;
        p1_stocks = MatchSettingsData.stock_total;
        p2_stocks = MatchSettingsData.stock_total;
        p1Left.text = p1_stocks.ToString();
        p2Left.text = p2_stocks.ToString();

        inptmng = GameObject.Find("InputManager");
        keyinpt = inptmng.GetComponent<KeyInputManager>();
        continpt = inptmng.GetComponent<ControllerInputManager>();
        if(MatchSettingsData.mstrinptmng == "Keys")
        {
            continpt.enabled = false;
            keyinpt.enabled = true;
            primaryINPT = keyinpt;
            keys = true;
        }
        else
        {
            continpt.enabled = true;
            keyinpt.enabled = false;
            primaryINPT = continpt;
            keys = false;
        }
        stats = GameObject.Find("StatsManager").GetComponent<StatsManager>();

        P1.setTag("P1");
        P1.transform.position = P1spawnPoint.position;
        p1_origin = Instantiate(P1);//clone P1
        p1_origin.enabled = false;
        p1_origin.transform.position = new Vector3(-5, 245, 0);
        //p1_origin.rend.enabled = false;
         
        P2.setTag("P2");
        P2.transform.position = P2spawnPoint.position;
        p2_origin = Instantiate(P2);//clone P2
        p2_origin.enabled = false;
        p2_origin.transform.position = new Vector3(5, 245, 0);

        pauseMenu.SetActive(false);
        primaryINPT.lockcontrols();
    }

    // Update is called once per frame
    void Update ()
	{
        if(gamestart)
        {
		    DisplayCountdown();
            checkgameover();

		    //for testing input purposes
		    if(Input.GetKeyUp(KeyCode.Escape))
		    {
			    print("Toggled");
			    toggleInputs();
		    }
        }
        else
        {
            StartCoroutine(startmatch());
        }
	}
}
