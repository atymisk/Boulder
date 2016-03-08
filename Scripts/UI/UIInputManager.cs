using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInputManager : MonoBehaviour
{
    //public enum SceneName { MAIN, SETTING, STAGE };
    public Button[] btnlist;
    public static int index = 0;
	private System.Diagnostics.Stopwatch stopwatch;
	private bool canChange = false;
	// Use this for initialization
	void Start ()
    {
		stopwatch = new System.Diagnostics.Stopwatch ();
		//btnlist[index].onClick.Invoke();
		stopwatch.Start ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (stopwatch.ElapsedMilliseconds > 250 && canChange) {
			canChange = false;
			stopwatch.Reset();
		}

        if(index != 3 && !canChange)
        {
            Button[] minilist = btnlist[index].GetComponentsInChildren<Button>();
	        if(Input.GetKeyDown(KeyCode.A) || Input.GetAxis("LeftJoystickX_P2") < 0 || Input.GetAxis("LeftJoystickX_P1") < 0)
            {
                minilist[1].onClick.Invoke();
				canChange = true;
				stopwatch.Reset();
				stopwatch.Start();
            }
            if(Input.GetKeyDown(KeyCode.D) || Input.GetAxis("LeftJoystickX_P2") > 0 || Input.GetAxis("LeftJoystickX_P1") > 0)
            {
                minilist[2].onClick.Invoke();
				canChange = true;
				stopwatch.Reset();
				stopwatch.Start();
            }

        }
	}
}
