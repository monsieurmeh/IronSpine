using FeatSettings;
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

        private MelonLogger.Instance? mLogger;
        private IronSpineFeat mFeat = new IronSpineFeat();

        public IronSpineFeat Feat { get { return mFeat; } }

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
    }
}
