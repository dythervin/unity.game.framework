using System;
using Dythervin.Common;
using Dythervin.Core.Extensions;

namespace Dythervin.Game.Framework.Data
{
    public struct Features
    {
        public static TypeID<IModelData> GetDataGroupId(Type type)
        {
            // if (type.Instantiatable())
            //     throw new ArgumentException();

            return TypeID<IModelData>.Get(type);
        }

        public static TypeID<IModelData> GetDataGroupId<TData>()
            where TData : IModelData
        {
            return GetDataGroupId(typeof(TData));
        }

        public static TypeID<IModelData> GetDataId(Type type)
        {
            // if (!type.Instantiatable())
            //     throw new ArgumentException();

            return TypeID<IModelData>.Get(type);
        }

        public static TypeID<IModelData> GetDataId<TData>(TData data)
            where TData : class, IModelData
        {
            return TypeID<IModelData>.Get(data.GetType());
        }

        public static TypeID<IModelData> GetDataId<TData>()
            where TData : IModelData
        {
            return GetDataId(typeof(TData));
        }
    }
}