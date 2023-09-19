using System.Collections.Generic;
using Dythervin.AutoAttach;
using Dythervin.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.Utils
{
    public class LogText : MonoBehaviour
    {
        [Attach(Attach.Child)]
        [SerializeField] private TMP_Text text;

        [Attach(Attach.Child)]
        [SerializeField] private Button button;

        private LogData _logData;

        public RectTransform RectTransform => (RectTransform)transform;

        public LogData LogData
        {
            get => _logData;
            set
            {
                if (EqualityComparer<LogData>.Default.Equals(_logData, value))
                    return;

                _logData = value;
                SetData(value);
            }
        }

        protected virtual void SetData(LogData value)
        {
            text.text = value.value.text;
            text.color = value.value.color;
            if (text.fontSizeMax != text.fontSize)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(RectTransform);
            }
        }

        private void Awake()
        {
            //button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            DDebug.Log(_logData.value.logType, _logData.stackTrace);
        }
    }
}