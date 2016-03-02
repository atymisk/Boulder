using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	// Use this for initialization
	public Robot playerOne;
	public Robot playerTwo;

    public float widthmod = 90;
    public float posheightmod = 50;
    public float negheightmod = 30;

    //set up this code to handle 4 different events:
    //p1 and p2 are alive
    //p1 is alive, p2 is dead
    //p2 is alive, p1 is dead
    //both are dead

    private bool p1dead = false;
    private bool p2dead = false;

    private bool p1win = false;
    private bool p2win = false;

    public void p1_is_dead()
    {
        p1dead = true;
    }

    public void p2_is_dead()
    {
        p2dead = true;
    }

    public void p1wins()
    {
        p1win = true;
        p2win = false;
    }
    public void p2wins()
    {
        p2win = true;
        p1win = false;
    }

    public void p1_respawn(Robot newp1)
    {
        playerOne = newp1;
        p1dead = false;
    }

    public void p2_respawn(Robot newp2)
    {
        playerTwo = newp2;
        p2dead = false;
    }

    void Start ()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    Vector3 GetCameraPos()
    {
        //current/original position
        //Vector3 originpos = Camera.main.transform.position;
        Vector3 middle = new Vector3(0, 1.5f, -10); //hardcode value for now to represent if both players are dead
        if (!p1dead && !p2dead)//neither are dead
        {
            //aim camera at both
            middle = (playerOne.transform.position + playerTwo.transform.position) * 0.5f;
        }
        else if (p1dead && !p2dead || p2win)
        {
            //aim camera at p2
            middle = playerTwo.transform.position;
            //middle = originpos;
        }
        else if (!p1dead && p2dead || p1win)
        {
            //aim camera at p1
            middle = playerOne.transform.position;
            //middle = originpos;
        }

        return new Vector3(middle.x, middle.y+10,middle.z);
	}
	
	float GetCameraSize()//affects the zoom
    {
		float minSizeY = 30;

        float width = 1;//default width
        float height = 1;//default height

        if(!p1dead && !p2dead)//both alive
        {
            //multiplying by 0.5, because the ortographicSize is actually half the height
            width = Mathf.Abs(playerOne.transform.position.x - playerTwo.transform.position.x) * 0.75f + 30;
            height = Mathf.Abs(playerOne.transform.position.y - playerTwo.transform.position.y) * 0.99f;
            if(width < 46 && height < 8)
            {
                minSizeY = 22;//lerp this
            }
        }
        else if (!p1dead && p2dead)//p2 alive
        {
            width = Mathf.Abs(playerOne.transform.position.x) * ((p1win || p2win) ? (0.5f):(1.5f)) +30;
            height = Mathf.Abs(playerOne.transform.position.y) * ((p1win || p2win) ? (0.5f) : (1.5f));
        }
        else if (p1dead && !p2dead)//p1 alive
        {
            width = Mathf.Abs(playerTwo.transform.position.x) * ((p1win || p2win) ? (0.5f) : (1.5f)) + 30;
            height = Mathf.Abs(playerTwo.transform.position.y) * ((p1win || p2win) ? (0.5f) : (1.5f));
        }

        if(p1win || p2win)
        {
            minSizeY = 20;
        }

		float minSizeX = minSizeY * Screen.width / Screen.height;//horizontal size is based on actual screen ratio

		//computing the size: get the bigger of the two values for more zoom
		float camSizeX = Mathf.Max(width, minSizeX);
        float orthosize = Mathf.Clamp(Mathf.Max(height, camSizeX * Screen.height / Screen.width, minSizeY),minSizeY,60);
        return orthosize;
	}

	// Update is called once per frame
	void Update ()
    {
		Vector3 target = GetCameraPos();
        float speed = 2.5f;//((p1win||p2win)? 10 : 2.5f);
        Vector3 newpos = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);//Lerp the camera
        transform.position = new Vector3(newpos.x, newpos.y, -10);
		float targetsize = GetCameraSize();
        Camera.main.orthographicSize =  Mathf.Lerp(Camera.main.orthographicSize, targetsize, Time.deltaTime*speed);
    }
}
