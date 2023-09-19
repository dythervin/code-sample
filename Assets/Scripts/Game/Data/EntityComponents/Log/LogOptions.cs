using System.ComponentModel;
using UnityEngine;

namespace Game.Data
{
    public readonly struct LogOptions
    {
        public readonly string text;

        public readonly Color color;

        public readonly LogType logType;

        public LogOptions(string text, LogType logType,
            [DefaultValue(nameof(Color) + "." + nameof(Color.white))] Color color)
        {
            this.text = text;
            this.color = color;
            this.logType = logType;
        }

        public LogOptions(string text, LogType logType)
        {
            this.text = text;
            this.logType = logType;

            color = this.logType switch
            {
                LogType.Assert => Color.red,
                LogType.Error => Color.red,
                LogType.Exception => Color.red,
                LogType.Warning => Color.yellow,
                _ => Color.white
            };
        }

        public static implicit operator LogOptions(string text)
        {
            return new LogOptions(text, LogType.Log);
        }
    }
}