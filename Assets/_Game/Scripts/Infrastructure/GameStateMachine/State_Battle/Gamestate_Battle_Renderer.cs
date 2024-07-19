using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Gamestate_Battle_Renderer : MonoBehaviour, IGameStateRenderer
{
    [SerializeField] private Transform _UI;

    [Header("Cards")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _playerHandContainer;

    [Header("Entities")]
    [SerializeField] private GameObject _entityPrefab;
    [SerializeField] private Transform _entitiesLeftContainer;
    [SerializeField] private Transform _entitiesRightContainer;


    private List<MapNodeVisual> _visualChildren = new();
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
    }

    public void Hide()
    {
        _UI.gameObject.SetActive(false);
    }

}

public interface IBattleModel : IGameStateRendererUser
{
}