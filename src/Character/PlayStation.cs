namespace Appalachia.KOC.Character
{
    [BOTDPlayerInputMapping(BOTDPlayerInputMapping.PlayStation)]
    internal class PlayStation : IBOTDPlayerInputMapping
    {
        public string moveX => "PSLStickX";

        public string moveY => "PSLStickY";

        public string lookX => "PSRStickX";

        public string lookY => "PSRStickY";

        public string run => "PSLTrigger";

        public string jump => "PSCross";
    }
}
