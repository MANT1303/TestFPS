using System;
using System.Collections.Generic;

public class StateMachine
{
    private IState _currentState;
    private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();


    public void AddState(IState state)
    {
        _states.Add(state.GetType(), state);
    }
    public void SetState<T>() where T : IState
    {
        if (_currentState != null && _currentState.GetType() == typeof(T))
        {
            return;
        }
        if (_states.TryGetValue(typeof(T), out IState newState))
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

    }
    public void Update()
    {
        _currentState?.Update();   
    }
}
