namespace Appalachia.Core.Character
{
    [BOTDPlayerInputMapping(BOTDPlayerInputMapping.PlayStationForWindows)]
    internal class PlayStationForWindows : IBOTDPlayerInputMapping
    {
        public string moveX => "PSWinLStickX";

        public string moveY => "PSWinLStickY";

        public string lookX => "PSWinRStickX";

        public string lookY => "PSWinRStickY";

        public string run => "PSWinLTrigger";

        public string jump => "PSWinCross";
    }
}