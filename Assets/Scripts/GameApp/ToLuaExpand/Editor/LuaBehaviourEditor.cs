using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GameApp.ToLuaExpand.Editor
{
    [CustomEditor(typeof(LuaBehaviour))]
    public class LuaBehaviourEditor:UnityEditor.Editor
    {
        private ReorderableList reorderableList;
        private SerializedProperty _luaPath;
        private SerializedProperty _valueWraps;
        private void OnEnable()
        {
            _luaPath = serializedObject.FindProperty("_luaPath");
            
            _valueWraps = serializedObject.FindProperty("_val");

            reorderableList = new ReorderableList(serializedObject, _valueWraps, true, true, true, true)
            {
                //设置单个元素的高度
                elementHeight = 20,
                //绘制单个元素
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = _valueWraps.GetArrayElementAtIndex(index);
                    rect.height -= 4;
                    rect.y += 2;
                    EditorGUI.PropertyField(rect, element);
                },
                //
                drawHeaderCallback = (rect) =>
                    EditorGUI.LabelField(rect, "Lua Values"),
                onAddCallback = (reorderableList) =>
                {
                    _valueWraps.InsertArrayElementAtIndex(_valueWraps.arraySize);
                    _valueWraps.serializedObject.ApplyModifiedProperties();
                    
                    var newElement = _valueWraps.GetArrayElementAtIndex(_valueWraps.arraySize-1);
                    newElement.managedReferenceValue = new IntValue();
                    newElement.serializedObject.ApplyModifiedProperties();
                },
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(10);

            # region LuaPath
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            _luaPath = serializedObject.FindProperty("_luaPath");
            EditorGUILayout.PropertyField(_luaPath);
            if(GUILayout.Button( "@", GUILayout.MaxWidth(30)))
            {
                var refObj = AssetDatabase.LoadAssetAtPath<Object>("Assets/Lua/"+_luaPath.stringValue);
                if (refObj != null)
                {
                    EditorGUIUtility.PingObject(refObj);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.EndHorizontal();
            #endregion
            
            EditorGUILayout.Space(20);

            _valueWraps = serializedObject.FindProperty("_val");
            serializedObject.Update();
            reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}