using System;
using UnityEditor;
using UnityEngine;

namespace Scorewarrior.Test.Utility
{
    [CustomPropertyDrawer(typeof(EnumDictionary<,>), true)]
    public class EnumDictionaryDrawer<TKey, TValue> : PropertyDrawer
    {
        private bool foldout = true;
        private EnumDictionary<TKey, TValue> dictionary;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (foldout)
            {
                return EditorGUIUtility.singleLineHeight +
                       EditorGUIUtility.singleLineHeight * Enum.GetValues(typeof(TKey)).Length;
            }

            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            dictionary = (EnumDictionary<TKey, TValue>)fieldInfo.GetValue(property.serializedObject.targetObject);

            EditorGUI.BeginProperty(position, label, property);

            var foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            foldout = EditorGUI.Foldout(foldoutRect, foldout, label);

            position.y += EditorGUIUtility.singleLineHeight;

            if(!foldout) return;

            foreach (TKey key in Enum.GetValues(typeof(TKey)))
            {
                Rect singleLineRect =
                    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(singleLineRect, key.ToString());
                Rect valueFieldRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y,
                    position.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                TValue value = dictionary[key];
                TValue newValue = DrawValueField(valueFieldRect, value);
                if (!newValue.Equals(value))
                {
                    dictionary[key] = newValue;
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }

                position.y += EditorGUIUtility.singleLineHeight;
            }

            EditorGUI.EndProperty();
        }

        private TValue DrawValueField(Rect position, TValue value)
        {
            Type valueType = typeof(TValue);

            if (valueType == typeof(float))
            {
                return (TValue)(object)EditorGUI.FloatField(position, (float)(object)value);
            }
            else if (valueType == typeof(int))
            {
                return (TValue)(object)EditorGUI.IntField(position, (int)(object)value);
            }
            else if (valueType == typeof(bool))
            {
                return (TValue)(object)EditorGUI.Toggle(position, (bool)(object)value);
            }
            else if (valueType == typeof(string))
            {
                return (TValue)(object)EditorGUI.TextField(position, (string)(object)value);
            }
            else
            {
                EditorGUI.LabelField(position, "Unsupported type: " + valueType);
                return value;
            }
        }
    }
}