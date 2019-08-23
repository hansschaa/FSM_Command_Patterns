using UnityEngine;

public abstract class FSMTransition 
{
    public readonly FSMState TargetState;

    public FSMTransition(FSMState targetState)
    {
        TargetState = targetState;
    }

    public abstract bool IsValid(EnemyBehaviour enemyBehaviour, float time = 0);
}
