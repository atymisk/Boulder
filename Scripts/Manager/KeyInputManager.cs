using UnityEngine;
using System.Collections;

public class KeyInputManager : MonoBehaviour {

    // Use this for initialization
    public Character playerOne;
    public Character playerTwo;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Temporary implementation used for testing-------------------------------------

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
            playerOne.NormalMoveAlpha();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerOne.NormalMoveBeta();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerOne.SpecialMoveAlpha();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerOne.RocketPunch();
        }
        ///--------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerTwo.FaceRight();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            playerTwo.FaceLeft();
        }

        if (Input.GetKey(KeyCode.J))
        {
            playerTwo.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.L))
        {
            playerTwo.MoveRight();
        }
        else
        {
            playerTwo.StayStill();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            playerTwo.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            playerTwo.NormalMoveAlpha();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            playerTwo.NormalMoveBeta();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            playerTwo.SpecialMoveAlpha();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerTwo.RocketPunch();
        }
    }
}
