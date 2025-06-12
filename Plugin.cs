using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using CapuchinTemplate.Patches;
using UnityEngine;
using Caputilla;

namespace CapuchinTemplate
{
    [BepInPlugin(ModInfo.GUID, ModInfo.Name, ModInfo.Version)]
    public class Init : BasePlugin
    {
        // I wouldn't recommend modifying this Init class as it can be hard to understand for new modders.
        // If you're experienced then probably ignore these comments as they're mostly here to guide new modders.
        public static Init instance;
        public Harmony harmonyInstance;

        public override void Load()
        {
            harmonyInstance = HarmonyPatcher.Patch(ModInfo.GUID);
            instance = this;

            // If and only IF you're making custom MonoBehaviour's do this:
            // ClassInjector.RegisterTypeInIl2Cpp<CustomMonoBehaviour>();
            // If you don't do this, your MonoBehaviour will not be recognized by the game.

            AddComponent<Plugin>();
        }

        public override bool Unload()
        {
            if (harmonyInstance != null)
                HarmonyPatcher.Unpatch(harmonyInstance);

            return true;
        }
    }

    public class Plugin : MonoBehaviour
    {
        // This is the same as the Plugin class in a Gorilla Tag mod, you can use all MonoBehaviour methods such as Start() or Update() here.
        static bool inModded = false;

        void Start()
        {
            Debug.Log($"{ModInfo.Name} has loaded!");

            // Subscription to Caputilla events
            CaputillaManager.Instance.OnModdedJoin += OnModdedJoin;
            CaputillaManager.Instance.OnModdedLeave += OnModdedLeave;
        }

        void OnModdedJoin()
        {
            inModded = true;
        }

        void OnModdedLeave()
        {
            inModded = false;
        }

        void FixedUpdate()
        {
            if (inModded)
            {
                // The following code only runs when in a modded lobby!
            }
        }
    }
}
