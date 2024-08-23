using System;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure used to store the results of a comparison about where a position is located in relation to a bounding box.</summary>
    */
    [Serializable]
    public partial struct Axis
    {
        // MARK: Variables
        /**
        <summary>Is the position to the left, to the right or inside the bounding box (center)?</summary>
        */
        public X x;
        /**
        <summary>Is the position above, below or inside the bounding box (neutral)?</summary>
        */
        public Y y;
        /**
        <summary>Is the position in the front, back or inside the bounding box (body)?</summary>
        */
        public Z z;

        // MARK: Initializers
        /**
        <summary>Initializes the <c>Axis</c> using the specified emumerators</summary>
        */
        public Axis(X x, Y y, Z z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /**
        <summary>Initializes the <c>Axis</c> using a position and a deadzone value.</summary>
        <param name="delta">The position to be compared, result of the delta between the position to be checked and the center of the bounding box.</param>
        <param name="deadzone">The size dimensions of the bounding box.</param>
        <remarks>When no value is supplied to the deadzone, the position is compared by it's signal and is only capable of being outside of the box.</remarks>
        */
        public Axis(Vector3 delta, Vector3? deadzone = null)
        {
            x = GetX(delta, deadzone?.x ?? 0);
            y = GetY(delta, deadzone?.y ?? 0);
            z = GetZ(delta, deadzone?.z ?? 0);
        }

        // MARK: Methods
        /**
        <summary>Returns the result comparison of a position and a bounding box in the X axis.</summary>
        <param name="delta">The position to be compared, result of the delta between the position to be checked and the center of the bounding box.</param>
        <param name="deadzone">The size of the bounding box in the X axis</param>
        <returns>The result of the comparison in the X axis.</returns>
        */
        public static X GetX(Vector3 delta, float deadzone = 0)
        {
            return delta.x switch
            {
                float x when float.IsNaN(x) => X.NoneX,
                float x when x < deadzone => X.Left,
                float x when x > deadzone => X.Right,
                _ => X.Center,
            };
        }
        /**
        <summary>Returns the result comparison of a position and a bounding box in the Y axis.</summary>
        <param name="delta">The position to be compared, result of the delta between the position to be checked and the center of the bounding box.</param>
        <param name="deadzone">The size of the bounding box in the Y axis</param>
        <returns>The result of the comparison in the Y axis.</returns>
        */
        public static Y GetY(Vector3 vector, float deadzone = 0)
        {
            return vector.y switch
            {
                float y when float.IsNaN(y) => Y.NoneY,
                float y when y < deadzone => Y.Below,
                float y when y > deadzone => Y.Above,
                _ => Y.Neutral,
            };
        }
        /**
        <summary>Returns the result comparison of a position and a bounding box in the Z axis.</summary>
        <param name="delta">The position to be compared, result of the delta between the position to be checked and the center of the bounding box.</param>
        <param name="deadzone">The size of the bounding box in the Z axis</param>
        <returns>The result of the comparison in the Z axis.</returns>
        */
        public static Z GetZ(Vector3 vector, float deadzone = 0)
        {
            return vector.z switch
            {
                float z when float.IsNaN(z) => Z.NoneZ,
                float z when z < deadzone => Z.Back,
                float z when z > deadzone => Z.Front,
                _ => Z.Body,
            };
        }
    }

    #region Axis.X
    public partial struct Axis
    {
        /**
        <summary>Spatial relationship between two objects in the X axis.</summary>
        */
        [Flags]
        public enum X
        {
            /**
            <summary>No spatial relationships in the axis.</summary>
            */
            NoneX = 0,
            /**
            <summary>Element is to the left of spatial reference.</summary>
            */
            Left = 1,
            /**
            <summary>Element is aligned or inside the spatial reference.</summary>
            */
            Center = 2,
            /**
            <summary>Element is to the right of spatial reference.</summary>
            */
            Right = 4
        }
    }
    #endregion

    #region Axis.Y
    public partial struct Axis
    {
        /**
        <summary>Spatial relationship between two objects in the Y axis.</summary>
        */
        [Flags]
        public enum Y
        {
            /**
            <summary>No spatial relationships in the axis.</summary>
            */
            NoneY = 0,
            /**
            <summary>Element is below the spatial reference.</summary>
            */
            Below = 1,
            /**
            <summary>Element is aligned or inside the spatial reference.</summary>
            */
            Neutral = 2,
            /**
            <summary>Element is above the spatial reference.</summary>
            */
            Above = 4
        }
    }
    #endregion

    #region Axis.Z
    public partial struct Axis
    {
        /**
        <summary>Spatial relationship between two objects in the Z axis.</summary>
        */
        [Flags]
        public enum Z
        {
            /**
            <summary>No spatial relationships in the axis.</summary>
            */
            NoneZ = 0,
            /**
            <summary>Element is behind of spatial reference.</summary>
            */
            Back = 1,
            /**
            <summary>Element is aligned or inside the spatial reference.</summary>
            */
            Body = 2,
            /**
            <summary>Element is in front of spatial reference.</summary>
            */
            Front = 4
        }
    }
    #endregion

    #region Sign
    public static partial class AxisExtensions
    {
        /**
        <summary>Transforms a comparison result into a numeric signed value.</summary>
        <param name="multiplier">The multiplier for the value to be returned.</param>
        <returns>The signed value of the comparison.</returns>
        */
        public static float Sign(this Axis.X self, float multiplier = 1)
        {
            return self switch
            {
                Axis.X.Left => -multiplier,
                Axis.X.Center => 0,
                Axis.X.Right => multiplier,
                _ => float.NaN,
            };
        }
        /**
        <inheritdoc cref="Sign(Axis.X, float)"/>
        */
        public static float Sign(this Axis.Y self, float multiplier = 1)
        {
            return self switch
            {
                Axis.Y.Below => -multiplier,
                Axis.Y.Neutral => 0,
                Axis.Y.Above => multiplier,
                _ => float.NaN,
            };
        }
        /**
        <inheritdoc cref="Sign(Axis.X, float)"/>
        */
        public static float Sign(this Axis.Z self, float multiplier = 1)
        {
            return self switch
            {
                Axis.Z.Back => -multiplier,
                Axis.Z.Body => 0,
                Axis.Z.Front => multiplier,
                _ => float.NaN,
            };
        }
    }
    #endregion
}