using MelonLoader;


namespace IronSpine
{
    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            IronSpineManager.Instance.Initialize(LoggerInstance);
        }
    }
}
