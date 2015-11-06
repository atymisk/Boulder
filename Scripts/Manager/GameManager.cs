using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    /*
    This script should be managing the state of the game. ex: CharacterSelection, Match, Pause, etc
    Should also handle who is player 1 and who is player 2
    Managing the death and respawns of the players
    Managing the stocks and checking win condition
    */

    public Character P1;//possible issue, make a prefab of the player object maybe
    public Character P2;

    private Character p1_origin;
    private Character p2_origin;

    //needs to tell the input and camera manager that the player has died and respawned
    public InputManager inptmng;
    public CameraManager cmrmng;

    private int p1_stocks = 4;
    private int p2_stocks = 4;

    private bool gameover = false;
    private float respawntimer = 1.25f;
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

		Destroy(P1);
        p1_stocks--;
        inptmng.lockp1control();
        cmrmng.p1_is_dead();

        if(p1_stocks == 0)
        {
            gameover = true;
            print("gameover");
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

		Destroy(P2);
        p2_stocks--;
        inptmng.lockp2control();
        cmrmng.p2_is_dead();

        if (p2_stocks == 0)
        {
            gameover = true;
            print("gameover");
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
        P1.transform.position = P1.spawnPoint.position;
        P1.enabled = true;
        //give inputmanager and camera manager the new one
        inptmng.unlockp1control(P1);
        cmrmng.p1_respawn(P1);
    }

    IEnumerator p2respawn()
    {
        //create new Player2 object
        yield return new WaitForSeconds(respawntimer);
        P2 = Instantiate(p2_origin);
        P2.transform.position = P2.spawnPoint.position;
        P2.enabled = true;
        //give inputmanager and camera manager the new one
        inptmng.unlockp2control(P2);
        cmrmng.p2_respawn(P2);
    }

    // Use this for initialization
    void Awake ()
    {
        P1.setTag("P1");
        P1.transform.position = P1.spawnPoint.position;
        p1_origin = Instantiate(P1);//clone P1
        p1_origin.enabled = false;
        p1_origin.transform.position = new Vector3(-5,245,0);
        //p1_origin.rend.enabled = false;

        P2.setTag("P2");
        P2.transform.position = P2.spawnPoint.position;
        p2_origin = Instantiate(P2);//clone P2
        p2_origin.enabled = false;
        p2_origin.transform.position = new Vector3(5, 245, 0);
    }
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
