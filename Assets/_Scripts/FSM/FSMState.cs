using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place the labels for the States in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public abstract class FSMState
{
    protected readonly List<FSMTransition> Transitions;
    protected float TimeState;
    
    public readonly StateID StateId;
    public StateID Id => StateId;
    
    public readonly EnemyBehaviour EnemyBehaviour;

    public FSMState(EnemyBehaviour enemyBehaviour, StateID stateId)
    {
        this.EnemyBehaviour = enemyBehaviour;
        StateId = stateId;
        Transitions = new List<FSMTransition>();
    }

    public void AddTransition(FSMTransition trans)
    {
        if (trans.TargetState == null)
        {
            Debug.LogError("FSMState ERROR: target state null is not allowed");
            return;
        }

        // Since this is a Deterministic FSM,
        // check if the current transition was already inside the map
        if (Transitions.Contains(trans))
        {
            Debug.LogError("FSMState ERROR: State " + StateId.ToString() + " already has transition " + trans.ToString() +
                           "Impossible to assign to another state");
            return;
        }

        Transitions.Add(trans);
    }

    public void DeleteTransition(FSMTransition trans)
    {
        // Check for NullTransition
        if (trans == null)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (Transitions.Contains(trans))
        {
            Transitions.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + StateId.ToString() +
                       " was not on the state's transition list");
    }

    /// <summary>
    /// This method is used to set up the State condition before entering it.
    /// It is called automatically by the FSMSystem class before assigning it
    /// to the current state.
    /// </summary>
    public virtual void DoBeforeEntering() { }

    /// <summary>
    /// This method is used to make anything necessary, as reseting variables
    /// before the FSMSystem changes to another one. It is called automatically
    /// by the FSMSystem before changing to a new state.
    /// </summary>
    public virtual void DoBeforeLeaving() { }

    /// <summary>
    /// This method decides if the state should transition to another on its list
    /// NPC is a reference to the object that is controlled by this class
    /// </summary>
    public virtual FSMTransition CheckTransitions()
    {
        foreach (FSMTransition transition in Transitions)
            if(transition.IsValid(EnemyBehaviour))
                return transition;

        return null;
    }

    /// <summary>
    /// This method controls the behavior of the NPC in the game World.
    /// Every action, movement or communication the NPC does should be placed here
    /// NPC is a reference to the object that is controlled by this class
    /// </summary>
    public abstract void Act();
}
