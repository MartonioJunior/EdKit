using System;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct Axis
    {
        // MARK: Variables
        public X x;
        public Y y;
        public Z z;

        // MARK: Initializers
        public Axis(X x, Y y, Z z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    #region Axis.X
    public partial struct Axis
    {
        public enum X { None = 0, Left = 1, Center = 2, Right = 4 }
    }
    #endregion

    #region Axis.Y
    public partial struct Axis
    {
        public enum Y { None = 0, Below = 1, Neutral = 2, Above = 4 }
    }
    #endregion

    #region Axis.Z
    public partial struct Axis
    {
        public enum Z { None = 0, Back = 1, Body = 2, Front = 4 }
    }
    #endregion
}