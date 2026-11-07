using FeatSettings;
using Il2Cpp;
using Il2CppTLD.Stats;
using System.Globalization;


namespace IronSpine
{
    public class IronSpineFeat : CustomFeatDefinition
    {
        private const string KilogramKilometersKey = "KilogramKilometers";
        private IronSpineSettings mSettings = new IronSpineSettings();

        public IronSpineSettings IronSpineSettings { get { return mSettings; } }

        public override FeatType FeatType { get { return (FeatType)13; } }
        public override string Name { get { return "IronSpine"; } }
        public override string DisplayName { get { return "Iron Spine"; } }
        public override string Description { get { return "Hauling heavy loads over long distances has hardened your back.\n\nUnlock by carrying a total of 10,000 kg·km: kilograms carried times kilometers walked."; } }
        public override string ShortDescription { get { return "Carry 10,000 kg·km"; } }
        public override string BonusDescription { get { return "+3 kg carry capacity"; } }
        public override FeatType BadgeFeat { get { return FeatType.SnowWalker; } }
        public override CustomFeatSettings? Settings { get { return mSettings; } }

        public override void OnStatIncremented(StatID statID, float increment)
        {
            if (statID != StatID.DistanceTravelled) return;
            IronSpineManager.Instance.OnDistanceTravelled(increment);
        }

        public override void ApplySettings()
        {
            IronSpineManager.Instance.UpdateProgress();
        }

        public override void OnSave()
        {
            Save(KilogramKilometersKey, IronSpineManager.Instance.KilogramKilometers.ToString("R", CultureInfo.InvariantCulture));
        }

        public override void OnLoad()
        {
            string? value = Load(KilogramKilometersKey);
            IronSpineManager.Instance.KilogramKilometers = double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double kgkm) ? kgkm : 0.0;
        }
    }
}
