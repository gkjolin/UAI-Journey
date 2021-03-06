﻿using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    public enum playerStates
    {
        idle = 0,
        left,
        right,
        jump,
        landing,
        falling,
        takingDMG,
        immortal,
        kill,
        resurrect,
        firingWeapon,
        _stateCount
    }
    
    // prodleva mezi stavy - pole velikosti stejne jako playerStates
    public static float[] stateDelayTimer = new float[(int)playerStates._stateCount];
    
    // delegat pro zmenu stavu hrace
    public delegate void playerStateHandler(PlayerStateController.playerStates newState);
    public static event playerStateHandler onStateChange;

    // komunikujeme s ostatnimi tridami pomoci delegata a eventu onStateChange
    public void Move(float input = 0f)
    {
        if (input != 0.0f)
        {
            if (input < 0.0f)
            {
                if (onStateChange != null)
                    onStateChange(PlayerStateController.playerStates.left);
            }
            else
            {
                if (onStateChange != null)
                    onStateChange(PlayerStateController.playerStates.right);
            }
        }
        else
        {
            if (onStateChange != null)
                onStateChange(PlayerStateController.playerStates.idle);
        }
    }

    public void Jump(float input = 0f)
    {
        if (input > 0.0f)
        {
            if (onStateChange != null)
                onStateChange(PlayerStateController.playerStates.jump);
        }
    }

    public void Fire(float input = 0f)
    {
        if (input > 0.0f)
        {
            if (onStateChange != null)
                onStateChange(PlayerStateController.playerStates.firingWeapon);
        }
    }

    
    
    void LateUpdate()
    {
// pokud sme na desktopu, muzeme ovladat pohyb klavesnici

#if UNITY_ANDROID
        Move(TouchInput.Input.Movement);
        Jump(TouchInput.Input.Jump);
        Fire(TouchInput.Input.Fire);   
#endif
     
#if UNITY_STANDALONE
        // detekuj defaultni input z Uniy a prirad ho do promenne
        float horizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        float firing = Input.GetAxis("Fire1");

        Move(horizontal);
        Jump(jump);
        Fire(firing);
#endif
     
#if UNITY_EDITOR
// detekuj defaultni input z Uniy a prirad ho do promenne
        float horizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        float firing = Input.GetAxis("Fire1");

        Move(horizontal);
        Jump(jump);
        Fire(firing);
#endif
        
    }
}



