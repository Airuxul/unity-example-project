using System;
using System.Collections.Generic;

namespace GameApp.ToLuaExpand
{
    public enum ValueType
    {
        Int = 0,
        Float = 1,
        String = 2,
        Vector2 = 3,
        Vector3 = 4,
        Vector4 = 5,
        Rect = 6,
        Bounds = 7,
        Color = 8,
        Curve = 9,
        Bool = 10,
        Object = 11,
    }
    [Serializable]
    public class ValueWrap
    {
        public ValueType type = ValueType.Int;
        public string name;
        [System.NonSerialized]
        public object value;
    }
    
    public class BindableValue
    {
        public List<ValueWrap> wraps = new List<ValueWrap>();
        
    }
}