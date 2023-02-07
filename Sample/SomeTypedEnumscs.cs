using System;
using TypedEnum;

namespace TypedEnumSample
{
    [Serializable]
    public class MyState : TypedEnumBase
    {
        public static MyState Open { get; } = new(0, nameof(Open));
        public static MyState Closed { get; } = new(1, nameof(Closed));
        public static MyState SomethingElse { get; } = new SpecialState(2, nameof(SomethingElse)); 
        
        protected MyState(int index, string value) : base(index, value)
        {
        }
    }
    
    [Serializable]
    public class SpecialState : MyState
    {
        protected internal SpecialState(int index, string value) : base(index, value)
        {
        }
    }

    [Serializable]
    public class Animal : TypedEnumBase
    {
        public static Animal Frog { get; } = new(0, nameof(Frog));
        public static Animal Bee { get; } = new(1, nameof(Bee));

        protected Animal(int index, string value) : base(index, value)
        {
        }
    }
}