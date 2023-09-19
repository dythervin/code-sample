using Dythervin.AutoAttach;
using Dythervin.UpdateSystem;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.View.Utils
{
    public class TempLogText : LogText, IUpdatableDelta
    {
        private const float FadeDuration = 0.5f;

        private event Action<TempLogText> OnCompleted;

        [AttachOrAdd]
        [SerializeField] private CanvasGroup group;
        [Attach(Attach.Child)]
        [SerializeField] private Image durationProgress;

        private float _left;
        private float _duration;

        public void InitFly(float duration, Action<TempLogText> onComplete)
        {
            Assert.IsTrue(duration > 0);
            _left = duration;
            _duration = duration;
            durationProgress.fillAmount = 1;
            group.alpha = 1;
            OnCompleted = onComplete;
            this.SetUpdater(true);
        }

        void IUpdatableDelta.OnUpdate(float deltaTime)
        {
            //RectTransform.anchoredPosition += new Vector2(0, _speed * deltaTime);
            _left -= deltaTime;

            if (_left > 0)
            {
                if (_left < FadeDuration)
                {
                    group.alpha = _left / FadeDuration;
                }

                durationProgress.fillAmount = _left / _duration;
            }
            else
            {
                this.SetUpdater(false);
                OnCompleted.Invoke(this);
            }
        }
    }
}