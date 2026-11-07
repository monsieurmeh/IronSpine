using FeatSettings;
using Il2Cpp;
using Il2CppTLD.IntBackedUnit;
using MelonLoader;


namespace IronSpine
{
    public class IronSpineManager
    {
        #region Lazy Singleton

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly IronSpineManager instance = new IronSpineManager();
        }

        private IronSpineManager() { }
        public static IronSpineManager Instance { get { return Nested.instance; } }

        #endregion

        private const double Threshold = 10000.0;
        private const int BonusKg = 3;
        private MelonLogger.Instance? mLogger;
        private IronSpineFeat mFeat = new IronSpineFeat();
        private double mKilogramKilometers = 0.0;

        public IronSpineFeat Feat { get { return mFeat; } }
        public double KilogramKilometers { get { return mKilogramKilometers; } set { mKilogramKilometers = value; UpdateProgress(); } }

        public void Initialize(MelonLogger.Instance logger)
        {
            mLogger = logger;
            if (!FeatSettingsManager.Instance.TryRegisterCustomFeat(mFeat))
            {
                Log("Could not register Iron Spine with FeatSettings!");
            }
        }

        public void Log(string msg)
        {
            mLogger?.Msg(msg);
        }

        public void OnDistanceTravelled(float meters)
        {
            Encumber encumber = GameManager.GetEncumberComponent();
            if (encumber == null) return;
            mKilogramKilometers += meters * 0.001 * encumber.GetGearWeightKG().ToQuantity(1.0f);
            UpdateProgress();
        }

        public void UpdateProgress()
        {
            mFeat.Progress = (float)(mKilogramKilometers / Threshold);
        }

        public void AddCarryCapacity(ref ItemWeight carryCapacityBuff)
        {
            if (!mFeat.IsUnlockedAndEnabled) return;
            carryCapacityBuff += ItemWeight.FromKilograms(BonusKg);
        }
    }
}
