using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Affordance that is represented a description text, audio clip and material.</summary>
    */
    public partial class AffordanceData
    {
        // MARK: Variables
        /**
        <summary>Textual description when an affordance is executed.</summary>
        */
        [SerializeField] string description;
        /**
        <summary>Audio clip to be played when an affordance is executed.</summary>
        */
        [SerializeField] AudioClip clip;
        /**
        <summary>Material to be applied when an affordance is executed.</summary>
        */
        [SerializeField] Material material;
        /**
        <summary>Custom action to be executed when an affordance is executed.</summary>
        */
        [SerializeField] UnityEvent<AffordanceEffect> customAction;

        // MARK: Properties
        /**
        <inheritdoc cref="description"/>
        */
        public string Description {
            get => description;
            set => description = value;
        }
        /**
        <inheritdoc cref="clip"/>
        */
        public AudioClip Clip {
            get => clip;
            set => clip = value;
        }
        /**
        <inheritdoc cref="material"/>
        */
        public Material Material {
            get => material;
            set => material = value;
        }
    }

    #region ScriptableObject Implementation
    [CreateAssetMenu(fileName = "New Affordance", menuName = "EdKit/Affordance")]
    public partial class AffordanceData: ScriptableObject {}
    #endregion

    #region IAffordance Implementation
    public partial class AffordanceData: IAffordance<string, AudioClip, Material>
    {
        /**
        <inheritdoc cref="IAffordance{string, AudioClip, Material}.TextFeedback"/>
        */
        public string TextFeedback => description;
        /**
        <inheritdoc cref="IAffordance{string, AudioClip, Material}.AudioFeedback"/>
        */
        public AudioClip AudioFeedback => clip;
        /**
        <inheritdoc cref="IAffordance{string, AudioClip, Material}.VisualFeedback"/>
        */
        public Material VisualFeedback => material;
        /**
        <inheritdoc cref="IAffordance{string, AudioClip, Material}.UpdateAffordance"/>
        */
        public void UpdateAffordance(AffordanceEffect effect) => customAction?.Invoke(effect);
    }
    #endregion
}