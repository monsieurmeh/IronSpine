using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.IntBackedUnit;


namespace IronSpine
{
    internal class Patches
    {
        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.GetCarryCapacityKGBuff))]
        private static class PlayerManagerPatches_GetCarryCapacityKGBuff
        {
            private static void Postfix(ref ItemWeight __result)
            {
                IronSpineManager.Instance.AddCarryCapacity(ref __result);
            }
        }
    }
}
