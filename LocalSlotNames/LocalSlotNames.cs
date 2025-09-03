using BepInEx;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using FrooxEngine;
using FrooxEngine.UIX;
using HarmonyLib;

namespace LocalSlotNames
{
    [ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
    [BepInDependency(BepInExResoniteShim.PluginMetadata.GUID)]
    public class LocalSlotNames : BasePlugin
    {
		public override void Load() => HarmonyInstance.PatchAll();

		[HarmonyPatch(typeof(SlotInspector), "OnChanges")]
		class LocalSlotNamesPatch
        {
            static void Postfix(SyncRef<Text> ____slotNameText, SyncRef<Slot> ____rootSlot)
            {
                if ((____slotNameText.Target != null) && (!____slotNameText.Target.Content.IsLinked && !____slotNameText.Target.Content.IsDriven))
                {
                    ValueCopy<string> valueCopy = ____slotNameText.Target.Slot.AttachComponent<ValueCopy<string>>(true, null);
                    valueCopy.Source.Target = ____rootSlot.Target?.NameField;
                    valueCopy.Target.Target = ____slotNameText.Target.Content;
                }
            }
		}
    }
}