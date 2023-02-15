using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    private Animator playerAnimator;

    public string currentState;
    public string playerIdle = "playerIdle";
    public string playerWalk = "playerWalk";
    public string playerJump = "playerJump";

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerVariables.isWalking && !playerVariables.isJumping)
        {
            PlayAnim(playerJump);
        }
    }

    public void PlayAnim(string newState){
        if (currentState == newState) return;
        playerAnimator.Play(newState);
        currentState = newState;
    }

}
