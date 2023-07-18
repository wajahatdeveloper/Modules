using UnityEditor;
using UnityEngine;
using System;

namespace Quartzified.EditorAttributes
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (!isValid(property))
                return;

            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

            if(property.propertyType == SerializedPropertyType.String)
            {
                int index = Array.IndexOf(tags, property.stringValue);
                index = Mathf.Clamp(index, 0, tags.Length - 1);

                string pickTag = tags[EditorGUI.Popup(position, label.text, index, tags)];
                if(!pickTag.Equals(property.stringValue))
                {
                    property.stringValue = pickTag; 
                }
            }
            else
            {
                int index = property.intValue;
                index = Mathf.Clamp(index, 0, tags.Length - 1);

                int pickTag = EditorGUI.Popup(position, label.text, index, tags);
                if(pickTag != index)
                {
                    property.intValue = pickTag;
                }
            }

            EditorGUI.EndProperty();
        }

        private bool isValid(SerializedProperty property)
        {
            switch(property.propertyType)
            {
                case SerializedPropertyType.String:
                case SerializedPropertyType.Integer:
                    return true;
                default:
                    return false;
            }
        }

    }


}