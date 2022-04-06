using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController instance;

    private Animator _anim;

    private bool _move = false;
    private bool _slowRun = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            this.enabled = false;

        _anim = GetComponent<Animator>();
    }

    //Called by PLayerController when player moves
    public bool run
    {
        get { return _move; }

        set
        {
            _move = value;
            _anim.SetBool("Move", _move);
        }
    }

    //Called by PLayerController when player moves slower
    public bool slowedMovement
    {
        get { return _slowRun; }

        set
        {
            _slowRun = value;
            _anim.SetBool("SlowRun", _slowRun);
        }
    }

    //Called by questions when player loses money. It plays upset animation
    public void LoseMoney()
    {
        _anim.SetTrigger("Fail");
    }

    //Called by questions when player earns money. It plays happy animation
    public void EarnMoney()
    {
        _anim.SetTrigger("Success");
    }

    //Called at the end of the game while player's being lifted up
    public void Dance()
    {
        _anim.SetTrigger("Dance");
    }
}
