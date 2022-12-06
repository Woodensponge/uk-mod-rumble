using UMM;
using BepInEx;
using UnityEngine;
using UnityEngine.InputSystem;

//https://twitter.com/ShammyTV/status/1599929049794519041

namespace RumbleMod
{
#if UMM
    [UKPlugin("Rumble", "1.0.1", "Adds controller (or not a controller) rumble into the game.", true, true)]
    public class RumbleMod : UKMod
    {
        public enum RumblePriority
        {
            None,
            Screenshake,
            Whiplash
        }

        public static RumbleMod Instance;
        float currentRumble = 0f;
        RumblePriority currentRumblePriority;

        public float CurrentRumble { get => currentRumble; }
        public RumblePriority CurrentRumblePriority { get => currentRumblePriority; set => currentRumblePriority = value; }

        public override void OnModLoaded()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            Instance = this;

            ScreenShakePatch.ApplyHooks();
        }

        public void SetRumble(float rumble)
        {
            if (Gamepad.current == null)
            {
                return;
            }
            Gamepad.current.SetMotorSpeeds(rumble, rumble);
        }

        public override void OnModUnload()
        {
            ScreenShakePatch.RemoveHooks();
        }
    }
#endif

#if BEPINEX
    [BepInPlugin("woodensponge.ultrakill.rumble", "Rumble Mod", "1.0.0")]
    [BepInProcess("ULTRAKILL.exe")]
    public class RumbleMod : BaseUnityPlugin
    {
        public enum RumblePriority
        {
            None,
            Screenshake,
            Whiplash
        }

        public static RumbleMod Instance;
        float currentRumble = 0f;
        RumblePriority currentRumblePriority;

        public float CurrentRumble { get => currentRumble; }
        public RumblePriority CurrentRumblePriority { get => currentRumblePriority; set => currentRumblePriority = value; }

        public void Start()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            Instance = this;

            ScreenShakePatch.ApplyHooks();
        }

        public void SetRumble(float rumble)
        {
            if (Gamepad.current == null)
            {
                return;
            }
            Gamepad.current.SetMotorSpeeds(rumble, rumble);
        }

        public void OnDestroy()
        {
            ScreenShakePatch.RemoveHooks();
        }
    }
#endif
}
