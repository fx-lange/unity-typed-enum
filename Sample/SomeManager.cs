using TypedEnum;
using UnityEngine;

namespace TypedEnumSample
{
    public class SomeManager : MonoBehaviour
    {
        [SerializeReference] public MyState InitialState;

        [ContextMenu("Test")]
        private void Test()
        {
            if (InitialState == MyState.Open) 
            {
                Debug.Log("Open");
            }

            if (MyState.Open == Animal.Frog)
            {
                Debug.LogWarning("But frogs are no states?");
            }

            if (TypedEnumBase.ListAll(typeof(MyState)).Contains(Animal.Frog))
            {
                Debug.LogWarning("Frogs in states, shit!");
            }
        }
    }
}