using UnityEngine;
using System.Collections;

public class RobotTestInputs : MonoBehaviour {
    public Robot playerOne;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerOne.FaceRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerOne.FaceLeft();
        }


        if (Input.GetKey(KeyCode.D))
        {
            playerOne.MoveRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerOne.MoveLeft();
        }
        else
        {
            playerOne.StayStill();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerOne.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerOne.LeftPunch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerOne.RightKick();
        }
    }


}
