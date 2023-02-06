# Typed Enum for Unity

*Work In Progress!*

Extendable and typed enum for Unity with custom inspector support.

## What?

You wouldn't be here if you haven't heard about the typed enum pattern in c# before, right? 
So all this plugin does is to provide an extendable but lightweight typed enum base class with a custom property drawer. 
The property drawer allows to select enum options via a popup field.

## Usage

Extend ``TypedEnumBase`` and add all your static members.

```c#
public class MyState : TypedEnumBase
{
    public static MyState Open { get; } = new(0, nameof(Open));
    public static MyState Closed { get; } = new(1, nameof(Closed));

    protected MyState(int index, string value) : base(index, value)
    {
    }
}
```

Add ``[SerializeReference]`` to gain inspector support. 

```c#
public class SomeManager : MonoBehaviour
{
    [SerializeReference] public MyState InitialState;
    ...
}
```

## Dependencies

* Unity 2022.2 (as the property drawer is only implemented in UIToolkit)

## Issues

* Currently we compare the type in equals to make sure we don't compare frogs with apples. Overhead?
* References don't match after domain reload, have to compare members anyway. Kick serializereference?