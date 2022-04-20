using System.Collections;
using System.Collections.Generic;
public interface IEnemyAnimation 
{
    // below parameters are corresponding to the ones in Animator component
    public enum Parameters {
        IDLE, 
        TRACK,
        ATTACK, 
        DIE,
        IDLE_OR_TRACK,
    }

    void IdleAnim();
    void TrackAnim();
    void IdleOrTrackAnim();
    void AttackAnim();
    void DieAnim();
    void AnimationSetter<T>(string _name, T _condition);
}