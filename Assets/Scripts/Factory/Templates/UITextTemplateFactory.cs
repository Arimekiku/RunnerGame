using VContainer;

public class UITextTemplateFactory : GameInstanceFactory
{
    private readonly DisposableWorldText _canvasPrefab;

    public UITextTemplateFactory(IObjectResolver objectResolver, PlayerBehaviour playerBehaviour) : base(objectResolver)
    {
        playerBehaviour.OnNewBlock += CreateInstance;
    }

    private void CreateInstance()
    {
        CreateInstanceByObject(_canvasPrefab);
    }
}