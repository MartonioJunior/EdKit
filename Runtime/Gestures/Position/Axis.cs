using System;
using UnityEngine;

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

        public Axis(Vector3 vector, Vector3? deadzone = null)
        {
            x = GetX(vector, deadzone?.x ?? 0);
            y = GetY(vector, deadzone?.y ?? 0);
            z = GetZ(vector, deadzone?.z ?? 0);
        }

        // MARK: Methods
        public static X GetX(Vector3 vector, float deadzone = 0)
        {
            return vector.x switch
            {
                float x when float.IsNaN(x) => X.None,
                float x when x < deadzone => X.Left,
                float x when x > deadzone => X.Right,
                _ => X.Center,
            };
        }

        public static Y GetY(Vector3 vector, float deadzone = 0)
        {
            return vector.y switch
            {
                float y when float.IsNaN(y) => Y.None,
                float y when y < deadzone => Y.Below,
                float y when y > deadzone => Y.Above,
                _ => Y.Neutral,
            };
        }

        public static Z GetZ(Vector3 vector, float deadzone = 0)
        {
            return vector.z switch
            {
                float z when float.IsNaN(z) => Z.None,
                float z when z < deadzone => Z.Back,
                float z when z > deadzone => Z.Front,
                _ => Z.Body,
            };
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