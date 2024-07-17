using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapNodeVisual : MonoBehaviour, IMapObject, IPointerDownHandler
{
    private Image _mapIcon;
    private MapNodeData _latestData;

    public event Action OnUpdated;

    private void Awake()
    {
        _mapIcon = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _latestData.SetState(MapNodeState.Explored);
        OnUpdated?.Invoke();
    }

    public void Setup(MapNodeData mapObjectData)
    {
        _latestData = mapObjectData;
        SetIconDependingOnState();
    }

    private void SetIconDependingOnState()
    {
        Sprite sprite;

        switch (_latestData.State)
        {
            default:
            case MapNodeState.Open:
                {
                    sprite = _latestData.Flavor.MapIcon_Open;
                    break;
                }
            case MapNodeState.Locked:
                {
                    sprite = _latestData.Flavor.MapIcon_Locked;
                    break;
                }
            case MapNodeState.Explored:
                {
                    sprite = _latestData.Flavor.MapIcon_Explored;
                    break;
                }
        }

        _mapIcon.sprite = sprite;
    }
}

