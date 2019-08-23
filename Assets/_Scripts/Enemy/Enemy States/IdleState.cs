using UnityEngine;

public class IdleState : FSMState
{
    private static readonly int HorizontalVel = Animator.StringToHash("HorizontalVel");

    public IdleState(EnemyBehaviour npc, StateID stateId) : base(npc, stateId)
    {
        
    }

    public override void Act()
    {
        TimeState += Time.deltaTime;
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
        EnemyBehaviour.animator.SetInteger(HorizontalVel,0);
    }
}
