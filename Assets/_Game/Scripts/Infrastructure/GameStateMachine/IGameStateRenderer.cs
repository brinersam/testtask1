public interface IGameStateRenderer
{
    void Hide();
    void Render();
}

public interface IGameStateRendererUser
{
    void RegisterRenderer(IGameStateRenderer renderer);
}
