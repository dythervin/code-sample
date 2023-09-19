using System;
using Dythervin.Collections;
using Dythervin.ObjectPool.Component.Global;
using Game.View.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class LogComponentView : EntityComponentView<ILogComponent>
    {
        [SerializeField] private LayoutGroup persistentLayoutGroup;

        [SerializeField] private RectTransform layoutGroup;

        [SerializeField] private PrefabPooled<TempLogText> logTextPrefab;

        private readonly Action<TempLogText> _onLogComplete;

        //private static readonly string[] Seq = new[] { "◜", "◝", "◟", "◞", };

        public LogComponentView()
        {
            _onLogComplete = OnComplete;
        }

        protected override void Init()
        {
            base.Init();
            Model.OnLog += DebugComponentOnLog;
        }

        private void DebugComponentOnLog(LogData logData)
        {
            bool isPersistent = false;

            TempLogText logText = logTextPrefab.Pool.Get();

            logText.transform.localScale = logTextPrefab.Prefab.transform.localScale;
            logText.LogData = logData;
            logText.RectTransform.SetParent(isPersistent ? persistentLayoutGroup.transform : layoutGroup, false);
            //logText.gameObject.SetActive(true);
            if (!isPersistent)
            {
                float distance = layoutGroup.sizeDelta.y;
                logText.InitFly(5, _onLogComplete);
            }
        }

        private void OnComplete(TempLogText obj)
        {
            logTextPrefab.Pool.Release(ref obj);
        }

        [Button]
        private void ShowAll()
        {
            var logs = Model.Logs;
            foreach (LogData logData in logs.ToEnumerable())
            {
                
            }
        }

        // void IUpdatableDelta.OnUpdate(float deltaTime)
        // {
        //     _elapsed += deltaTime;
        //     if (_data.duration > 0)
        //     {
        //         if (_elapsed > _data.duration)
        //         {
        //             this.SetUpdater(false);
        //             text.enabled = false;
        //             textAdd.enabled = false;
        //             progressBar.SetActive(false);
        //             return;
        //         }
        //     
        //         progressBarFill.fillAmount = _elapsed / _data.duration;
        //     }
        //
        //     if (_data.IsCycled)
        //     {
        //         const int frequency = 20;
        //         int frame = Time.frameCount;
        //         if ((frame - 1) / frequency != frame / frequency)
        //         {
        //             textAdd.text = Seq[frame / frequency % Seq.Length];
        //         }
        //     }
        // }
    }
}