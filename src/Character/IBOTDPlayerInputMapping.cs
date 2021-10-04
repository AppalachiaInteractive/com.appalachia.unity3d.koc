namespace Appalachia.KOC.Character
{
    public interface IBOTDPlayerInputMapping
    {
        string moveX { get; }
        string moveY { get; }
        string lookX { get; }
        string lookY { get; }
        string run { get; }
        string jump { get; }
    }
}
