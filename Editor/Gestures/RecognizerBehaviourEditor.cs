using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MartonioJunior.EdKit.Editor
{
    public partial class RecognizerBehaviourEditor
    {
        // MARK: Variables
        List<IPose> poses = new();
        List<IGesture> gestures = new();

        // MARK: Methods
        public VisualElement CreateButtonSystem<T>(List<T> elements, Action<T> action)
        {
            var buttonSystem = new VisualElement();
            elements.ForEach(element => {
                var button = new Button(() => action(element)) {
                    text = element.ToString()
                };
                buttonSystem.Add(button);
            });
            return buttonSystem;
        }

        public VisualElement CreatePoseDebug()
        {
            var target = serializedObject.targetObject as RecognizerBehaviour;
            poses = new List<IPose>(target.PoseEvaluator.AllPossibleElements);

            var buttonSystem = CreateButtonSystem(poses, (pose) => {
                target.FeedPoseEvent(new(pose, new(), Time.time, 1.0f));
                Debug.Log("Invoked");
            });

            var root = new VisualElement();
            root.Add(new Label("Pose Debug"));
            root.Add(buttonSystem);
            return root;
        }

        public VisualElement CreateGestureDebug()
        {
            var target = serializedObject.targetObject as RecognizerBehaviour;
            gestures = new List<IGesture>(target.GestureEvaluator.AllPossibleElements);

            var buttonSystem = CreateButtonSystem(gestures, (gesture) => {
                target.FeedGestureEvent(new(gesture, new(), Time.time, 1.0f));
                Debug.Log("Invoked");
            });

            var root = new VisualElement();
            root.Add(new Label("Gesture Debug"));
            root.Add(buttonSystem);
            return root;
        }
    }

    #region UnityEditor.Editor Implementation
    [CustomEditor(typeof(RecognizerBehaviour), true)]
    public partial class RecognizerBehaviourEditor: UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            root.Add(CreatePoseDebug());
            root.Add(CreateGestureDebug());
            return root;
        }
    }
    #endregion
}