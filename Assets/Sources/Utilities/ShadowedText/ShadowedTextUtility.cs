using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Balthazariy.Utilities
{
    public class ShadowedTextUtility : MonoBehaviour
    {
        #region Settings
        [Header("GENERAL SETTINGS")]
        [TextArea()]
        [SerializeField] private string _textValue;
        [SerializeField] private TMP_FontAsset _fontAsset;
        [SerializeField] private FontStyles _fontStyles;
        [SerializeField] private TextAlignmentOptions _alignmentOptions;
        [SerializeField] private bool _autoSize = true;
        [Space(5)]
        [SerializeField] private float _textSizeMax = 12;
        [SerializeField] private float _textSizeMin = 12;
        [Space(5)]
        [Header("SPECIFIC SETTINGS")]
        [SerializeField] private bool _changeRootObjectName = true;
        [SerializeField] private bool _isButtonTitleText;
        [SerializeField] private Button _button;
        [SerializeField] private bool _isRaycast = false;

        [Space(5)]
        [Header("FONTS SETTINGS")]
        [SerializeField] private Color _mainColor;
        [SerializeField] private Color _shadowColor;
        [SerializeField] private bool _isChangeOffset = false;
        [SerializeField] private Vector3 _shadowOffset;
        [Range(0, 1)]
        [SerializeField] private float _shadowThickness = 0.5f;
        [SerializeField] private bool _isWrapping = false;
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
            var childs = transform.childCount;
            for (var i = childs - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            if (gameObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
                DestroyImmediate(text);

            _initialize = false;
        }

        public void ReinitTextInEditor()
        {
            InitTextInEditor();
        }
        #endregion

        #region Initialize
        private void InitMainText() // this is main text but in the same time it's shadow text...
        {
            var shadowedTextObject = this.gameObject;
            TextMeshProUGUI shadowedText = null;
            if (shadowedTextObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
                shadowedText = text;
            else
                shadowedText = shadowedTextObject.AddComponent<TextMeshProUGUI>();

            if (_changeRootObjectName)
                shadowedTextObject.name = "Text_Shadowed";

            if (_isChangeOffset)
                shadowedTextObject.transform.localPosition = _shadowOffset;

            shadowedText.color = _shadowColor;
            SetupFontSettings(shadowedText);

            shadowedText.outlineWidth = _shadowThickness;
            shadowedText.outlineColor = _shadowColor;
            shadowedText.raycastTarget = false;
        }

        private void InitShadowText() // this is main text that showed above shadow
        {
            var textObject = new GameObject("Text_Main");
            textObject.transform.parent = gameObject.transform;
            textObject.transform.localScale = Vector3.one;
            textObject.transform.localPosition = _shadowOffset * -1;

            var shadowRectTransform = gameObject.GetComponent<RectTransform>();
            var rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, shadowRectTransform.rect.width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, shadowRectTransform.rect.height);

            var text = textObject.AddComponent<TextMeshProUGUI>();
            text.color = _mainColor;
            SetupFontSettings(text);

            if (_isButtonTitleText)
                _button.targetGraphic = text;

            text.raycastTarget = _isRaycast;
        }

        private void SetupFontSettings(TextMeshProUGUI targetText)
        {
            targetText.text = _textValue;
            targetText.alignment = _alignmentOptions;
            targetText.enableAutoSizing = _autoSize;
            targetText.fontSizeMax = _textSizeMax;
            targetText.fontSizeMin = _textSizeMin;
            targetText.font = _fontAsset;
            targetText.fontStyle = _fontStyles;
            targetText.enableWordWrapping = _isWrapping;
        }
        #endregion
    }
}