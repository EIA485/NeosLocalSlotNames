using NeosModLoader;
using HarmonyLib;
using FrooxEngine;
using FrooxEngine.UIX;

namespace LocalSlotNames
{
	public class LocalSlotNames : NeosMod
	{
		public override string Name => "LocalSlotNames";
		public override string Author => "eia485";
		public override string Version => "1.0.0";
		public override string Link => "https://github.com/EIA485/NeosLocalSlotNames";
		public override void OnEngineInit()
		{
			Harmony harmony = new Harmony("net.eia485.LocalSlotNames");
			harmony.PatchAll();
		}

		[HarmonyPatch(typeof(SlotInspector), "OnChanges")]
		class LocalSlotNamesPatch
        {
            static void Postfix(SyncRef<Text> ____slotNameText, SyncRef<Slot> ____rootSlot)
            {
                if ((____slotNameText.Target != null) & (!____slotNameText.Target.Content.IsLinked && !____slotNameText.Target.Content.IsDriven))
                {
                    ValueCopy<string> valueCopy = ____slotNameText.Target.Slot.AttachComponent<ValueCopy<string>>(true, null);
                    valueCopy.Source.Target = ____rootSlot.Target?.NameField;
                    valueCopy.Target.Target = ____slotNameText.Target.Content;
                }
            }
		}
    }
}