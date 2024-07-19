using Game.Infrastructure;

public class GameState_Battle : IGameState<GameState_Battle, GameState_Battle_Params>, IBattleModel, IGameStateRendererUser
{
    private GameStateMachine _gsm;
    private IGameStateRenderer _renderer;
    private PlayerData _playerData;
    public GameState_Battle(GameStateMachine gsm, PlayerData data)
    {

    }

    public void Enter(GameState_Battle_Params data)
    {
        _renderer.Render();
    }

    public void Enter()
    {
        Enter(new()); // add debug room or something to handle this
    }

    public void Exit()
    {
        _renderer.Hide();
    }

    public void RegisterRenderer(IGameStateRenderer renderer)
    {
        _renderer = renderer;
    }
}

public struct GameState_Battle_Params : IGameStateParams<GameState_Battle>
{
    public MapNodeData _mapNode;
}