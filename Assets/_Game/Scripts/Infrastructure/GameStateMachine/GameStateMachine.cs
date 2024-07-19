using System;
using System.Collections.Generic;
public class GameStateMachine
{
    private Dictionary<Type, IGameState> _registeredStates = new();
    private Type _currentState;

    public GameStateMachine()
    {
    }

    // Jesus wept
    public void AddGameState<TState, TConfig>(IGameState<TState, TConfig> gameState) 
        where TState : IGameState 
        where TConfig : IGameStateParams<TState>
    {
        _registeredStates[typeof(TState)] = gameState;
    }

    public void EnterState<TState,TConfig>(TConfig stateConfig)
        where TState : IGameState
        where TConfig : IGameStateParams<TState> 
    {
        ExitCurrentState(); 
        _currentState = typeof(TState);

        var stateRaw = _registeredStates[typeof(TState)];
        ((IGameState<TState, TConfig>)stateRaw).Enter(stateConfig);
    }

    private void ExitCurrentState()
    {
        if (_currentState != null)
            _registeredStates[_currentState].Exit();
    }

}
