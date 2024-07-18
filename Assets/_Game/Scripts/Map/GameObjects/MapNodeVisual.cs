using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class MapNodeVisual : MonoBehaviour, IMapNode, IPointerDownHandler
{
    private IMapModel _mapModel; 
    private Image _mapIcon;
    private MapNodeData _currentData;

    [Inject]
    private void Inject(IMapModel model)
    {
        _mapModel = model;
    }

    private void Awake()
    {
        _mapIcon = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _mapModel.HandleNodeInteraction(_currentData);
    }
    
    public void SetData(MapNodeData mapObjectData)
    {
        _currentData = mapObjectData;
        SetIconDependingOnState();
    }

    private void SetIconDependingOnState()
    {
        Sprite sprite;

        switch (_currentData.State)
        {
            default:
            case MapNodeState.Open:
                {
                    sprite = _currentData.Flavor.MapIcon_Open;
                    break;
                }
            case MapNodeState.Locked:
                {
                    sprite = _currentData.Flavor.MapIcon_Locked;
                    break;
                }
            case MapNodeState.Explored:
                {
                    sprite = _currentData.Flavor.MapIcon_Explored;
                    break;
                }
        }

        _mapIcon.sprite = sprite;
    }
}
