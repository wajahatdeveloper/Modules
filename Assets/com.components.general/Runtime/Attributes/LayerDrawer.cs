using UnityEditor;
using UnityEngine;
using System;

namespace Quartzified.EditorAttributes
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (!isValid(property))
                return;

            string[] layers = UnityEditorInternal.InternalEditorUtility.layers;

            if(property.propertyType == SerializedPropertyType.String)
            {
                int index = Array.IndexOf(layers, property.stringValue);
                index = Mathf.Clamp(index, 0, layers.Length - 1);

                string pickLayer = layers[EditorGUI.Popup(position, label.text, index, layers)];
                if(!pickLayer.Equals(property.stringValue))
                {
                    property.stringValue = pickLayer; 
                }

            }
            else
            {
                int index = property.intValue;
                index = Mathf.Clamp(index, 0, layers.Length - 1);

                int pickLayer = EditorGUI.Popup(position, label.text, index, layers);
                if(pickLayer != index)
                {
                    property.intValue = pickLayer;
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