using Game.Infrastructure;

public class GameState_Battle : IGameState<GameState_Battle, GameState_Battle_Params>
{
    private PlayerData _playerData;
    public GameState_Battle(PlayerData data)
    {

    }

    public void Enter(GameState_Battle_Params data)
    {

    }

    public void Enter()
    {

    }

    public void Exit()
    {
    }

}

public struct GameState_Battle_Params : IGameStateParams<GameState_Battle>
{
    public MapNodeData _mapNode;
}