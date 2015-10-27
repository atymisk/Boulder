using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	// Use this for initialization
	public Character playerOne;
	public Character playerTwo;

    //set up this code to handle 4 different events:
    //p1 and p2 are alive
    //p1 is alive, p2 is dead
    //p2 is alive, p1 is dead
    //both are dead

    private bool p1dead = false;
    private bool p2dead = false;

    public void p1_is_dead()
    {
        p1dead = true;
    }

    public void p2_is_dead()
    {
        p2dead = true;
    }

    public void p1_respawn(Character newp1)
    {
        playerOne = newp1;
        p1dead = false;
    }

    public void p2_respawn(Character newp2)
    {
        playerTwo = newp2;
        p2dead = false;
    }

    void Start () {
		
	}

	void SetCameraPos()
    {
        Vector3 middle = new Vector3(0,1.5f,0); //hardcode value for now to represent if both players are dead
        if (!p1dead && !p2dead)//neither are dead
        {
            //aim camera at both
            middle = (playerOne.transform.position + playerTwo.transform.position) * 0.5f;
        }
        else if(p1dead && !p2dead)
        {
            //aim camera at p2
            middle = playerTwo.transform.position;
        }
        else if (!p1dead && p2dead)
        {
            //aim camera at p1
            middle = playerOne.transform.position;
        }
        /*else// if(p1dead && p2dead)//both are dead
        {
            //aim camera at stage center
        }*/
		GetComponent<Camera>().transform.position = new Vector3(
			middle.x,
			middle.y,
			GetComponent<Camera>().transform.position.z
			);
	}
	
	void SetCameraSize()
    {
		float minSizeY = 40;
		//horizontal size is based on actual screen ratio
		float minSizeX = minSizeY * Screen.width / Screen.height;

        float width = 1;//default width
        float height = 1;//default height

        if(!p1dead && !p2dead)//both alive
        {
            //multiplying by 0.5, because the ortographicSize is actually half the height
            width = Mathf.Abs(playerOne.transform.position.x - playerTwo.transform.position.x) * 0.5f + 30;
            height = Mathf.Abs(playerOne.transform.position.y - playerTwo.transform.position.y) * 0.5f;
        }
        else if (!p1dead && p2dead)//p2 alive
        {
            width = Mathf.Abs(playerOne.transform.position.x) * 0.5f + 30;
            height = Mathf.Abs(playerOne.transform.position.y) * 0.5f;
        }
        else if (p1dead && !p2dead)//p1 alive
        {
            width = Mathf.Abs(playerTwo.transform.position.x) * 0.5f + 30;
            height = Mathf.Abs(playerTwo.transform.position.y) * 0.5f;
        }/*
        else if(p1dead && p2dead)//both dead
        {
            width = Mathf.Abs(playerTwo.transform.position.x) * 0.5f + 30;
            height = Mathf.Abs(playerTwo.transform.position.y) * 0.5f;
        }*/

		//computing the size
		float camSizeX = Mathf.Max(width, minSizeX);
		GetComponent<Camera>().orthographicSize = Mathf.Max(height,
		                                                    camSizeX * Screen.height / Screen.width, minSizeY);
		
	}
	
	// Update is called once per frame
	void Update () {
		SetCameraPos();
		SetCameraSize();
	}
}
