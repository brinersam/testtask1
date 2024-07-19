public interface IGameState
{
    void Exit();
    void Enter();
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