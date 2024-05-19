using System;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using AxisX = Axis.X;
    using AxisY = Axis.Y;
    using AxisZ = Axis.Z;
    #endregion

    [Serializable]
    public partial struct Position
    {
        // MARK: Variables
        [SerializeField] Bounds bounds;

        // MARK: Properties
        public Vector3 Value => bounds.center;
        public Vector3 Deadzone => bounds.extents;
        public static Position Infinite => new Position(Vector3.zero, Vector3.one*float.PositiveInfinity);

        // MARK: Initializers
        public Position(Vector3 value) => bounds = new Bounds(value, new Vector3(0.25f, 0.45f, 0.25f));
        public Position(Vector3 center, Vector3 deadzone) => bounds = new Bounds(center, deadzone);

        // MARK: Methods
        public bool Contains(Vector3 value) => bounds.Contains(value);

        public float PrecisionFor(Vector3 point)
        {
            var delta = point - bounds.center;

            var precisionVector = new Vector3(
                Mathf.Abs(bounds.extents.x / delta.x),
                Mathf.Abs(bounds.extents.y / delta.y),
                Mathf.Abs(bounds.extents.z / delta.z)
            );

            return Mathf.Max(precisionVector.x, precisionVector.y, precisionVector.z);
        }
    }

    #region AxisX
    public partial struct Position
    {
        public AxisX XFor(Vector3 value) {
            switch (value.x) {
                case float x when float.IsNaN(x): return AxisX.None;
                case float x when x < -bounds.min.x: return AxisX.Left;
                case float x when x > bounds.max.x: return AxisX.Right;
                default: return AxisX.Center;
            }
        }
    }
    #endregion

    #region AxisY
    public partial struct Position
    {
        public AxisY YFor(Vector3 value) {
            switch (value.y) {
                case float y when float.IsNaN(y): return AxisY.None;
                case float y when y < -bounds.min.y: return AxisY.Below;
                case float y when y > bounds.max.y: return AxisY.Above;
                default: return AxisY.Neutral;
            }
        }
    }
    #endregion

    #region AxisZ
    public partial struct Position
    {
        public AxisZ ZFor(Vector3 value) {
            switch (value.z) {
                case float z when float.IsNaN(z): return AxisZ.None;
                case float z when z < -bounds.min.z: return AxisZ.Back;
                case float z when z > bounds.max.z: return AxisZ.Front;
                default: return AxisZ.Body;
            }
        }
    }
    #endregion

    #region ToString
    public partial struct Position
    {
        public override string ToString()
        {
            return $"X: {bounds.center.x}, Y: {bounds.center.y}, Z: {bounds.center.z} ({bounds.extents})";
        }
    }
    #endregion

    #region Gizmos Support
    public partial struct Position
    {
        public void DrawGizmos(Transform origin)
        {
            Gizmos.DrawWireCube(origin.position+bounds.center, bounds.size);
        }
    }
    #endregion
}