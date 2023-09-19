using System;
using System.Collections.Generic;
using System.Linq;
using Dythervin.Collections;
using Dythervin.Core.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Data
{
    [Serializable]
    public class FactionMatrix
        : CollisionMatrix<int>, IFactionMatrix
#if UNITY_EDITOR
            , ISerializationCallbackReceiver
#endif
    {
        private const bool SelfIntersectionDefault = false;

        private const int NoneOffset = 1;
        public static readonly IReadOnlyList<Faction> FactionValues = Faction.None.GetValues(true).ToArray()[NoneOffset..];

        public int this[Faction a, Faction b]
        {
            get
            {
                Validate(a, b, out int aInt, out int bInt);
                return this[aInt, bInt];
            }
            set
            {
                Validate(a, b, out int aInt, out int bInt);
                this[aInt, bInt] = value;
            }
        }

#if UNITY_EDITOR
        static FactionMatrix()
        {
            CollisionMatrixHelper.RegisterType(typeof(FactionMatrix), Faction.None.GetNames(true).ToArray()[NoneOffset..]);
        }
#endif

        public FactionMatrix() : base(SelfIntersectionDefault)
        {
        }

        public FactionMatrix([NotNull] IReadOnlyCollisionMatrix<int> matrix) : base(matrix)
        {
        }

        private static void Validate(Faction a, Faction b, out int aInt, out int bInt)
        {
            Assert.IsTrue(a != b && a != Faction.None && b != Faction.None);
            aInt = (int)a - NoneOffset;
            bInt = (int)b - NoneOffset;
        }

#if UNITY_EDITOR
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            SetSize(FactionValues.Count, SelfIntersectionDefault);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            SetSize(FactionValues.Count, SelfIntersectionDefault);
        }
#endif
    }
}