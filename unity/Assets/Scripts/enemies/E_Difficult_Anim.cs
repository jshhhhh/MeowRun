using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// animation state controller
public class E_Difficult_Anim : MonoBehaviour, IEnemyAnimation
{    
    private IEnemyBehavior.enemyState enemyState;
    private E_Difficult difficultEnemy;
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
        enemyState = E_Difficult.current;
    }
    private void UpdateAnimation()
    {
        switch(enemyState) {
            case IEnemyBehavior.enemyState.Idle :
                // diffculty enemy has IdleOrTrack state
                IdleOrTrackAnim();
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
            default : 
                IdleOrTrackAnim();
                break;
        }
    }

    // ============== IEnemyAnimation implementation ============== // 
    public void IdleAnim() {
        AnimationSetter(IEnemyAnimation.Parameters.IDLE.ToString(), true);
    }
    public void TrackAnim() {
        AnimationSetter(IEnemyAnimation.Parameters.TRACK.ToString(), true);
    }
    public void IdleOrTrackAnim() {
        AnimationSetter(IEnemyAnimation.Parameters.IDLE_OR_TRACK.ToString(), true);
    }
    public void AttackAnim() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AnimationSetter(IEnemyAnimation.Parameters.ATTACK.ToString(), true);
        } 

        if (!Input.GetKeyDown(KeyCode.Space)) {
            AnimationSetter(IEnemyAnimation.Parameters.ATTACK.ToString(), false);
        }
    }
    public void DieAnim() {
        AnimationSetter(IEnemyAnimation.Parameters.DIE.ToString(), true);
    }
    // ============== IEnemyAnimation implementation ============== // 


    public void AnimationSetter<T>(string _name, T _condition)
    {
        if (_condition.GetType() == typeof(bool))
        {
            animator.SetBool(_name, bool.Parse(_condition.ToString()));
        }
    }
}

