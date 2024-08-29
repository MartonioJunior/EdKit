using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Data structure containing information about how an affordance should be configured in a scene.</summary>
    */
    [System.Serializable]
    public partial struct AffordanceEffect
    {
        // MARK: Variables
        /**
        <summary>Alignment of the effect, based on the reference axis.</summary>
        <remarks>Values range from -1.0 (before the origin) to +1.0 (after the origin)</remarks>
        */
        [SerializeField] float alignment;
        /**
        <summary>Duration of the effect.</summary>
        <remarks>For convention, it is recommended to work with a value measured in seconds.</remarks>
        */
        [SerializeField] float duration;
        /**
        <summary>Scale of the effect.</summary>
        <remarks>Values range from 0.0 (no effect) to +1.0 (maximum effect)</remarks>
        */
        [SerializeField] float scale;

        // MARK: Properties
        /**
        <summary>Alignment of the effect, based on the reference axis.</summary>
        <remarks>Values range from -1.0 (before the origin) to +1.0 (after the origin)</remarks>
        */
        public float Alignment => alignment;
        /**
        <inheritdoc cref="duration"/>
        */
        public float Duration => duration;
        /**
        <inheritdoc cref="scale"/>
        */
        public float Scale => scale;

        // MARK: Initializers
        /**
        <summary>Initializes a new affordance effect.</summary>
        <param name="alignment">Alignment of the effect.</param>
        <param name="scale">Scale of the effect.</param>
        <param name="duration">Duration of the effect.</param>
        <param name="multiplier">Multiplier to be applied to the alignment and scale values.</param>
        */
        public AffordanceEffect(float alignment, float scale, float duration, float multiplier = 1)
        {
            this.alignment = Mathf.Clamp(alignment * multiplier, -1, 1);
            this.scale = Mathf.Clamp(scale * multiplier, 0, 1);
            this.duration = duration;
        }
    }

    #region Builder-like API
    public partial struct AffordanceEffect
    {
        /**
        <summary>Creates a new affordance effect with a different alignment.</summary>
        <param name="newAlignment">New alignment value.</param>
        */
        public AffordanceEffect WithAlignment(float newAlignment) => new(newAlignment, scale, duration);
        /**
        <summary>Creates a new affordance effect with a different duration.</summary>
        <param name="newDuration">New duration value.</param>
        */
        public AffordanceEffect WithDuration(float newDuration) => new(alignment, scale, newDuration);
        /**
        <summary>Creates a new affordance effect with a different scale.</summary>
        <param name="newScale">New scale value.</param>
        */
        public AffordanceEffect WithScale(float newScale) => new(alignment, newScale, duration);
    }
    #endregion
}