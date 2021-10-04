namespace Appalachia.KOC.Gameplay.Inputs
{
    public class InputManagerEntry
    {
        public enum Axis
        {
            X,
            Y,
            Third,
            Fourth,
            Fifth,
            Sixth,
            Seventh,
            Eigth
        }

        public enum Joy
        {
            All,
            First,
            Second
        }

        public enum Kind
        {
            KeyOrButton,
            Mouse,
            Axis
        }

        public string altBtnNegative = "";
        public string altBtnPositive = "";
        public Axis axis = Axis.X;
        public string btnNegative = "";
        public string btnPositive = "";
        public float deadZone = 0.0f;
        public string desc = "";
        public float gravity = 0.0f;
        public bool invert = false;
        public Joy joystick = Joy.All;
        public Kind kind = Kind.Axis;

        public string name = "";
        public float sensitivity = 0.0f;
        public bool snap = false;
    }
}
