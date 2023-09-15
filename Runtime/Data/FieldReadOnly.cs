using System;
using Dythervin.AssetIdentifier;
using Dythervin.AssetIdentifier.Addressables;
using Dythervin.Data.Abstractions;
using Sirenix.OdinInspector;
using UnityEngine;
#if ODIN_INSPECTOR && UNITY_EDITOR
#endif

namespace Dythervin.Game.Framework.Data
{
    [Serializable]
    public class FieldReadOnly<T> : IVarReadOnly<T>
    {
        [SerializeField] private bool isRef;
#if ODIN_INSPECTOR && UNITY_EDITOR
        [HideIf(nameof(isRef))]
#endif
        [SerializeField]
        private T value;

#if ODIN_INSPECTOR && UNITY_EDITOR
        [ShowIf(nameof(isRef))]
#endif
        [SerializeField]
        private AssetIdRef<IVarReadOnly<T>> reference;

        public T Value => isRef ? reference.Load().Value : value;

        public FieldReadOnly(T value)
        {
            isRef = false;
            this.value = value;
            reference = default;
        }

        public FieldReadOnly(AssetIdRef<IVarReadOnly<T>> value)
        {
            isRef = true;
            this.value = default;
            reference = value;
        }

        public static implicit operator T(in FieldReadOnly<T> value)
        {
            return value.Value;
        }

        // public static implicit operator VarReadOnly<T>(T value)
        // {
        //     return new VarReadOnly<T>(value);
        // }

        public bool IsConst => true;
    }
}