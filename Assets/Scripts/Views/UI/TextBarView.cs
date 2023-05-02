using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Test.Views.UI
{
    public class TextBarView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textField;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private Image _sliderFillImage;

        public void SetMaxValue(float maxValue)
        {
            _slider.maxValue = maxValue;
            SetValue(maxValue);
        }

        public void SetValue(float currentValue)
        {
            _textField.text = currentValue.ToString();
            _slider.value = currentValue;
        }

        public void SetColor(Color color)
        {
            _sliderFillImage.color = color;
        }
    }
}