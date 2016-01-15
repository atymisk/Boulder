using UnityEngine;
using System.Collections;

public class MatchSettingsData : MonoBehaviour
{
    public static float match_time = 120;//2 min default
    public static int stock_total = 4;//4 stock default

    void increaseTime()
    {
        match_time += 60;
    }
    
    void decreaseTime()
    {
        match_time -= 60;
    }

    void increaseStocks()
    {
        stock_total++;
    }

    void decreaseStocks()
    {
        stock_total--;
    }

	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
