using TMPro;
using UnityEngine;

namespace Balthazariy.Utilities
{
    public class TextMeshProUguiUtility : MonoBehaviour
    {
        private GameObject _selfObject;

        private GameObject _mainTextObject;
        [HideInInspector] public TextMeshProUGUI MainText;

        private GameObject _shadowTextObject;
        [HideInInspector] public TextMeshProUGUI ShadowText;

        private bool _initialized;
        private bool _shadowInitialized;

        #region Text Settings

        #region Main Text

        [Header("SETTINGS")]
        [TextArea()]
        [SerializeField] private string _textValue = "Lorem Ipsum Dolore";
        [SerializeField] private TMP_FontAsset _fontAsset;
        [SerializeField] private FontStyles _fontStyles;
        [SerializeField] private TextAlignmentOptions _alignmentOptions = TextAlignmentOptions.Center;
        [SerializeField] private float _textSizeMax = 24;
        [SerializeField] private float _textSizeMin = 8;
        [SerializeField] private float _leftTextPadding = 8f;
        [SerializeField] private float _rightTextPadding = 8f;
        [Space(5)]
        [SerializeField] private bool _autoSize = true;
        [SerializeField] private bool _isRaycast = false;
        [SerializeField] private bool _isWrapping = false;
        [SerializeField] private Color _mainColor = Color.white;
        private bool _isHaveShadow;

        #endregion

        #region Shadow Text

        [HideInInspector] public Color ShadowColor = Color.black;
        [HideInInspector] public float ShadowThsickness = 0.5f;
        [HideInInspector] public Vector3 ShadowOffset;

        #endregion

        #endregion

        public bool GetInitState() => _initialized;
        public bool GetShadowInitState() => _shadowInitialized;
        public bool GetIsHaveShadow() => _isHaveShadow;
        public void SetIsHaveShadow(bool value) => _isHaveShadow = value;
        public TMP_FontAsset GetFontAsset() => _fontAsset;

        #region Editor Methods

        #region Main Text

        public void InitText()
        {
            InitTextVariables();

            SetupFontSettings(MainText);
            MainText.color = _mainColor;
            MainText.raycastTarget = _isRaycast;

            _initialized = true;
        }

        public void RefreshText()
        {
            RemoveText();

            InitText();
        }

        public void RemoveText()
        {
            _initialized = false;

            DestroyImmediate(_mainTextObject);
            MainText = null;
        }

        public void AddShadowToText()
        {
            InitShadowTextVariables();

            SetupFontSettings(ShadowText);
            ShadowText.color = ShadowColor;
            ShadowText.raycastTarget = false;
            ShadowText.outlineWidth = ShadowThsickness;
            ShadowText.outlineColor = ShadowColor;
            _shadowTextObject.transform.localPosition = ShadowOffset;

            _shadowInitialized = true;
        }

        #endregion

        #region Shadow Text

        public void RefreshShadow()
        {
            RemoveShadow();

            AddShadowToText();
        }

        public void RemoveShadow()
        {
            _shadowInitialized = false;

            DestroyImmediate(_shadowTextObject);
            ShadowText = null;
        }

        #endregion

        #endregion

        private void Awake()
        {
            _selfObject = this.gameObject;

            _mainTextObject = _selfObject.transform.Find("Text_Main").gameObject;
            _shadowTextObject = _selfObject.transform.Find("Text_Shadow").gameObject;

            MainText = _mainTextObject.GetComponent<TextMeshProUGUI>();
            ShadowText = _mainTextObject.GetComponent<TextMeshProUGUI>();
        }

        private void SetupFontSettings(TextMeshProUGUI targetText)
        {
            targetText.font = _fontAsset;
            targetText.fontSizeMax = _textSizeMax;
            targetText.fontSizeMin = _textSizeMin;
            targetText.fontStyle = _fontStyles;
            targetText.enableAutoSizing = _autoSize;
            targetText.enableWordWrapping = _isWrapping;
            targetText.margin = new Vector4(_leftTextPadding, 0, _rightTextPadding, 0);
            targetText.alignment = _alignmentOptions;
            targetText.text = _textValue;
        }

        private void InitTextVariables()
        {
            _selfObject = null;
            _selfObject = this.gameObject;

            Transform textObject = _selfObject.transform.Find("Text_Main");
            if (textObject == null)
            {
                _mainTextObject = new GameObject("Text_Main");
                _mainTextObject.transform.parent = _selfObject.transform;
                _mainTextObject.transform.localScale = Vector3.one;
                _mainTextObject.transform.localPosition = Vector3.zero;

                _mainTextObject.AddComponent<RectTransform>();

                UpdateTextObjectPositionAndAnchors(_mainTextObject);
            }
            else
                _mainTextObject = textObject.gameObject;

            if (_mainTextObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
                MainText = text;
            else
                MainText = _mainTextObject.AddComponent<TextMeshProUGUI>();
        }

        private void InitShadowTextVariables()
        {
            _selfObject = null;
            _selfObject = this.gameObject;

            Transform textObject = _selfObject.transform.Find("Text_Shadow");
            if (textObject == null)
            {
                _shadowTextObject = new GameObject("Text_Shadow");
                _shadowTextObject.transform.parent = _selfObject.transform;
                _shadowTextObject.transform.localScale = Vector3.one;
                _shadowTextObject.transform.localPosition = Vector3.zero;

                _shadowTextObject.transform.SetAsFirstSibling();

                _shadowTextObject.AddComponent<RectTransform>();

                UpdateTextObjectPositionAndAnchors(_shadowTextObject);
            }
            else
                _shadowTextObject = textObject.gameObject;

            if (_shadowTextObject.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
                ShadowText = text;
            else
                ShadowText = _shadowTextObject.AddComponent<TextMeshProUGUI>();
        }

        private void UpdateTextObjectPositionAndAnchors(GameObject textObject)
        {
            var shadowRectTransform = _selfObject.GetComponent<RectTransform>();
            var rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, shadowRectTransform.rect.width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, shadowRectTransform.rect.height);
        }
    }
}