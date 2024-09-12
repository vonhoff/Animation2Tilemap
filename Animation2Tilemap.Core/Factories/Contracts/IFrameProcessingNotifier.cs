namespace Animation2Tilemap.Core.Factories.Contracts;

public interface IFrameProcessingNotifier
{
    event Action<string> FrameProcessed;
}