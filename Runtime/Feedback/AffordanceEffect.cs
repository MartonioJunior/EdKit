using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct AffordanceEffect
    {
        // MARK: Variables
        [SerializeField] float alignment;
        [SerializeField] float scale;

        // MARK: Initializers
        public AffordanceEffect(float alignment, float scale, float multiplier)
        {
            this.alignment = Mathf.Clamp(alignment * multiplier, -1, 1);
            this.scale = Mathf.Clamp(scale * multiplier, 0, 1);
        }
    }
}