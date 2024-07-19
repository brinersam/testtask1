public class GameState_Map : IGameState<GameState_Map, GameState_Map_Params>
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
    public void Enter()
    {

    }

    public void Exit()
    {
    }
}

public struct GameState_Map_Params : IGameStateParams<GameState_Map>
{
    public (string health, string name) _characterStats;
}
