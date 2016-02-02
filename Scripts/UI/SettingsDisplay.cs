using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsDisplay : MonoBehaviour
{
    public Text time;
    public Text lives;
    public Text control;
    string timeMinSec;

    public void displayTime()
    {
        int minutes = (int)(MatchSettingsData.match_time / 60);
        timeMinSec = minutes + ":00";
    }

	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        displayTime();
        time.text = timeMinSec;
        lives.text = MatchSettingsData.stock_total.ToString();
        control.text = MatchSettingsData.mstrinptmng;
	}
}
