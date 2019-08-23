using UnityEngine;
using System.Collections;
public class WalkState : FSMState
{
    private float _velocity;
    private static readonly int HorizontalVel = Animator.StringToHash("HorizontalVel");

    public WalkState(EnemyBehaviour enemyBehaviour, StateID stateId, float velocity) : base(enemyBehaviour, stateId)
    {
        _velocity = velocity;
    }

    public override void Act()
    {
        TimeState += Time.deltaTime;
        EnemyBehaviour.rigidbody.velocity = new Vector2( _velocity * EnemyBehaviour.transform.localScale.x , 
            EnemyBehaviour.rigidbody.velocity.y );
    }
    
    public override FSMTransition CheckTransitions()
    {
        foreach (FSMTransition transition in Transitions)
            if(transition.IsValid(EnemyBehaviour, TimeState))
                return transition;

        return null;
    }

    public override void DoBeforeEntering()
    {
        TimeState = 0;
        EnemyBehaviour.FlipSprite();
        EnemyBehaviour.animator.SetInteger(HorizontalVel,1);

    }
}
