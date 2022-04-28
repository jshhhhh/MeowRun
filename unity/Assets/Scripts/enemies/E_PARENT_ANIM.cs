using System.Collections;
using System.Collections.Generic;
public interface IEnemyAnimation 
{
    void IdleAnim();
    void TrackAnim();
    void AttackAnim();
    void DieAnim();
    void AnimationSetter<T>(string _name, T _condition);
}