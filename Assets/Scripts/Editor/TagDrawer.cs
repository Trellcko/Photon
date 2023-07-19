using Trellcko;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Unavinar.DrawTD.Editor
{
    [CustomPropertyDrawer(typeof(TagProperty))]
    public class TagDrawer : PropertyDrawer
    {
        private int _index = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                for (int i = 0; i < InternalEditorUtility.tags.Length; i++)
                {
                    if (property.stringValue == InternalEditorUtility.tags[i])
                    {
                        _index = i;
                    }
                }
                _index = EditorGUI.Popup(position, label.text, _index, InternalEditorUtility.tags);

                property.stringValue = InternalEditorUtility.tags[_index];
                return;
            }
            EditorGUI.LabelField(position, "Wrong field type!");
        }
    }
}