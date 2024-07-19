using System;
using System.Collections.Generic;
public class GameStateMachine : IGameStateManager
{
    private Dictionary<Type, IGameState> _registeredStates = new();
    private Type _baseState;
    private Type _currentState;

    public GameStateMachine()
    {
    }

    // Jesus wept
    public void AddGameState<T1, T2>(IGameState<T1, T2> gameState) 
        where T1 : IGameState 
        where T2 : IGameStateParams<T1>
    {
        _registeredStates[typeof(T1)] = gameState;
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

/// ///////////////////////
public class GameState_Map : IGameState<GameState_Map,GameState_Map_Params>
{
    private string _characterName;
    private string _characterHealth;
    public GameState_Map()
    {

    }

    public void Enter(GameState_Map_Params stateConfig)
    {
        _characterName = stateConfig._characterStats.health;
        _characterHealth = stateConfig._characterStats.name;
    }

    public void Exit()
    {
    }
}

public struct GameState_Map_Params : IGameStateParams<GameState_Map>
{
    public (string health,string name) _characterStats;
}
/// /////////////////////// 

public class GameState_Battle : IGameState<GameState_Battle, GameState_Battle_Params>
{
    private PlayerData _playerData;
    public GameState_Battle()
    {

    }

    public void Enter(GameState_Battle_Params data)
    {
        _playerData = data.playerData;
        Entity playerEntity = data.playerData.Entity;
        SOEntity pCharacter = playerEntity.EntityData;
        string DEBUGREADOUT = $"{pCharacter.name} begins to fight in room {data.roomData}!";
        playerEntity.DEBUGDealDamage(5);

    }

    public void Exit()
    {
        Entity playerEntity = _playerData.Entity;
        SOEntity pCharacter = playerEntity.EntityData;
        string DEBUGREADOUT = $"{pCharacter.name} emerges victorious at {playerEntity.DEBUGReadHealth()}!";
    }

}

public struct GameState_Battle_Params : IGameStateParams<GameState_Battle>
{
    public PlayerData playerData;
    public string roomData;
}
/// ///////////////////////

public interface IGameState 
{
    void Exit();
}

public interface IGameState<TState, TConfig> : IGameState
    where TConfig : IGameStateParams<TState>
    where TState : IGameState
{
    void Enter(TConfig stateConfig);
}

public interface IGameStateParams<State>
    where State : IGameState
{
}


