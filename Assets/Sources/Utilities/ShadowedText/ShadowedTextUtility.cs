using TMPro;
using UnityEngine;

namespace Balthazariy.Utilities
{
    public class ShadowedTextUtility : MonoBehaviour
    {
        private GameObject _shadowedTextObject;
        private TextMeshProUGUI _shadowedText;

        private GameObject _textObject;
        private TextMeshProUGUI _text;

        #region Settings
        [Header("GENERAL SETTINGS")]
        [TextArea()]
        [SerializeField] private string _textValue;
        [SerializeField] private TMP_FontAsset _fontAsset;
        [SerializeField] private TextAlignmentOptions _alignmentOptions;
        [SerializeField] private bool _autoSize = true;
        [Space(5)]
        [SerializeField] private float _textSizeMax = 12;
        [SerializeField] private float _textSizeMin = 12;
        [Space(5)]
        [SerializeField] private bool _changeRootObjectName = true;

        [Space(5)]
        [Header("FONTS SETTINGS")]
        [SerializeField] private Color _mainColor;
        [SerializeField] private Color _shadowColor;

        [SerializeField] private Vector3 _shadowOffset;
        [Range(0, 1)]
        [SerializeField] private float _shadowThickness;
        #endregion

        private bool _initialize;


        #region Editor Methods
        public void InitTextInEditor()
        {
            if (_initialize)
                RemoveTextInEditor();

            InitMainText();

            InitShadowText();

            _initialize = true;
        }

        public void RemoveTextInEditor()
        {
            for (int i = 0; i < _shadowedTextObject.transform.childCount; i++)
                DestroyImmediate(_shadowedTextObject.transform.GetChild(i).gameObject);

            DestroyImmediate(_shadowedText);

            _initialize = false;
        }

        public void ReinitTextInEditor()
        {
            if (_initialize)
                RemoveTextInEditor();

            InitMainText();

            InitShadowText();

            _initialize = true;
        }
        #endregion

        #region Initialize
        private void InitMainText() // this is main text but in the same time it's shadow text...
        {
            _shadowedTextObject = this.gameObject;
            _shadowedText = _shadowedTextObject.AddComponent<TextMeshProUGUI>();

            if (_changeRootObjectName)
                _shadowedTextObject.name = "Text_Shadowed";

            _shadowedTextObject.transform.localPosition = _shadowOffset;

            _shadowedText.color = _shadowColor;
            SetupFontSettings(_shadowedText);

            _shadowedText.outlineWidth = _shadowThickness;
            _shadowedText.outlineColor = _shadowColor;
        }

        private void InitShadowText() // this is main text that showed above shadow
        {
            _textObject = new GameObject("Text_Main");
            _textObject.transform.parent = _shadowedTextObject.transform;
            _textObject.transform.localScale = Vector3.one;
            _textObject.transform.localPosition = _shadowOffset * -1;

            var rectTransform = _textObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;

            rectTransform.sizeDelta = _shadowedTextObject.GetComponent<RectTransform>().sizeDelta;

            _text = _textObject.AddComponent<TextMeshProUGUI>();
            _text.color = _mainColor;
            SetupFontSettings(_text);
        }

        private void SetupFontSettings(TextMeshProUGUI targetText)
        {
            targetText.text = _textValue;
            targetText.alignment = _alignmentOptions;
            targetText.enableAutoSizing = _autoSize;
            targetText.fontSizeMax = _textSizeMax;
            targetText.fontSizeMin = _textSizeMin;
            targetText.font = _fontAsset;
        }
        #endregion
    }
}