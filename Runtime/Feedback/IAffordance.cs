namespace MartonioJunior.EdKit
{
    public interface IAffordance<Text,Audio,Visual>
    {
        Text TextFeedback { get; }
        Audio AudioFeedback { get; }
        Visual VisualFeedback { get; }

        void UpdateAffordance(AffordanceEffect effect);
    }
}