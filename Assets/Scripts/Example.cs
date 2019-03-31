using UnityEngine;

public class Example : MonoBehaviour
{
    [Header("Base types")]
    public OInt demoOInt;
    public OUInt demoOUInt;
    public OBool demoOBool;
    public OFloat demoOFloat;
    public OString demoOString;

    [Header("Collections")]
    public IntOArray demoIntOArray;

    [Header("Unity engine")]
    public OTransform demoOTransform;
    public OGameObject demoOGameObject;
    public OColor demoOColor;

    void Start()
    {
        OInt nonStrictOInt = new OInt(0, false);
        OInt strictOInt = new OInt(0, true);

        nonStrictOInt.OnValueChanged += NonStrictOInt_OnValueChanged;
        strictOInt.OnValueChanged += StrictOInt_OnValueChanged;

        nonStrictOInt.Value = 1;
        // Displays "Non strict changed to 1"
        strictOInt.Value = 1;
        // Displays "Strict changed to 1"

        nonStrictOInt.Value = 1;
        // Displays "Non strict changed to 1"
        strictOInt.Value = 1;
        // No display, same value set and strict, so no event trigger

        nonStrictOInt.Value = 2;
        // Displays "Non strict changed to 2"
        strictOInt.Value = 2;
        // Displays "Strict changed to 2"

        // Possible thanks to the implicit operator T(Observable<T>)
        int i = nonStrictOInt;
    }

    void NonStrictOInt_OnValueChanged(int value, int oldValue)
    {
        Debug.Log("Non strict changed to " + value);
    }

    void StrictOInt_OnValueChanged(int value, int oldValue)
    {
        Debug.Log("Strict changed to " + value);
    }
}