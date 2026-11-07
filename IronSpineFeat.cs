using FeatSettings;
using Il2Cpp;
using Il2CppTLD.Stats;
using System.Globalization;
using UnityEngine;


namespace IronSpine
{
    public class IronSpineFeat : CustomFeatDefinition
    {
        private const string KilogramKilometersKey = "KilogramKilometers";
        private IronSpineSettings mSettings = new IronSpineSettings();
        private Texture2D? mUnlockedBadge;

        public IronSpineSettings IronSpineSettings { get { return mSettings; } }

        public override FeatType FeatType { get { return (FeatType)13; } }
        public override string Name { get { return "IronSpine"; } }
        public override string DisplayName { get { return "IRONSPINE_FEAT_NAME"; } }
        public override string Description { get { return "IRONSPINE_FEAT_DESCRIPTION"; } }
        public override string ShortDescription { get { return "IRONSPINE_FEAT_SHORT_DESCRIPTION"; } }
        public override string BonusDescription { get { return "IRONSPINE_FEAT_BONUS_DESCRIPTION"; } }
        public override FeatType BadgeFeat { get { return FeatType.SnowWalker; } }
        public override Texture2D? UnlockedBadgeTexture { get { return mUnlockedBadge ??= LoadBadge("IronSpine_Unlocked.png"); } }
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

        public override string FormatText(string text)
        {
            return text.Replace("{kgkm}", mSettings.Threshold.ToString("N0", CultureInfo.InvariantCulture))
                       .Replace("{bonus}", mSettings.BonusKg.ToString(CultureInfo.InvariantCulture));
        }

        private static Texture2D? LoadBadge(string name)
        {
            using Stream? stream = typeof(IronSpineFeat).Assembly.GetManifestResourceStream($"IronSpine.{name}");
            if (stream == null)
            {
                IronSpineManager.Instance.Log($"Badge {name} is missing from IronSpine.dll");
                return null;
            }
            using MemoryStream memory = new MemoryStream();
            stream.CopyTo(memory);
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (!ImageConversion.LoadImage(texture, memory.ToArray()))
            {
                IronSpineManager.Instance.Log($"Could not load badge {name}");
                return null;
            }
            texture.name = name;
            texture.hideFlags = HideFlags.DontUnloadUnusedAsset;
            return texture;
        }
    }
}
