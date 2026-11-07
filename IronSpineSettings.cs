using FeatSettings;
using ModSettings;


namespace IronSpine
{
    public class IronSpineSettings : CustomFeatSettings
    {
        public IronSpineSettings() : base("IronSpine") { }

        [Section("Iron Spine")]
        [Name("Required kg·km")]
        [Description("Kilograms carried times kilometers walked needed to unlock Iron Spine.\nDefault: 10,000")]
        [Slider(1, 100000)]
        public int Threshold = 10000;

        [Name("Carry Capacity Bonus (kg)")]
        [Description("Extra carry capacity while Iron Spine is enabled.\nDefault: 3 kg")]
        [Slider(1, 20)]
        public int BonusKg = 3;
    }
}
