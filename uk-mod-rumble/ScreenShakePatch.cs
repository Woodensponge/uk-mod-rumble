using UnityEngine;
using UnityEngine.InputSystem;

namespace RumbleMod
{
    public static class ScreenShakePatch
    {
        public static void ApplyHooks()
        {
            On.CameraController.Update += CameraController_Update;
            On.HookArm.Update += HookArm_Update;
        }

        private static void HookArm_Update(On.HookArm.orig_Update orig, HookArm self)
        {
            orig(self);
            if (RumbleMod.Instance.CurrentRumblePriority > RumbleMod.RumblePriority.Whiplash)
            {
                return;
            }
            if (self.state == HookState.Pulling)
            {
                RumbleMod.Instance.CurrentRumblePriority = RumbleMod.RumblePriority.Whiplash;
                RumbleMod.Instance.SetRumble(1f);
            }
            else
            {
                if (RumbleMod.Instance.CurrentRumblePriority == RumbleMod.RumblePriority.Whiplash)
                {
                    RumbleMod.Instance.CurrentRumblePriority = RumbleMod.RumblePriority.None;
                    RumbleMod.Instance.SetRumble(0f);
                }
            }
        }

        private static void CameraController_Update(On.CameraController.orig_Update orig, CameraController self)
        {
            orig(self);
            if (RumbleMod.Instance.CurrentRumblePriority == RumbleMod.RumblePriority.Screenshake)
            {
                RumbleMod.Instance.CurrentRumblePriority = RumbleMod.RumblePriority.None;
            }
            if (RumbleMod.Instance.CurrentRumblePriority > RumbleMod.RumblePriority.Screenshake)
            {
                return;
            }
            RumbleMod.Instance.CurrentRumblePriority = RumbleMod.RumblePriority.Screenshake;
            RumbleMod.Instance.SetRumble(Mathf.Clamp(self.cameraShaking / 2.0f, 0f, 1f));
        }

        public static void RemoveHooks()
        {
            On.CameraController.Update -= CameraController_Update;
        }
    }
}