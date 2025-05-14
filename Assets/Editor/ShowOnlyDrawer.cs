using UnityEditor;
using UnityEngine;

// Ŀ���� �Ӽ��� ShowOnlyAttribute�� ���� �ʵ忡 ���� ����� ���� Drawer�� ����
[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    // Unity�� �ν����Ϳ��� �ش� �Ӽ��� �׸� �� ȣ��Ǵ� �޼���
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string valueString;

        // SerializedProperty�� Ÿ�Կ� ���� ���ڿ��� ��ȯ�Ͽ� �б� �������� �����ֱ� ���� switch�� ���
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                valueString = property.intValue.ToString();     // int ���� ���ڿ��� ��ȯ
                break;
            case SerializedPropertyType.Float:
                valueString = property.floatValue.ToString();   // float ���� ���ڿ��� ��ȯ
                break;
            case SerializedPropertyType.String:
                valueString = property.stringValue;             // ���ڿ��� �״�� ���
                break;
            case SerializedPropertyType.Boolean:
                valueString = property.boolValue.ToString();    // bool ���� ���ڿ��� ��ȯ
                break;
            case SerializedPropertyType.ObjectReference:
                // ������Ʈ�� null�� �ƴϸ� �ش� �̸��� ���, null�̸� ( None ) ǥ��
                valueString = property.objectReferenceValue != null ? property.objectReferenceValue.name : "( None )";
                break;
            default:
                // �������� �ʴ� Ÿ���� ��� �⺻ �޽��� ǥ��
                valueString = "( Not Supported )";
                break;
        }
        // ���� �ν����Ϳ� �� �ʵ�� ��� (���� �Ұ����� ����)
        EditorGUI.LabelField(position, label.text, valueString);
    }
}