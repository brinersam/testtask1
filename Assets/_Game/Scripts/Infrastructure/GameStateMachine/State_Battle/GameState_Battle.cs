using Game.Infrastructure;
using System.Collections.Generic;
using System.Linq;

public class GameState_Battle : IGameState<GameState_Battle, GameState_Battle_Params>, IBattleModel, IGameStateRendererUser
{
    private GameStateMachine _gsm;
    private IGameStateRenderer _renderer;
    private PlayerData _playerData;
    private Battle _currentBattle;
    public GameState_Battle(GameStateMachine gsm, PlayerData data)
    {
        _gsm = gsm;
        _playerData = data;
    }

    public void RegisterRenderer(IGameStateRenderer renderer)
    {
        _renderer = renderer;
    }

    public void Enter(GameState_Battle_Params data)
    {
        IBattleRenderer battleRenderer = _renderer as IBattleRenderer;

        Entity[] enemyTeamEnts = data._mapNode.EntitySpawners[0].Spawn(isEnemy: true).ToArray();

        Team teamAI = new Team(new AiDriver(), enemyTeamEnts);
        Team teamPlayer = new Team(new PlayerDriver(), new Entity[] { _playerData.Entity });

        TurnQueue turnQueue = new TurnQueue();

        turnQueue.TurnStage_Choose(teamAI).
                TurnStage_ActChoose(teamPlayer).
                TurnStage_Act(teamAI);

        _currentBattle = new Battle(new Team[] { teamPlayer, teamAI },
                                    turnQueue,
                                    battleRenderer);

        _renderer.Render();
        _currentBattle.PlayATurn();
    }

    public void Exit()
    {
        var battleReport = _currentBattle.GetBattleReport();
        _currentBattle = null;
        _renderer.Hide();
    }

    public void HandleClick()
    {
        _currentBattle.HandleClick();
    }

    public IEnumerable<Card> GetPlayerDeckDEBUG()
    {
        return _playerData.Entity.Deck;
    }
}

public struct GameState_Battle_Params : IGameStateParams<GameState_Battle>
{
    public MapNodeData _mapNode;
}