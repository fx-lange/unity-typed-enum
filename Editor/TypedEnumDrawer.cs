using UnityEditor;
using UnityEngine.UIElements;

namespace TypedEnum.Editor
{
    [CustomPropertyDrawer(typeof(TypedEnumBase))]
    public class TypedEnumDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                container.Add(new HelpBox($"{property.displayName} must be marked as [SerializeReference].",
                    HelpBoxMessageType.Error));
                return container; 
            }

            PopupField<TypedEnumBase> popup;
            var fieldType = fieldInfo.FieldType;
            var all = TypedEnumBase.ListAll(fieldType);
            if (all.Count == 0)
            {
                container.Add(new HelpBox($"Field type {fieldType} has no options.", 
                    HelpBoxMessageType.Error));
                return container;

            }
            var currentVal = property.managedReferenceValue as TypedEnumBase;
            if (currentVal == null || !all.Contains(currentVal))
            {
                popup = new PopupField<TypedEnumBase>(property.displayName, all, 0);
            }
            else
            {
                popup = new PopupField<TypedEnumBase>(property.displayName, all, currentVal);
            }
            
            popup.RegisterValueChangedCallback(changeEvent =>
            {
                property.managedReferenceValue = changeEvent.newValue;
                property.serializedObject.ApplyModifiedProperties();
            });
            
            container.Add(popup);
            return container;
        }
    }
}