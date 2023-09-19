using Dythervin.Collections;
using Dythervin.Core;
using Dythervin.Data.Asset;
using Game.Data;
using UnityEngine;

namespace Game
{
    [SingletonAsset]
    public class ResistanceCalculation : SingletonAsset<ResistanceCalculation>
    {
        [SerializeField]
        private SerializedDictionary<DamageType, ConstantCurveSo> values;

        [SerializeField]
        private ConstantCurveSo defaultCurve;

        public static float Calculate(DamageType damageType, int value)
        {
            if (!Instance.values.TryGetValue(damageType, out ConstantCurveSo curve))
                curve = Instance.defaultCurve;

            return value < 0
                ? -curve.EvaluateAt(-value)
                : curve.EvaluateAt(value);
        }
    }
}