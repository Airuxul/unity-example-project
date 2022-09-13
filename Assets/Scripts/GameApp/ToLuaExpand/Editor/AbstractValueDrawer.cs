using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameApp.ToLuaExpand.Editor
{
    [CustomPropertyDrawer(typeof(AbstractValue))]
    public class AbstractValueDrawer : PropertyDrawer
    {
        private SerializedProperty typeProperty;

        private SerializedProperty nameProperty;

        private SerializedProperty valueProperty;
        
        private float spacing = 10;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();
            
            FindAllProperty(property);
            
            if (nameProperty == null)
            {
                Debug.LogError("Wrong Property");
                return;
            }

            ValueType enumType = (ValueType) Enum.GetValues(typeof(ValueType)).GetValue(typeProperty.enumValueIndex);

            #region Rect Create
            Rect typeRect =new Rect(position)
            {
                width = 70,
                x = position.x + position.width
            };
            typeRect.x -= typeRect.width;

            Rect nameRect = new Rect(position)
            {
                width = 120
            };
            
            Rect valueRect =new Rect(position)
            {
                width = position.width - nameRect.width - typeRect.width - 2 * spacing
            };
            valueRect.x = typeRect.x - valueRect.width - spacing;
            #endregion
            
            #region GUI Draw
            nameProperty.stringValue = EditorGUI.TextField(nameRect, nameProperty.stringValue);
            typeProperty.enumValueIndex = (int) (ValueType) EditorGUI.EnumPopup(typeRect, enumType);
            string valueTypeStr = GetTypeName(property.managedReferenceFullTypename);
            switch (enumType)
            {
                case ValueType.Int:
                    if (!JudgeType<IntValue>(valueTypeStr,property)) return;
                    valueProperty.intValue = EditorGUI.IntField(valueRect, valueProperty.intValue);
                    break;
                case ValueType.Float:
                    if (!JudgeType<FloatValue>(valueTypeStr,property)) return;
                    valueProperty.floatValue = EditorGUI.FloatField(valueRect, valueProperty.floatValue);
                    break;
                case ValueType.String:
                    if (!JudgeType<StringValue>(valueTypeStr,property)) return;
                    valueProperty.stringValue = EditorGUI.TextField(valueRect, valueProperty.stringValue);
                    break;
                case ValueType.Object:
                    if (!JudgeType<ObjectValue>(valueTypeStr,property)) return;
                    
                    DrawObjectField(valueRect, o =>
                    {
                        property.FindPropertyRelative("value").objectReferenceValue = o;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                    
                    var obj = EditorGUI.ObjectField(valueRect, "",valueProperty.objectReferenceValue,typeof(Object),true);
                    if (obj != null && obj != valueProperty.objectReferenceValue)
                    {
                        valueProperty.objectReferenceValue = obj;
                    }
                    break;
                
            }
            #endregion
            
            property.serializedObject.ApplyModifiedProperties();
        }

        private void FindAllProperty(SerializedProperty property)
        {
            typeProperty = property.FindPropertyRelative("valueType");
            nameProperty = property.FindPropertyRelative("name");
            valueProperty = property.FindPropertyRelative("value");
        }
        
        private string GetTypeName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                Debug.LogError("type name is null");
                return "";
            }
            var index = typeName.LastIndexOf(' ');
            if(index >= 0)
                typeName = typeName.Substring(index + 1);

            index = typeName.LastIndexOf('.');
            if(index >= 0)
                return typeName.Substring(index + 1);

            return typeName;
        }

        private bool JudgeType<T>(string typeName, SerializedProperty property) where T : AbstractValue, new()
        {
            if (typeName != typeof(T).Name)
            {
                ReCreateProperty<T>(property);
                return false;
            }

            return true;
        }
        
        private void ReCreateProperty<T>(SerializedProperty property) where T : AbstractValue, new()
        {
            property.managedReferenceValue = new T();
            property.serializedObject.ApplyModifiedProperties();
        }

        private void DrawObjectField(Rect rect, Action<Object> onChange)
        {
            var e = Event.current;
            if (rect.Contains(e.mousePosition) && DragAndDrop.objectReferences.Length > 0 &&
                (e.type == EventType.DragUpdated || e.type == EventType.DragPerform))
            {
                var obj = DragAndDrop.objectReferences[0];
                if (e.type == EventType.DragUpdated)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Move;
                    DragAndDrop.AcceptDrag();
                }
                else if (e.type == EventType.DragPerform && obj is GameObject gameObject)
                {
                    var components = gameObject.GetComponents<Component>();
                    var objs = new List<Object> {gameObject};
                    objs.AddRange(components);

                    var options = new List<string> {gameObject.GetType().ToString()};
                    options.AddRange(components.Select(cmp => cmp.GetType().ToString()));
                    CreateMenu(options.ToArray(), (idx) =>
                    {
                        onChange(objs[idx]);
                    }).ShowAsContext();
                }
                else if(e.type == EventType.DragPerform && obj)
                {
                    onChange(obj);
                }
                e.Use();
            }
        }
        
        private GenericMenu CreateMenu(IList<string> options, Action<int> callback)
        {
            var menu = new GenericMenu();

            for (var i = 0; i < options.Count; i++)
            {
                var index = i;
                menu.AddItem(new GUIContent(options[index]), false, () => callback(index));    
            }
            return menu;
        }
    }
}
    
