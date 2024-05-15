namespace MartonioJunior.EdKit
{
    public static partial class Axis {}

    #region Axis.X
    public static partial class Axis
    {
        public enum X { None = 0, Left = 1, Center = 2, Right = 4 }
    }
    #endregion

    #region Axis.Y
    public static partial class Axis
    {
        public enum Y { None = 0, Below = 1, Neutral = 2, Above = 4 }
    }
    #endregion

    #region Axis.Z
    public static partial class Axis
    {
        public enum Z { None = 0, Back = 1, Body = 2, Front = 4 }
    }
    #endregion
}