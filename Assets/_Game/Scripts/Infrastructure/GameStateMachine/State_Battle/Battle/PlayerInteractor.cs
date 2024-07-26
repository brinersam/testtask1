using Game.Infrastructure;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class PlayerInteractor
{
    public bool TurnRequested => _turnCompletedTCS != null &&
        _turnCompletedTCS.Task.Status == TaskStatus.WaitingForActivation;

    private TaskCompletionSource<bool> _turnCompletedTCS;
    private PlayerTurnState _state;
    private readonly Battle _context;

    private Card _lastCardSelected;
    private PlayerData _playerData;


    public PlayerInteractor(Battle context, PlayerData playerData)
    {
        _context = context;
        _playerData = playerData;
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
        _turnCompletedTCS = new TaskCompletionSource<bool>();
        _state = PlayerTurnState.ChooseCard;
    }

    private void ClickHandle(PointerEventData eventData, BattleClickInfo_endturn data)
    {
        _turnCompletedTCS.SetResult(true);
    }
    private void ClickHandle(PointerEventData eventData, BattleClickInfo_card data)
    {
        if (_state != PlayerTurnState.ChooseCard)
            return;

        if (data.Card.CardCost > _playerData.Entity.Energy.current)
            return; //todo blink energy bar

        foreach (SOCardEffect effect in data.Card.Effects)
        {
            // if effect needs to have targets
            //await select targets

            _context.ProcessEffect(_playerData.Entity, effect);
        }

    }
    private void ClickHandle(PointerEventData eventData, BattleClickInfo_entity data)
    {
        if (_state != PlayerTurnState.ChooseTargets)
            return;


    }
}


enum PlayerTurnState
{
    Unavailable,
    ChooseCard,
    ChooseTargets
}