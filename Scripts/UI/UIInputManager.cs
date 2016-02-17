using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInputManager : MonoBehaviour
{
    //public enum SceneName { MAIN, SETTING, STAGE };
    public Button[] btnlist;
    public static int index = 0;
	// Use this for initialization
	void Start ()
    {
        //btnlist[index].onClick.Invoke();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(index != 3)
        {
            Button[] minilist = btnlist[index].GetComponentsInChildren<Button>();
	        if(Input.GetKeyDown(KeyCode.A) || Input.GetAxis("LeftJoystickX_P2") < 0 || Input.GetAxis("LeftJoystickX_P1") < 0 || Input.GetAxis("DPadX_P1") < 0)
            {
                minilist[1].onClick.Invoke();
            }
            if(Input.GetKeyDown(KeyCode.D) || Input.GetAxis("LeftJoystickX_P2") > 0 || Input.GetAxis("LeftJoystickX_P1") > 0 || Input.GetAxis("DPadX_P1") > 0)
            {
                minilist[2].onClick.Invoke();
            }

        }
	}
}
