using UnityEngine;
using System.Collections;

public class MoveEventHandler : MonoBehaviour 
{
    //Public members
    public enum CharacterState { Idle, LightFlinch, HeavyFlinch, MoveA, MoveB, MoveC, MoveD};

	public int playerNum;

    //Private members
    private CharacterState currentState;
	private bool onNormalAlpha = false;
	private bool onNormalBeta = false;
	private bool onSpecialAlpha = false;
	private bool onLightHit = false;
	private bool onHeavyHit = false;
	private Animator anim;

	void Start () 
	{
        currentState = CharacterState.Idle;
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	//Events
    public void OnIdleStart()
    {
        //currentState = CharacterState.Idle;
    }

	public void OnNormalAlphaStart()
	{
        if(!IsBusy())
        {
            currentState = CharacterState.MoveA;
            anim.SetTrigger("OnMoveA");
        }
	}

	public void OnNormalBetaStart()
	{
        if (!IsBusy())
        {
            currentState = CharacterState.MoveB;
            anim.SetTrigger("OnMoveB");
        }
            
    }
	
	public void OnSpecialAlphaStart()
	{
        if (!IsBusy())
        {
            currentState = CharacterState.MoveC;
            anim.SetTrigger("OnMoveC");
        }
            
    }

    public void OnNormalDethaStart()
    {
        if(!IsBusy())
        {
            currentState = CharacterState.MoveD;
            anim.SetTrigger("OnMoveD");
        }
    }

	public void OnLightHitStart()
	{
        currentState = CharacterState.LightFlinch;
        anim.SetTrigger("OnTriggerLightHit");
    }
    
	public void OnHeavyHitStart()
	{
        currentState = CharacterState.HeavyFlinch;
        anim.SetTrigger("OnTriggerHeavyHit");
	}

    public void OnForceIdle()
    {
        currentState = CharacterState.Idle;
        anim.SetTrigger("ForceIdle");
    }

    public void OnMoveOrFlinchEnd()
    {

        
        currentState = CharacterState.Idle;
    }

	//---
	public bool IsBusy()
	{
        return currentState != CharacterState.Idle;
	}

    public CharacterState GetCurrentState()
    {
        return currentState;
    }
}
