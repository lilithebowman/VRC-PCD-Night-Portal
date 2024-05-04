
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Texel
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PortalAudioConfig : UdonSharpBehaviour
    {
        [SerializeField] internal StreamControl streamControl;
        [SerializeField] internal AudioOverrideZone portalAudioZone;
        [SerializeField] internal AudioOverrideZone botAudioZone;

        void Start()
        {
            if (streamControl)
            {
                streamControl.PortalAudioZone = portalAudioZone;
                streamControl.BotAudioZone = botAudioZone;
            }
        }
    }
}
