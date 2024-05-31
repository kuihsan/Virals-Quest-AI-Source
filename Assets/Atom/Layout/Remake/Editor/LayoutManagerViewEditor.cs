using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(LayoutManagerView)), CanEditMultipleObjects]
    public class LayoutManagerViewEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           PresetType,
           HeaderColor,
           HeaderTransparency,
           FooterColor,
           FooterTransparency,
           BackgroundColor,
           BackgroundTransparency,
           PanelColor,
           PanelTransparency,
           IconColor,
           IconTransparency,
           PrimaryNormalColor,
           PrimaryHighlightColor,
           PrimaryPressedColor,
           PrimarySelectedColor,
           PrimaryDisabledColor,
           PrimaryButtonTransparency,
           SecondaryNormalColor,
           SecondaryHighlightColor,
           SecondaryPressedColor,
           SecondarySelectedColor,
           SecondaryDisabledColor,
           SecondaryButtonTransparency
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            PresetType = serializedObject.FindProperty("PresetType");
            HeaderColor = serializedObject.FindProperty("HeaderColor");
            HeaderTransparency = serializedObject.FindProperty("HeaderTransparency");
            FooterColor = serializedObject.FindProperty("FooterColor");
            FooterTransparency = serializedObject.FindProperty("FooterTransparency");
            BackgroundColor = serializedObject.FindProperty("BackgroundColor");
            BackgroundTransparency = serializedObject.FindProperty("BackgroundTransparency");
            PanelColor = serializedObject.FindProperty("PanelColor");
            PanelTransparency = serializedObject.FindProperty("PanelTransparency");
            IconColor = serializedObject.FindProperty("IconColor");
            IconTransparency = serializedObject.FindProperty("IconTransparency");
            PrimaryNormalColor = serializedObject.FindProperty("PrimaryNormalColor");
            PrimaryHighlightColor = serializedObject.FindProperty("PrimaryHighlightColor");
            PrimaryPressedColor = serializedObject.FindProperty("PrimaryPressedColor");
            PrimarySelectedColor = serializedObject.FindProperty("PrimarySelectedColor");
            PrimaryDisabledColor = serializedObject.FindProperty("PrimaryDisabledColor");
            PrimaryButtonTransparency = serializedObject.FindProperty("PrimaryButtonTransparency");
            SecondaryNormalColor = serializedObject.FindProperty("SecondaryNormalColor");
            SecondaryHighlightColor = serializedObject.FindProperty("SecondaryHighlightColor");
            SecondaryPressedColor = serializedObject.FindProperty("SecondaryPressedColor");
            SecondarySelectedColor = serializedObject.FindProperty("SecondarySelectedColor");
            SecondaryDisabledColor = serializedObject.FindProperty("SecondaryDisabledColor");
            SecondaryButtonTransparency = serializedObject.FindProperty("SecondaryButtonTransparency");
        }

        public Color ConvertColor(string aColor, float aTransparency)
        {
            Color result = Color.black;
            ColorUtility.TryParseHtmlString(aColor, out result);
            result.a = aTransparency;
            return result;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(PresetType, true);
                EditorGUILayout.PropertyField(HeaderColor, true);
                EditorGUILayout.PropertyField(HeaderTransparency, true);
                EditorGUILayout.PropertyField(FooterColor, true);
                EditorGUILayout.PropertyField(FooterTransparency, true);
                EditorGUILayout.PropertyField(BackgroundColor, true);
                EditorGUILayout.PropertyField(BackgroundTransparency, true);
                EditorGUILayout.PropertyField(PanelColor, true);
                EditorGUILayout.PropertyField(PanelTransparency, true);
                EditorGUILayout.PropertyField(IconColor, true);
                EditorGUILayout.PropertyField(IconTransparency, true);
                EditorGUILayout.PropertyField(PrimaryNormalColor, true);
                EditorGUILayout.PropertyField(PrimaryHighlightColor, true);
                EditorGUILayout.PropertyField(PrimaryPressedColor, true);
                EditorGUILayout.PropertyField(PrimarySelectedColor, true);
                EditorGUILayout.PropertyField(PrimaryDisabledColor, true);
                EditorGUILayout.PropertyField(PrimaryButtonTransparency, true);
                EditorGUILayout.PropertyField(SecondaryNormalColor, true);
                EditorGUILayout.PropertyField(SecondaryHighlightColor, true);
                EditorGUILayout.PropertyField(SecondaryPressedColor, true);
                EditorGUILayout.PropertyField(SecondarySelectedColor, true);
                EditorGUILayout.PropertyField(SecondaryDisabledColor, true);
                EditorGUILayout.PropertyField(SecondaryButtonTransparency, true);

                if ((LayoutManagerView.CPresetType)PresetType.enumValueIndex == LayoutManagerView.CPresetType.Default)
                {
                    HeaderColor.colorValue = ConvertColor("#43454B", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#43454B", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#43454B", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#43454B", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#43454B", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#43454B", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#9FA2AB", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#C8C9CD", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#AEB6C1", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#43454B", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#43454B", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#9FA2AB", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#C8C9CD", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#AEB6C1", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#43454B", SecondaryButtonTransparency.floatValue);
                }
                else if ((LayoutManagerView.CPresetType)PresetType.enumValueIndex == LayoutManagerView.CPresetType.Vermilion)
                {
                    HeaderColor.colorValue = ConvertColor("#9F243D", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#9F243D", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#9F243D", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#9F243D", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#9F243D", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#9F243D", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#FF9358", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#F8B997", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#F6AA83", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#9F243D", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#9F243D", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#FF9358", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#F8B997", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#F6AA83", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#9F243D", SecondaryButtonTransparency.floatValue);
                }
                else if ((LayoutManagerView.CPresetType)PresetType.enumValueIndex == LayoutManagerView.CPresetType.Emerald)
                {
                    HeaderColor.colorValue = ConvertColor("#3CC85A", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#3CC85A", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#3CC85A", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#3CC85A", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#3CC85A", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#3CC85A", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#BEC83B", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#DBE37F", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#D0D969", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#3CC85A", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#3CC85A", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#BEC83B", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#DBE37F", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#D0D969", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#3CC85A", SecondaryButtonTransparency.floatValue);
                }
                else if ((LayoutManagerView.CPresetType)PresetType.enumValueIndex == LayoutManagerView.CPresetType.Azure)
                {
                    HeaderColor.colorValue = ConvertColor("#2845B3", HeaderTransparency.floatValue);
                    FooterColor.colorValue = ConvertColor("#2845B3", FooterTransparency.floatValue);
                    BackgroundColor.colorValue = ConvertColor("#2845B3", BackgroundTransparency.floatValue);
                    PanelColor.colorValue = ConvertColor("#2845B3", PanelTransparency.floatValue);
                    IconColor.colorValue = ConvertColor("#2845B3", IconTransparency.floatValue);

                    PrimaryNormalColor.colorValue = ConvertColor("#2845B3", PrimaryButtonTransparency.floatValue);
                    PrimaryHighlightColor.colorValue = ConvertColor("#ABAB2B", PrimaryButtonTransparency.floatValue);
                    PrimaryPressedColor.colorValue = ConvertColor("#FFFBE6", PrimaryButtonTransparency.floatValue);
                    PrimarySelectedColor.colorValue = ConvertColor("#ADBCCF", PrimaryButtonTransparency.floatValue);
                    PrimaryDisabledColor.colorValue = ConvertColor("#2845B3", PrimaryButtonTransparency.floatValue);

                    SecondaryNormalColor.colorValue = ConvertColor("#2845B3", SecondaryButtonTransparency.floatValue);
                    SecondaryHighlightColor.colorValue = ConvertColor("#ABAB2B", SecondaryButtonTransparency.floatValue);
                    SecondaryPressedColor.colorValue = ConvertColor("#FFFBE6", SecondaryButtonTransparency.floatValue);
                    SecondarySelectedColor.colorValue = ConvertColor("#ADBCCF", SecondaryButtonTransparency.floatValue);
                    SecondaryDisabledColor.colorValue = ConvertColor("#2845B3", SecondaryButtonTransparency.floatValue);
                }


            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}