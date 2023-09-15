using Dythervin.Serialization;

namespace Dythervin.Game.Framework.Data
{
    public interface IModelData : IFeatureProvider, IDSerializable
    {
        bool IsReadOnly { get; }
    }
}