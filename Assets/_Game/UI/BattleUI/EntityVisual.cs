using Zenject;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EntityVisual : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image _creatureImage;
    [SerializeField] Transform _intentContainer; // ai chosen actions before they take effect
    [SerializeField] Transform _buffContainer;
    [SerializeField] Slider _hpBar;

    //private Battle _battle;
    private Entity _lastCreatureData;
    private IBattleModel _battleModel;

    [Inject]
    private void Init(IBattleModel model)
    {
        _battleModel = model; 
    }

    public void SetData(Entity ent)//, Battle battle)
    {
        ent._myVisual = this;
        _lastCreatureData = ent;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        _creatureImage.sprite = _lastCreatureData.EntityData.Flavor.CharacterSprite;

        foreach (var intent in _lastCreatureData.QueuedActions)
        {
            //_intentContainer.Instantiate(intentIcon)
            // accumulate effect to one or to many effects or whatver that logic goes here
        }

        foreach (var buff in _lastCreatureData.currentEffects)
        {
            //_buffContainer.Instantiate(buffIcon)
            // draw buffs
        }
        _hpBar.value = (float)_lastCreatureData.Health.current/ _lastCreatureData.Health.max;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _battleModel.HandleClick(eventData, new BattleClickInfo_entity(eventData, _lastCreatureData));
        Debug.Log($"Entity {_lastCreatureData.EntityData.name} was clicked!");
    }
}

