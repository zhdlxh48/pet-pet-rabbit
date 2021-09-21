﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
namespace FMOD
{
    public partial class VERSION
    {
        public const string dll = "fmodstudio" + dllSuffix;
    }
}

namespace FMOD.Studio
{
    public partial class STUDIO_VERSION
    {
        public const string dll = "fmodstudio" + dllSuffix;
    }
}
#endif

namespace FMODUnity
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class PlatformMac : Platform
    {
        static PlatformMac()
        {
            Settings.AddPlatformTemplate<PlatformMac>("52eb9df5db46521439908db3a29a1bbb");
        }

        public override string DisplayName { get { return "macOS"; } }
        public override void DeclareUnityMappings(Settings settings)
        {
            settings.DeclareRuntimePlatform(RuntimePlatform.OSXPlayer, this);
#if UNITY_EDITOR
            settings.DeclareBuildTarget(BuildTarget.StandaloneOSX, this);
#endif
        }

#if UNITY_EDITOR
        public override Legacy.Platform LegacyIdentifier { get { return Legacy.Platform.Mac; } }
#endif

        public override string GetPluginPath(string pluginName)
        {
            return string.Format("{0}/{1}.bundle", GetPluginBasePath(), pluginName);
        }
    }
}
