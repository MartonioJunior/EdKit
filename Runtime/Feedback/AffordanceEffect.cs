using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct AffordanceEffect
    {
        // MARK: Variables
        [SerializeField] float alignment;
        [SerializeField] float duration;
        [SerializeField] float scale;

        // MARK: Properties
        public float Alignment => alignment;
        public float Duration => duration;
        public float Scale => scale;

        // MARK: Initializers
        public AffordanceEffect(float alignment, float scale, float duration, float multiplier = 1)
        {
            this.alignment = Mathf.Clamp(alignment * multiplier, -1, 1);
            this.scale = Mathf.Clamp(scale * multiplier, 0, 1);
            this.duration = duration;
        }
    }
}