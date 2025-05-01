namespace Animation2Tilemap.Factories.Contracts;

public interface IFrameProcessingNotifier
{
    event Action<string> FrameProcessed;
}