using System;
using Dythervin.Serialization;

namespace Dythervin.Game.Framework.Data
{
    public interface IModelDataWrapper
    {
        IModelData WrappedData { get; }
    }

    public interface IModelDataWrapper<out TData> : IModelDataWrapper
        where TData : class, IModelData
    {
        new TData WrappedData { get; }
    }

    public interface IModelDataWrapped : IModelData, IModelDataWrapper
    {
        Type WrappedType { get; }
    }

    public interface IModelDataWrapped<out TData> : IModelDataWrapped, IModelDataWrapper<TData>
        where TData : class, IModelData
    {
    }

    public static class ModelDataWrappedExtensions
    {
        public static void EnsureNotWrapped<TObserverData>(this TObserverData data, out TObserverData observerData)
            where TObserverData : class, IModelData
        {
            while (data is IModelDataWrapped dataWrapped)
            {
                data = (TObserverData)dataWrapped.WrappedData;
            }

            observerData = data;
        }

        public static TObserverData EnsureNotWrapped<TObserverData>(this TObserverData data)
            where TObserverData : class, IModelData
        {
            data.EnsureNotWrapped(out data);
            return data;
        }

        public static void GetOrCopy<TObserverData>(this TObserverData data, out TObserverData observerData)
            where TObserverData : class, IModelData
        {
            bool isReadOnly = data.IsReadOnly;
            while (data is IModelDataWrapped dataWrapped)
            {
                data = (TObserverData)dataWrapped.WrappedData;
                if (!data.IsReadOnly)
                    isReadOnly = false;
            }

            observerData = isReadOnly ? data : data.GetSerializedCopy();
        }

        public static TObserverData GetOrCopy<TObserverData>(this TObserverData data)
            where TObserverData : class, IModelData
        {
            data.GetOrCopy(out data);
            return data;
        }
    }
}