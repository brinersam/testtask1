public interface IGameStateEnterable<TState> : IGameState
    where TState : IGameState
{
    void Enter();
}

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