using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Observable<T>
{
    [SerializeField] T value = default;
    public bool strict;

    public delegate void ValueChanged(T value, T oldValue = default);
    public event ValueChanged OnValueChanged;
    public T Value
    {
        get { return value; }
        set
        {
            if (!this.value.Equals(value) || !strict)
            {
                if (OnValueChanged != null)
                    OnValueChanged(value, this.value);
                this.value = value;
            }
        }
    }

    public Observable(T value, bool strict = false)
    {
        this.value = value;
        this.strict = strict;
    }

    public override string ToString()
    {
        return "value = " + value + ", strict = " + strict;
    }

    // Allows to call for example "int i = myOInt" instead of "int i = myOInt.Value"
    public static implicit operator T(Observable<T> obs)
    {
        return obs.Value;
    }

    public void CallOnValueChanged()
    {
        if (OnValueChanged != null)
            OnValueChanged(value);
    }
}

// base types
[System.Serializable] public class OInt : Observable<int> { public OInt(int value = default, bool strict = false) : base(value, strict) { } }
[System.Serializable] public class OUInt : Observable<uint> { public OUInt(uint value = default, bool strict = false) : base(value, strict) { } }
[System.Serializable] public class OBool : Observable<bool> { public OBool(bool value = default, bool strict = false) : base(value, strict) { } }
[System.Serializable] public class OFloat : Observable<float> { public OFloat(float value = default, bool strict = false) : base(value, strict) { } }
[System.Serializable] public class OString : Observable<string> { public OString(string value = default, bool strict = false) : base(value, strict) { } }

// collections
public class OArray<T> : Observable<T[]> { public OArray(T[] value = default, bool strict = false) : base(value, strict) { } }
public class ODictionary<TKey, TValue> : Observable<Dictionary<TKey, TValue>> { public ODictionary(Dictionary<TKey, TValue> value = default, bool strict = false) : base(value, strict) { } }
public class OList<T> : Observable<List<T>> { public OList(List<T> value = default, bool strict = false) : base(value, strict) { } }

// typed collections need to be declared to appear in the inspector
[System.Serializable] public class IntOArray : OArray<int> { }

// Unity engine
[System.Serializable] public class OTransform : Observable<Transform> { public OTransform(Transform value = default, bool strict = false) : base(value, strict) { } }
[System.Serializable] public class OGameObject : Observable<GameObject> { public OGameObject(GameObject value = default, bool strict = false) : base(value, strict) { } }
[System.Serializable] public class OColor : Observable<Color> { public OColor(Color value = default, bool strict = false) : base(value, strict) { } }