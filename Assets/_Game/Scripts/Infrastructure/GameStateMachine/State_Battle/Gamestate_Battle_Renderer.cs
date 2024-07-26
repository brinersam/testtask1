using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Gamestate_Battle_Renderer : MonoBehaviour, IGameStateRenderer, IBattleRenderer
{
    [SerializeField] private Transform _UI;

    [Header("Cards")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _playerHandContainer;
    private List<CardVisual> _visualCardsInHand = new();

    [Header("Entities")]
    [SerializeField] private GameObject _entityPrefab;
    [SerializeField] private Transform _entitiesGOLeftContainer;
    private List<EntityVisual> _visualChildrenLeft = new();

    [SerializeField] private Transform _entitiesGORightContainer;
    private List<EntityVisual> _visualChildrenRight = new();

    //Mid battle used fields
    private Team _teamLeft;
    private Team _teamRight;
    public Team TeamLeft { get => _teamLeft; set => _teamLeft = value; }
    public Team TeamRight { get => _teamRight; set => _teamRight = value; }

    private IBattleModel _model;

    [Inject] DiContainer _container;
    [Inject]
    void Init(IBattleModel model)
    {
        _model = model;
        model.RegisterRenderer(this);
    }

    public void Render()
    {
        _UI.gameObject.SetActive(true);

        RenderTeam(_visualChildrenLeft, _teamLeft, _entitiesGOLeftContainer);
        RenderTeam(_visualChildrenRight, _teamRight, _entitiesGORightContainer);
        //RenderCards(_model.GetPlayerDeckDEBUG().ToList());
    }

    private void RenderTeam(List<EntityVisual> gobjectsList, Team team, Transform parentTransform)
    {
        if (team.entities.Length <= 0)
            return;

        int currentChildIdx = 0;
        int childrenCount = gobjectsList.Count;

        // Iterate over all entities provided
        foreach (Entity entData in team.entities)
        {
            // If we dont have enough entity objects, generate a new one on the fly and add to list
            if (currentChildIdx + 1 > childrenCount)
            {
                gobjectsList.Add(InstantiateGameObjReturnComponent<EntityVisual>(_entityPrefab, parentTransform));
                childrenCount++;
            }

            gobjectsList[currentChildIdx].SetData(entData);
            currentChildIdx++;
        }
    }

    private void RenderCards(IList<Card> cardsInHand)
    {
        int currentChildIdx = 0;
        int childrenCount = _visualCardsInHand.Count;

        // Iterate over all entities provided
        foreach (Card entData in cardsInHand)
        {
            // If we dont have enough entity objects, generate a new one on the fly and add to list
            if (currentChildIdx + 1 > childrenCount)
            {
                _visualCardsInHand.Add(InstantiateGameObjReturnComponent<CardVisual>(_cardPrefab, _playerHandContainer));
                childrenCount++;
            }

            _visualCardsInHand[currentChildIdx].SetData(entData);
            currentChildIdx++;
        }
    }

    private T InstantiateGameObjReturnComponent<T>(GameObject prefab, Transform parent)
    {
        GameObject obj = _container.InstantiatePrefab(prefab, parent);
        if (!obj.TryGetComponent(out T component))
        {
            Debug.LogError($"{obj.gameObject.name} prefab lacks {typeof(T)} component!", gameObject);
            return default;
        }
        return component;
    }

    public void Hide()
    {
        _UI.gameObject.SetActive(false);
    }

}

public interface IBattleModel : IGameStateRendererUser
{
    //IEnumerable<Card> GetPlayerDeckDEBUG(); // todo wire properly to inhand container of battle
    void HandleClick(PointerEventData eventData, IBattleClickInfo clickData);
}