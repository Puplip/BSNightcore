using IPA;
using IPALogger = IPA.Logging.Logger;

namespace BSNightcore
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        /// <summary>
        /// Use to send log messages through BSIPA.
        /// </summary>
        internal static IPALogger Log { get; private set; }

        [Init]
        public Plugin(IPALogger logger)
        {
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            NightcorePatch.Init();
        }

        [OnExit]
        public void OnApplicationQuit()
        {

        }

    }
}
