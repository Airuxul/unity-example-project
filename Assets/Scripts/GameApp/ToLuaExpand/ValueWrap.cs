using System;
using UnityEngine;
using Object = UnityEngine.Object;

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
        Object = 11
    }

    
    [Serializable]
    public abstract class AbstractValue
    {
        public string name;
        public ValueType valueType;
    }
    
    [Serializable]
    public class IntValue : AbstractValue
    {
        public IntValue()
        {
            valueType = ValueType.Int;
        }
        
        public int value;
    }

    [Serializable]
    public class FloatValue : AbstractValue
    {
        public FloatValue()
        {
            valueType = ValueType.Float;
        }

        public float value;
    }

    [Serializable]
    public class StringValue : AbstractValue
    {
        public StringValue()
        {
            valueType = ValueType.String;
        }

        public string value;
    }

    [Serializable]
    public class Vector2Value : AbstractValue
    {
        public Vector2Value()
        {
            valueType = ValueType.Vector2;
        }

        public Vector2 value;
    }

    [Serializable]
    public class Vector3Value : AbstractValue
    {
        public Vector3Value()
        {
            valueType = ValueType.Vector3;
        }

        public Vector3 value;
    }

    [Serializable]
    public class Vector4Value : AbstractValue
    {
        public Vector4Value()
        {
            valueType = ValueType.Vector4;
        }

        public Vector4 value;
    }

    [Serializable]
    public class RectValue : AbstractValue
    {
        public RectValue()
        {
            valueType = ValueType.Rect;
        }
        
        public Rect value;
    }
    
    [Serializable]
    public class BoundsValue : AbstractValue
    {
        public BoundsValue()
        {
            valueType = ValueType.Rect;
        }
        
        public Bounds value;
    }

    [Serializable]
    public class ColorValue : AbstractValue
    {
        public ColorValue()
        {
            valueType = ValueType.Color;
        }

        public Color value;
    }

    [Serializable]
    public class CurveValue : AbstractValue
    {
        public CurveValue()
        {
            valueType = ValueType.Curve;
        }

        public AnimationCurve value;
    }

    [Serializable]
    public class BoolValue : AbstractValue
    {
        public BoolValue()
        {
            valueType = ValueType.Bool;
        }

        public bool value;
    }
    
    [Serializable]
    public class ObjectValue : AbstractValue
    {
        public ObjectValue()
        {
            valueType = ValueType.Object;
            value = default;
        }

        public Object value;
    }
}