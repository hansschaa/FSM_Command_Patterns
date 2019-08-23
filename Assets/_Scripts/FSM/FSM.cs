using System.Collections.Generic;
using UnityEngine;

public enum StateID
{
    NullStateID = 0, // Use this ID to represent a non-existing State in your system	
    NearHitState = 1,
    FarHitState = 2,
    PatrolState = 3,
    IdleState = 4,
    ChaseState = 5,
    ReturnState = 6,
    BackState = 7,
    ThrowSpikes = 8,
    JumpState = 9,
    LostPlayerState = 10,
    FallDownState = 11,
    RunForward = 12,
}

public class FSM 
{
    private readonly List<FSMState> states;
    private FSMState _currentState;
    private EnemyBehaviour _owner;

    public FSM(EnemyBehaviour owner)
    {
        _owner = owner;
        states = new List<FSMState>();
    }

    /// <summary>
    /// This method places new states inside the FSM,
    /// or prints an ERROR message if the state was already inside the List.
    /// First state added is also the initial state.
    /// </summary>
    public void AddState(FSMState s)
    {
        // Check for Null reference before deleting
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed: ");
            return;
        }
        
        if (states.Count == 0)
        {
            states.Add(s);
            _currentState = s;
            _owner.UpdateStateView(_currentState);
            return;
        }

        // Add the state to the List if it's not inside it
        foreach (FSMState state in states)
        {
            if (state.Id == s.Id)
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.Id +
                               " because state has already been added");
                return;
            }
        }
        
        states.Add(s);
    }

    /// <summary>
    /// This method delete a state from the FSM List if it exists, 
    ///   or prints an ERROR message if the state was not on the List.
    /// </summary>
    public void DeleteState(StateID id)
    {
        // Check for NullState before deleting
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
            return;
        }

        // Search the List and delete the state if it's inside it
        foreach (FSMState state in states)
        {
            if (state.Id == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                       ". It was not on the list of states");
    }

    /// <summary>
    /// This method tries to change the state the FSM is in based on
    /// the current state and the transition passed. If current state
    ///  doesn't have a target state for the transition passed, 
    /// an ERROR message is printed.
    /// </summary>
    public void DoTransition(FSMState targetState)
    {
        _currentState.DoBeforeLeaving();
        _currentState = targetState;
        _owner.UpdateStateView(_currentState);
        _currentState.DoBeforeEntering();
    } 

    public virtual void FSMUpdate()
    {
        FSMTransition transition = _currentState.CheckTransitions();

        if (transition != null)
            DoTransition(transition.TargetState);

        else
            _currentState.Act();
    }
}
