internal interface IMapModel
{
    void HandleNodeInteraction(MapNodeData node);
    void RegisterRenderer(IMapRenderer renderer);
}