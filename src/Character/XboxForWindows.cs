namespace Appalachia.KOC.Character
{
    [BOTDPlayerInputMapping(BOTDPlayerInputMapping.XboxForWindows)]
    internal class XboxForWindows : IBOTDPlayerInputMapping
    {
        public string moveX => "XboxWinLStickX";

        public string moveY => "XboxWinLStickY";

        public string lookX => "XboxWinRStickX";

        public string lookY => "XboxWinRStickY";

        public string run => "XboxWinLTrigger";

        public string jump => "XboxWinButtonA";
    }
}