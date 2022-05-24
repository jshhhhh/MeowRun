using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// animation state controller
public class E_Intermediate_Anim : MonoBehaviour, IEnemyAnimation
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
        enemyState = E_Intermediate.current;
    }
    private void UpdateAnimation()
    {
        switch(enemyState) {
            case IEnemyBehavior.enemyState.Idle :
                if (this.gameObject.name.Contains("snake")) IdleOrTrackAnim();
                else IdleAnim();
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

