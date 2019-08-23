using UnityEngine;

public class TimeTransition : FSMTransition
{
    private float _totalTime;
    public TimeTransition(FSMState targetState, float totalTime) : base(targetState)
    {
        _totalTime = totalTime;
    }

    public override bool IsValid(EnemyBehaviour enemyBehaviour, float time)
    {
        if (time > _totalTime)
            return true;

        return false;
    }
}
