namespace Appalachia.KOC.Character
{
    [BOTDPlayerInputMapping(BOTDPlayerInputMapping.XboxForMac)]
    internal class XboxForMac : IBOTDPlayerInputMapping
    {
        public string moveX => "XboxMacLStickX";

        public string moveY => "XboxMacLStickY";

        public string lookX => "XboxMacRStickX";

        public string lookY => "XboxMacRStickY";

        public string run => "XboxMacLTrigger";

        public string jump => "XboxMacButtonA";
    }
}
