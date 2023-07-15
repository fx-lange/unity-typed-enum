**Typed Enum** is a plugin that brings the benefits of the typed enum pattern to Unity, providing a lightweight and extendable base class, along with a custom property drawer for convenient selection of enum options using drop down fields in the inspector.

## Dependencies

* Unity 2022.2

## Installation

Install via Package Manager 
* [Add package via git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
* `https://github.com/fx-lange/unity-typed-enum.git`

## How to use

Extend ``TypedEnumBase`` and add static members.

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

## Known Issues

* Currently we compare the type in equals to make sure we don't compare frogs with apples. Possible overhead.
