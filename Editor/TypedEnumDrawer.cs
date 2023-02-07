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

            PopupField<TypedEnumBase> popup;
            var fieldType = fieldInfo.FieldType;
            var all = TypedEnumBase.ListAll(fieldType);
            if (all.Count == 0)
            {
                container.Add(new HelpBox($"Field type {fieldType} has no options.", 
                    HelpBoxMessageType.Error));
                return container;

            }
            var currentVal = (TypedEnumBase)fieldInfo.GetValue(property.serializedObject.targetObject);
            if (currentVal == null || !all.Contains(currentVal))
            {
                popup = new PopupField<TypedEnumBase>(property.displayName, all, -1);
            }
            else
            {
                popup = new PopupField<TypedEnumBase>(property.displayName, all, currentVal);
            }

            popup.RegisterValueChangedCallback(changeEvent =>
            {
                var target = property.serializedObject.targetObject;   
                Undo.RecordObject(target,$"Set {property.name} of {target.name}");
                fieldInfo.SetValue(property.serializedObject.targetObject, changeEvent.newValue);
                EditorUtility.SetDirty(target);
                property.serializedObject.ApplyModifiedProperties();
            });
            container.Add(popup);

            container.RegisterCallback<AttachToPanelEvent>(evt =>
            {
                Undo.undoRedoPerformed += Repaint;
            });
            container.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                Undo.undoRedoPerformed -= Repaint;
            });
            return container;

            void Repaint()
            {
                popup.SetValueWithoutNotify((TypedEnumBase)fieldInfo.GetValue(property.serializedObject.targetObject));
            }
        }
    }
}