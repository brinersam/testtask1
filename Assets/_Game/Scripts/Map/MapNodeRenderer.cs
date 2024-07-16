using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapNodeRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _roomVisualPrefab;
    [SerializeField] private Transform _roomVisualContainer;

    private List<RoomVisual> _visualChildren = new();
    private IMapNodeProvider _provider;

    [Inject]
    void Init(IMapNodeProvider provider)
    {
        _provider = provider;
    }
        
    void Start()
    {
        RefreshRoomVisuals();
    }

    public void RefreshRoomVisuals()
    {
        int currentChildIdx = 0;
        int childrenCount = _visualChildren.Count;
        
        // Iterate over all rooms provided
        foreach (MapNodeData roomData in _provider)
        {
            // If we dont have enough node objects, generate a new one on the fly and add to list
            if (currentChildIdx+1 > childrenCount)
            {
                GenerateNodeGObject(ref childrenCount);
            }

            _visualChildren[currentChildIdx].Setup(roomData);
            currentChildIdx++;
        }

        //// If we have more gameObjects than data, turn them off (probably not possible to reach this actually)
        //if (currentChildIdx <= _visualChildren.Count)
        //{
        //    for (int i = currentChildIdx; i < _visualChildren.Count; i++)
        //    {
        //        _visualChildren[i].gameObject.SetActive(false);
        //    }
        //}
    }

    private void GenerateNodeGObject(ref int childCount)
    {
        GameObject obj = (GameObject)Instantiate(_roomVisualPrefab, _roomVisualContainer, instantiateInWorldSpace: false);
        if (!obj.TryGetComponent(out RoomVisual roomVisual))
        {
            Debug.LogError($"{obj.gameObject.name} prefab lacks {typeof(RoomVisual)} component!", gameObject);
            return;
        }
        _visualChildren.Add(roomVisual);
        childCount++;
    }


}

public interface IMapNodeProvider : IEnumerable<MapNodeData>
{

}