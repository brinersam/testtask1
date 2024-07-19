using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapNodeRenderer : MonoBehaviour, IGameStateRenderer
{
    [SerializeField] private GameObject _roomVisualPrefab;
    [SerializeField] private Transform _UI;
    [SerializeField] private Transform _roomVisualContainer;

    private List<MapNodeVisual> _visualChildren = new();
    private INodeProvider _nodeProvider;
    private IMapModel _model;


    [Inject] DiContainer _container;
    [Inject]
    void Init(INodeProvider nodeProvider, IMapModel model)
    {
        _nodeProvider = nodeProvider;
        _model = model;
        model.RegisterRenderer(this);
    }
        
    //void Start()
    //{
    //    Render();
    //}

    public void Render()
    {
        _UI.gameObject.SetActive(true);

        int currentChildIdx = 0;
        int childrenCount = _visualChildren.Count;
        
        // Iterate over all rooms provided
        foreach (MapNodeData roomData in _nodeProvider)
        {
            // If we dont have enough node objects, generate a new one on the fly and add to list
            if (currentChildIdx+1 > childrenCount)
            {
                GenerateNodeGObject(ref childrenCount);
            }

            _visualChildren[currentChildIdx].SetData(roomData);
            currentChildIdx++;
        }
    }

    public void Hide()
    {
        _UI.gameObject.SetActive(false);
    }

    private void GenerateNodeGObject(ref int childCount)
    {
        GameObject obj = _container.InstantiatePrefab(_roomVisualPrefab, _roomVisualContainer);
        if (!obj.TryGetComponent(out MapNodeVisual roomVisual))
        {
            Debug.LogError($"{obj.gameObject.name} prefab lacks {typeof(MapNodeVisual)} component!", gameObject);
            return;
        }
        _visualChildren.Add(roomVisual);

        childCount++;
    }


}

public interface IGameStateRenderer
{
    void Hide();
    void Render();
}

public interface INodeProvider : IEnumerable<MapNodeData>
{
}