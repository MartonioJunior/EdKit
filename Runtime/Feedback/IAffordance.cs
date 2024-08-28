namespace MartonioJunior.EdKit
{
    /**
    <summary>
    Interface that defines an affordance: a source of multimodal feedback data.
    In EdKit, feedback is divided into three categories: <br/>
    - Text: Feedback that is presented visually in an user interface or spoken to the user through text-to-speech. <br/>
    - Audio: Feedback that is presented through sound mechanisms. <br/>
    - Visual: Feedback that is presented visually in the environment, such as highlighting objects or changing their appearance. <br/>
    It's important to note that the feedback data is not directly presented to the user, but rather serves as a source
    of data to be applied at runtime using `UpdateAffordance`.
    </summary>
    */
    public interface IAffordance<Text,Audio,Visual>
    {
        /**
        <summary>Textual representation for an affordance.</summary>
        */
        Text TextFeedback { get; }
        /**
        <summary>Audio representation for an affordance.</summary>
        */
        Audio AudioFeedback { get; }
        /**
        <summary>Visual representation for an affordance.</summary>
        */
        Visual VisualFeedback { get; }
        /**
        <summary>Updates the affordance with a given effect, as well as executing other types of effects (e.g. haptics).</summary>
        <param name="effect">Effect to be applied.</param>
        <remarks>This method can also be ran the affordance in the game's loop.</remarks>
        */
        void UpdateAffordance(AffordanceEffect effect);
    }
}