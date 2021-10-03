namespace Appalachia.Core.Character
{
    [BOTDPlayerInputMapping(BOTDPlayerInputMapping.Xbox)]
    internal class Xbox : IBOTDPlayerInputMapping
    {
        public string moveX => "XboxLStickX";

        public string moveY => "XboxLStickY";

        public string lookX => "XboxRStickX";

        public string lookY => "XboxRStickY";

        public string run => "XboxLTrigger";

        public string jump => "XboxButtonA";
    }
}