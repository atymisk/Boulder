using UnityEngine;
using System.Collections;

public class MatchSettingsData : MonoBehaviour
{
    public static float match_time = 120;//2 min default
    public static int stock_total = 4;//4 stock default

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

	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
