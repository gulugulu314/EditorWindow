using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class mEditorWindow : EditorWindow {

    private string mystring = "Hello World";
    private bool groupEnable = false;
    private bool mBool = false;
    private float mFloat = 1.23f;

    //BeginFadeGroup
    AnimBool m_ShowExtraFields;
    Color mcolor = Color.white;
    string mstring = "";
    int number = 0;

    //intpopup
    int selectedsize = 2;
    string[] names = new string[]{ "Normal", "Double", "Qurdruple" };
    int[] sizes  =new int[] { 1, 2, 3 };

    //初始化窗口
    [MenuItem("Window/MyEditorWindow")]
    static void init()
    {
        EditorWindow.GetWindow(typeof(mEditorWindow)).Show();
    }

    void OnEnable()
    {
        m_ShowExtraFields = new AnimBool(true);
        m_ShowExtraFields.valueChanged.AddListener(Repaint);
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Setting", EditorStyles.boldLabel);
        mystring = EditorGUILayout.TextField("Text field", mystring);

        groupEnable = EditorGUILayout.BeginToggleGroup("Optional Setting", groupEnable);
        mBool = EditorGUILayout.Toggle("Toggle", mBool);
        mFloat = EditorGUILayout.Slider("Slider",mFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();


        //beginfadegroup
        m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("Show Extra Field", m_ShowExtraFields.target);

        if(EditorGUILayout.BeginFadeGroup(m_ShowExtraFields.faded))
        {
            EditorGUI.indentLevel++; //缩进值
            EditorGUILayout.PrefixLabel("Color");
            mcolor = EditorGUILayout.ColorField(mcolor);
            EditorGUILayout.PrefixLabel("Text");
            mstring = EditorGUILayout.TextField(mstring);
            EditorGUILayout.PrefixLabel("Number");
            number = EditorGUILayout.IntField(number);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        //beginhorizontalgroup
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Hello World");
        GUILayout.Label("Second Label");
        GUILayout.Button("FirstButton");
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginVertical();
        
        GUILayout.Label("Hello World");
        GUILayout.Label("Second Label");
        GUILayout.Button("FirstButton");
        EditorGUILayout.BeginVertical();

        //intpopup 使用弹出框
        selectedsize = EditorGUILayout.IntPopup("Resize LocalScale:", selectedsize, names, sizes);
        if (GUILayout.Button("Resize"))
        {
            ResizeScale();
        }

    }

    private void ResizeScale()
    {
        if (Selection.activeTransform)
        {
            Selection.activeTransform.localScale = new Vector3(selectedsize, selectedsize, selectedsize);
        }
        else
        {
            Debug.Log("No Object Selected, Please Selected an object in the scene window!");
        }
    }
}
