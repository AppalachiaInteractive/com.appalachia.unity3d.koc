namespace Appalachia.Core.Character
{
    [BOTDPlayerInputMapping(BOTDPlayerInputMapping.Vive)]
    internal class Vive : IBOTDPlayerInputMapping
    {
        public string moveX => "ViveLThumbX";

        public string moveY => "ViveLThumbY";

        public string lookX => "ViveRThumbX";

        public string lookY => "ViveRThumbY";

        public string run => "ViveLTrigger";

        public string jump => "ViveLThumb";
    }
}