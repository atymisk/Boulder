using UnityEngine;
using System.Collections;

public class MatchSettingsData : MonoBehaviour
{
    public static float match_time = 120;//2 min default
    public static int stock_total = 4;//4 stock default
    public static string mstrinptmng = "Keys";
    //private static string[] inpt_types = {"Keys", "Controller", "P1 Controller/P2 Keys", "P1 Keys/P2 Controller"};
    public void increaseTime()
    {
        match_time += 60;
        if(match_time >= (60*24))
        {
            match_time = 60;//loopback
        }
    }
    
    public void decreaseTime()
    {
        match_time -= 60;
        if(match_time <= 0)
        {
            match_time = (60*24);//loopback
        }
    }

    public void increaseStocks()
    {
        stock_total++;
        if(stock_total >= 99)
        {
            stock_total = 1;
        }
    }

    public void decreaseStocks()
    {
        stock_total--;
        if(stock_total <= 0)
        {
            stock_total = 99;
        }
    }

    public void changeinptsetting()
    {
        if(mstrinptmng == "Keys")
        {
            mstrinptmng = "Controller";
        }
        else
        {
            mstrinptmng = "Keys";
        }
    }

	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
