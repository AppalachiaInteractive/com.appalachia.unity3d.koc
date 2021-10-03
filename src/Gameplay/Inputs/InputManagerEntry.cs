namespace Appalachia.Core.Gameplay.Inputs
{
    public class InputManagerEntry
    {
        public enum Kind { KeyOrButton, Mouse, Axis }
        public enum Axis { X, Y, Third, Fourth, Fifth, Sixth, Seventh, Eigth }
        public enum Joy { All, First, Second }

        public string   name = "";
        public string   desc = "";
        public string   btnNegative = "";
        public string   btnPositive = "";
        public string   altBtnNegative = "";
        public string   altBtnPositive = "";
        public float    gravity = 0.0f;
        public float    deadZone = 0.0f;
        public float    sensitivity = 0.0f;
        public bool     snap = false;
        public bool     invert = false;
        public Kind     kind = Kind.Axis;
        public Axis     axis = Axis.X;
        public Joy      joystick = Joy.All;
    }
}