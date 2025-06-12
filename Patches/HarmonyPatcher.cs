using BepInEx;
using HarmonyLib;
using System.Reflection;
using BepInEx.Unity.IL2CPP;

namespace CapuchinTemplate.Patches
{
    public class HarmonyPatcher
    {
        public static Harmony Patch(string UUID)
        {
            Harmony thisHarmony = new Harmony(UUID);
            thisHarmony.PatchAll(Assembly.GetCallingAssembly());

            return thisHarmony;
        }

        public static Harmony PatchAssembly(string UUID, Assembly assemblyToPatch)
        {
            Harmony thisHarmony = new Harmony(UUID);
            thisHarmony.PatchAll(assemblyToPatch);

            return thisHarmony;
        }

        public static void Unpatch(Harmony instance) =>
            instance.UnpatchSelf();
    }
}
