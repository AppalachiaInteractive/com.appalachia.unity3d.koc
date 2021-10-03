namespace Appalachia.Core.Character
{
    [BOTDPlayerInputMapping(BOTDPlayerInputMapping.PlayStationForMac)]
    internal class PlayStationForMac : IBOTDPlayerInputMapping
    {
        public string moveX => "PSMacLStickX";

        public string moveY => "PSMacLStickY";

        public string lookX => "PSMacRStickX";

        public string lookY => "PSMacRStickY";

        public string run => "PSMacLTrigger";

        public string jump => "PSMacCross";
    }
}