using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class AffordanceData
    {
        // MARK: Variables
        [SerializeField] string displayName;
        [SerializeField] AudioClip clip;
        [SerializeField] Color color;
        [SerializeField] Material material;
        [SerializeField] UnityEvent action;
    }

    #region ScriptableObject Implementation
    [CreateAssetMenu(fileName = "New Affordance", menuName = "EdKit/Affordance")]
    public partial class AffordanceData: ScriptableObject
    {
        
    }
    #endregion

    #region IAffordance Implementation
    public partial class AffordanceData : IAffordance
    {
        public AudioClip Audio => clip;
        public Color Color => color;
        public Material Material => material;
        public string Text => displayName;

        public void Run() => action?.Invoke();
    }
    #endregion
}