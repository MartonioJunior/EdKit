using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MartonioJunior.EdKit.Editor
{
    public partial class DictionaryDrawer
    {
        // MARK: Variables
        private ReorderableList list;
        private SerializedProperty keys, values;
        private string displayName;
        
        // MARK: Methods
        private void AddPair(ReorderableList list)
        {
            values.InsertArrayElementAtIndex(values.arraySize);
            keys.InsertArrayElementAtIndex(keys.arraySize);
        }

        private void DrawList(SerializedProperty property, Rect position)
        {
            if (list is null) {
                list = new(property.serializedObject, keys, true, true, true, true) {
                    drawHeaderCallback = DrawHeader,
                    drawElementCallback = DrawPair,
                    onAddCallback = AddPair,
                    onRemoveCallback = RemovePair
                };
            }

            float height = 15;
            for (var i = 0; i < keys.arraySize; i++) {
                height = Mathf.Max(height, EditorGUI.GetPropertyHeight(keys.GetArrayElementAtIndex(i)));
            }

            list.elementHeight = height;
            list.DoList(position);
        }

        private void DrawPair(Rect rect, int index, bool isActive, bool isFocused)
        {
            var keyRect = rect.Sample(0, 0, 0.3f, 1);
            var valueRect = rect.Sample(0.3f, 0, 0.7f, 1);

            EditorGUI.PropertyField(keyRect, keys.GetArrayElementAtIndex(index), GUIContent.none, false);
            EditorGUI.PropertyField(valueRect, values.GetArrayElementAtIndex(index), GUIContent.none, false);
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, displayName);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return list?.GetHeight() ?? 32;
        }

        private void RemovePair(ReorderableList list)
        {
            values.DeleteArrayElementAtIndex(list.index);
            keys.DeleteArrayElementAtIndex(list.index);
        }
    }

    #region UnityEditor.PropertyDrawer Implementation
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>), true)]
    public partial class DictionaryDrawer: UnityEditor.PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            keys = property.FindPropertyRelative("keys");
            values = property.FindPropertyRelative("values");
            displayName = property.displayName;

            EditorGUI.BeginProperty(position, label, property);
            if (keys is not null && values is not null)
                DrawList(property, position);
            else EditorGUI.LabelField(position, "Dictionary was not initialized in code");
            EditorGUI.EndProperty();
        }
    }
    #endregion
}