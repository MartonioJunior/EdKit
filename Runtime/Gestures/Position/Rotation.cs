using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using AxisX = Axis.X;
    using AxisY = Axis.Y;
    using AxisZ = Axis.Z;
    #endregion

    [System.Serializable]
    public partial struct Rotation
    {
        // MARK: Variables
        [SerializeField] Quaternion value;
        [SerializeField] Vector3 deadzone;

        // MARK: Properties
        public Quaternion Value => value;
        public Vector3 Deadzone => deadzone;

        // MARK: Initializers
        public Rotation(Quaternion quaternion)
        {
            this.value = quaternion;
            this.deadzone = new Vector3(20, 20, 20);
        }

        public Rotation(Quaternion quaternion, Vector3 deadzone)
        {
            this.value = quaternion;
            this.deadzone = deadzone;
        }
    }

    #region Axis.X
    public partial struct Rotation
    {
        public AxisX XFor(Quaternion rotation)
        {
            var angle = value.eulerAngles.x;
            var rotationAngle = rotation.eulerAngles.x;
            var deltaX = Mathf.Abs(angle - rotationAngle);

            switch (deltaX) {
                case float x when deltaX <= deadzone.x: return AxisX.Center;
                case float x when deltaX > deadzone.x && angle < rotationAngle: return AxisX.Left;
                case float x when deltaX > deadzone.x && angle > rotationAngle: return AxisX.Right;
                default: return AxisX.None;
            }
        }
    }
    #endregion

    #region Axis.Y
    public partial struct Rotation
    {
        public AxisY YFor(Quaternion rotation)        
        {
            var angle = value.eulerAngles.y;
            var rotationAngle = rotation.eulerAngles.y;
            var deltaY = Mathf.Abs(angle - rotationAngle);

            switch (deltaY) {
                case float y when deltaY <= deadzone.y: return AxisY.Neutral;
                case float y when deltaY > deadzone.y && angle < rotationAngle: return AxisY.Below;
                case float y when deltaY > deadzone.y && angle > rotationAngle: return AxisY.Above;
                default: return AxisY.None;
            }
        }
    }
    #endregion

    #region Axis.Z
    public partial struct Rotation
    {
        public AxisZ ZFor(Quaternion rotation)
        {
            var angle = value.eulerAngles.z;
            var rotationAngle = rotation.eulerAngles.z;
            var deltaZ = Mathf.Abs(angle - rotationAngle);

            switch (deltaZ) {
                case float z when deltaZ <= deadzone.z: return AxisZ.Body;
                case float z when deltaZ > deadzone.z && angle < rotationAngle: return AxisZ.Back;
                case float z when deltaZ > deadzone.z && angle > rotationAngle: return AxisZ.Front;
                default: return AxisZ.None;
            }
        }
    }
    #endregion

    #region ToString
    public partial struct Rotation
    {
        public override string ToString()
        {
            return $"Value: {value}, Deadzone: {deadzone}";
        }
    }
    #endregion

    #region Gizmos Support
    public partial struct Rotation
    {
        public void DrawGizmos(Transform origin)
        {
            var position = origin.position;

            Gizmos.DrawWireSphere(position, 0.1f);
            Gizmos.DrawRay(position, value * Vector3.right);
            Gizmos.DrawRay(position, value * Vector3.up);
            Gizmos.DrawRay(position, value * Vector3.forward);
        }
    }
    #endregion
}