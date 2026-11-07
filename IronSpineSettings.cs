using FeatSettings;
using ModSettings;


namespace IronSpine
{
    public class IronSpineSettings : CustomFeatSettings
    {
        public IronSpineSettings() : base("IronSpine") { }

        [Section("IRONSPINE_FEAT_NAME", Localize = true)]
        [Name("IRONSPINE_Threshold_Name", Localize = true)]
        [Description("IRONSPINE_Threshold_Desc")]
        [Slider(1, 100000)]
        public int Threshold = 10000;

        [Name("IRONSPINE_BonusKg_Name", Localize = true)]
        [Description("IRONSPINE_BonusKg_Desc")]
        [Slider(1, 20)]
        public int BonusKg = 3;
    }
}
