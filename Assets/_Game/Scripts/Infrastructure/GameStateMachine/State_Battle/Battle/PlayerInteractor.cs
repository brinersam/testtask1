using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class PlayerInteractor
{
    public bool TurnRequested => _turnCompletedTCS != null &&
        _turnCompletedTCS.Task.Status == TaskStatus.WaitingForActivation;

    private TaskCompletionSource<bool> _turnCompletedTCS;

    private PlayerTurnState _state;
    private readonly Battle _context;

    private Team _playerTeam;
    private PlayerHand _playerHand;
    private int _curCreatureIdx;
    private HashSet<Entity> _targetedEntities;

    private Entity _currentEntity => _playerTeam[_curCreatureIdx];


    public PlayerInteractor(Battle context, Team playerTeam, PlayerHand playerHand)
    {
        _context = context;
        _playerTeam = playerTeam;
        _playerHand = playerHand;
        _targetedEntities = new();
    }

    public void Click(PointerEventData eventData, IBattleClickInfo data)
    {
        if (_state == PlayerTurnState.Unavailable)
            return;

        if (data is BattleClickInfo_endturn endturn)
            ClickHandle(eventData, endturn);
        else if (data is BattleClickInfo_card card)
            ClickHandle(eventData, card);
        else if (data is BattleClickInfo_entity entity)
            ClickHandle(eventData, entity);
    }

    public async Task WaitForInputAsync()
    {
        await _turnCompletedTCS.Task;
    }

    public void BeginTurn()
    {
        if (_turnCompletedTCS == null || _turnCompletedTCS.Task.IsCompleted)
        {
            _turnCompletedTCS = new TaskCompletionSource<bool>();
            _curCreatureIdx = 0;
        }

        _playerHand.RenderHand(_playerHand.GetHandForEntity(_currentEntity));
        _state = PlayerTurnState.ChooseCard;
    }

    private void ClickHandle(PointerEventData eventData, BattleClickInfo_endturn data)
    {
        if (_state == PlayerTurnState.ChooseTargets)
        {
            _state = PlayerTurnState.ChooseCard;
            return;
        }

        if (_curCreatureIdx >= _playerTeam.entities.Length)
        {
            _curCreatureIdx = 0;
            _turnCompletedTCS.SetResult(true);
        }

        else
        {
            _curCreatureIdx++;
            BeginTurn();
        }
    }
    private async void ClickHandle(PointerEventData eventData, BattleClickInfo_card data)
    {
        if (_state != PlayerTurnState.ChooseCard)
            return;

        if (_state == PlayerTurnState.ChooseTargets)
            _state = PlayerTurnState.ChooseCard;

        if (data.Card.CardCost > _currentEntity.Energy.current)
            return; //todo blink energy bar or some other form of visual response

        foreach (EffectWithTargeter effect in data.Card.Effects)
        {
            if (!effect.maxTargets && effect.targetAmount > 0)
            {
                _state = PlayerTurnState.ChooseTargets;
                await SelectTargetsAsync(effect.targetAmount);
                
            }

            _context.ProcessEffect(_currentEntity, effect, _targetedEntities);

            _playerHand.DiscardCard(_currentEntity, data.Card);

            _state = PlayerTurnState.ChooseCard;
        }
    }
    private void ClickHandle(PointerEventData eventData, BattleClickInfo_entity data)
    {
        if (_state != PlayerTurnState.ChooseTargets)
            return;

        _targetedEntities.Add(data.Entity);
    }

    private async Task SelectTargetsAsync(int amount)
    {
        _targetedEntities.Clear();

        while (_targetedEntities.Count < amount || _state == PlayerTurnState.ChooseTargets)
        {
            await Task.Delay(200);
        }
    }
}


enum PlayerTurnState
{
    Unavailable,
    ChooseCard,
    ChooseTargets
}