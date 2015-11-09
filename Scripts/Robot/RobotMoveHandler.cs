using UnityEngine;
using System.Collections;

public class RobotMoveHandler : MonoBehaviour {

    //Public members
    public enum CharacterState { Idle, LightFlinch, HeavyFlinch, LeftPunch, RightPunch, LeftKick, RightKick };

    //Private members
    private CharacterState currentState;
    private Animator anim;

    // Use this for initialization
    void Start () {
        currentState = CharacterState.Idle;
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void LeftPunch(string punchName) {
        if (!IsBusy())
        {
            currentState = CharacterState.LeftPunch;
            anim.SetTrigger(punchName);
        }
    }

    public void OnMoveOrFlinchEnd() {
        currentState = CharacterState.Idle;
    }

    public bool IsBusy() {
        return currentState != CharacterState.Idle;
    }

    public CharacterState GetCurrentState() {
        return currentState;
    }
}
