using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    public class LayoutManagerView : MonoBehaviour
    {

        public enum CPresetType {  Default, Vermilion, Emerald, Azure, Custom  }

        [Space(10)]
        public bool isEnabled;

        [Header("Preset Settings")]
        public CPresetType PresetType;

        [Header("Header Color")]
        public Color HeaderColor;
        [Range(0, 1)] public float HeaderTransparency;

        [Header("Footer Color")]
        public Color FooterColor;
        [Range(0, 1)] public float FooterTransparency;

        [Header("Background Color")]
        public Color BackgroundColor;
        [Range(0, 1)] public float BackgroundTransparency;

        [Header("Panel Color")]
        public Color PanelColor;
        [Range(0, 1)] public float PanelTransparency;

        [Header("Icon Color")]
        public Color IconColor;
        [Range(0, 1)] public float IconTransparency;

        [Header("Primary Button Color")]
        public Color PrimaryNormalColor;
        public Color PrimaryHighlightColor;
        public Color PrimaryPressedColor;
        public Color PrimarySelectedColor;
        public Color PrimaryDisabledColor;
        [Range(0, 1)] public float PrimaryButtonTransparency;

        [Header("Secondary Button Color")]
        public Color SecondaryNormalColor;
        public Color SecondaryHighlightColor;
        public Color SecondaryPressedColor;
        public Color SecondarySelectedColor;
        public Color SecondaryDisabledColor;
        [Range(0, 1)] public float SecondaryButtonTransparency;

        public Color ConvertColor(string aColor, float aTransparency)
        {
            Color result = Color.black;
            ColorUtility.TryParseHtmlString(aColor, out result);
            result.a = aTransparency;
            return result;
        }

        public void SetLayoutDefault()
        {
            PresetType = CPresetType.Default;

            HeaderColor = ConvertColor("#43454B", HeaderTransparency);
            FooterColor = ConvertColor("#43454B", FooterTransparency);
            BackgroundColor = ConvertColor("#43454B", BackgroundTransparency);
            PanelColor = ConvertColor("#43454B", PanelTransparency);
            IconColor = ConvertColor("#43454B", IconTransparency);

            PrimaryNormalColor = ConvertColor("#43454B", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#9FA2AB", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#C8C9CD", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#AEB6C1", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#43454B", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#43454B", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#9FA2AB", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#C8C9CD", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#AEB6C1", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#43454B", SecondaryButtonTransparency);
        }

        public void SetLayoutVermilion()
        {
            PresetType = CPresetType.Vermilion;

            HeaderColor = ConvertColor("#9F243D", HeaderTransparency);
            FooterColor = ConvertColor("#9F243D", FooterTransparency);
            BackgroundColor = ConvertColor("#9F243D", BackgroundTransparency);
            PanelColor = ConvertColor("#9F243D", PanelTransparency);
            IconColor = ConvertColor("#9F243D", IconTransparency);

            PrimaryNormalColor = ConvertColor("#9F243D", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#FF9358", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#F8B997", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#F6AA83", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#9F243D", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#9F243D", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#FF9358", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#F8B997", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#F6AA83", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#9F243D", SecondaryButtonTransparency);
        }

        public void SetLayoutEmerald()
        {
            PresetType = CPresetType.Emerald;

            HeaderColor = ConvertColor("#3CC85A", HeaderTransparency);
            FooterColor = ConvertColor("#3CC85A", FooterTransparency);
            BackgroundColor = ConvertColor("#3CC85A", BackgroundTransparency);
            PanelColor = ConvertColor("#3CC85A", PanelTransparency);
            IconColor = ConvertColor("#3CC85A", IconTransparency);

            PrimaryNormalColor = ConvertColor("#3CC85A", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#BEC83B", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#DBE37F", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#D0D969", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#3CC85A", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#3CC85A", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#BEC83B", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#DBE37F", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#D0D969", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#3CC85A", SecondaryButtonTransparency);
        }

        public void SetLayoutAzure()
        {
            PresetType = CPresetType.Azure;

            HeaderColor = ConvertColor("#2845B3", HeaderTransparency);
            FooterColor = ConvertColor("#2845B3", FooterTransparency);
            BackgroundColor = ConvertColor("#2845B3", BackgroundTransparency);
            PanelColor = ConvertColor("#2845B3", PanelTransparency);
            IconColor = ConvertColor("#2845B3", IconTransparency);

            PrimaryNormalColor = ConvertColor("#2845B3", PrimaryButtonTransparency);
            PrimaryHighlightColor = ConvertColor("#ABAB2B", PrimaryButtonTransparency);
            PrimaryPressedColor = ConvertColor("#FFFBE6", PrimaryButtonTransparency);
            PrimarySelectedColor = ConvertColor("#ADBCCF", PrimaryButtonTransparency);
            PrimaryDisabledColor = ConvertColor("#2845B3", PrimaryButtonTransparency);

            SecondaryNormalColor = ConvertColor("#2845B3", SecondaryButtonTransparency);
            SecondaryHighlightColor = ConvertColor("#ABAB2B", SecondaryButtonTransparency);
            SecondaryPressedColor = ConvertColor("#FFFBE6", SecondaryButtonTransparency);
            SecondarySelectedColor = ConvertColor("#ADBCCF", SecondaryButtonTransparency);
            SecondaryDisabledColor = ConvertColor("#2845B3", SecondaryButtonTransparency);
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (isEnabled)
            {
                if (PresetType == CPresetType.Default)
                {
                    SetLayoutDefault();
                }
                else if (PresetType == CPresetType.Vermilion)
                {
                    SetLayoutVermilion();
                }
                else if (PresetType == CPresetType.Emerald)
                {
                    SetLayoutEmerald();
                }
                else if (PresetType == CPresetType.Azure)
                {
                    SetLayoutAzure();
                }
            }
        }

    }
}
