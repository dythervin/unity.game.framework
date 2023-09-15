using System;
using System.Collections.Generic;
using Dythervin.Core.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dythervin.Game.Framework.View
{
    public static class DataAssetHelper
    {
        public static string GetAddressablesKey(Type dataType)
        {
            Assert.IsTrue(dataType.IsInstantiatable());
            return $"{nameof(DataAsset)}/{dataType.Name}";
        }

        public static string GetIAddressablesKey(Type dataType)
        {
            Assert.IsTrue(dataType.IsInterface);
            return $"{nameof(DataAsset)}/{dataType.Name[1..]}";
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/" + nameof(DataAsset) + nameof(SetAddressablesKey))]
        private static void SetAddressablesKey(MenuCommand menuCommand)
        {
            SetAddressablesKey((DataAsset)menuCommand.context);
        }
#endif
        public static void SetAddressablesKey(ScriptableObject scriptableObject,
            IReadOnlyCollection<string> labels = null)
        {
#if UNITY_EDITOR
            var settings = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings;
            var group = settings.DefaultGroup;
            // if (group == null)
            //     settings.CreateGroup(groupName, false, false, false, settings.DefaultGroup.Schemas, settings.DefaultGroup. );

            string path = AssetDatabase.GetAssetPath(scriptableObject);
            string guid = AssetDatabase.AssetPathToGUID(path);

            var entry = settings.CreateOrMoveEntry(guid, group, true, false);
            if (labels != null)
            {
                foreach (string label in labels)
                {
                    entry.labels.Add(label);
                }
            }

            entry.address = GetAddressablesKey(scriptableObject.GetType());
#endif
        }
    }
}