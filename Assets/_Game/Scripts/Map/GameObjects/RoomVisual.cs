using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomVisual : MonoBehaviour, IMapObject, IPointerDownHandler
{
    private Image _mapIcon;

    private void Awake()
    {
        _mapIcon = GetComponent<Image>();
    }

    private void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click!!");
    }

    public void Setup(MapNodeData mapObjectData)
    {
        SetIconDependingOnState(mapObjectData);
    }

    private void SetIconDependingOnState(MapNodeData mapObjectData)
    {
        Sprite sprite;

        switch (mapObjectData.State)
        {
            default:
            case MapNodeState.Open:
                {
                    sprite = mapObjectData.Flavor.MapIcon_Open;
                    break;
                }
            case MapNodeState.Locked:
                {
                    sprite = mapObjectData.Flavor.MapIcon_Locked;
                    break;
                }
            case MapNodeState.Explored:
                {
                    sprite = mapObjectData.Flavor.MapIcon_Explored;
                    break;
                }
        }

        _mapIcon.sprite = sprite;
    }
}
