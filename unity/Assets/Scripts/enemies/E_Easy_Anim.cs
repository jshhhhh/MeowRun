using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// animation state controller
public class E_Easy_Anim : MonoBehaviour, IEnemyAnimation
{
    private IEnemyBehavior.enemyState enemyState;
    private Animator animator; // Animator has parameters in it, which set in Unity Editor
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateState();
        UpdateAnimation();
    }

    private void UpdateState()
    {
        enemyState = E_Easy.current;
    }
    private void UpdateAnimation()
    {
        switch(enemyState) {
            case IEnemyBehavior.enemyState.Idle :
                IdleAnim();
                break;
            case IEnemyBehavior.enemyState.Track :
                TrackAnim();
                break;
            case IEnemyBehavior.enemyState.Fire :
                AttackAnim();
                break;
            case IEnemyBehavior.enemyState.Die :
                DieAnim();
                break;
        }
    }

    // ============== IEnemyAnimation implementation ============== // 
    public void IdleAnim() {
        AnimationSetter(
            IEnemyAnimation.Parameters.IDLE.ToString(), 
            IEnemyAnimation.Parameters.IDLE
        );    
    }
    public void TrackAnim() {
        AnimationSetter(
            IEnemyAnimation.Parameters.TRACK.ToString(), 
            IEnemyAnimation.Parameters.TRACK
        );  
    }

    public void AttackAnim() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AnimationSetter(
                IEnemyAnimation.Parameters.ATTACK.ToString(), 
                IEnemyAnimation.Parameters.ATTACK
            );
        }
    }
    public void DieAnim() {
        AnimationSetter(
            IEnemyAnimation.Parameters.DIE.ToString(), 
            IEnemyAnimation.Parameters.DIE
        );
    }
    public void IdleOrTrackAnim() { 
        // easy type enemey doesn't have IdleOrTrack animation.
    }
    // ============== IEnemyAnimation implementation ============== // 


    public void AnimationSetter<T>(string _name, T _condition)
    {
        if (_condition.GetType() == typeof(int))
        {
            animator.SetInteger(_name, (int.Parse(_condition.ToString())));
        }
    }
}

