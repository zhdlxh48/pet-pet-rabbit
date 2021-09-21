﻿using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FMOD
{
    public partial class VERSION
    {
#if DEVELOPMENT_BUILD
        public const string dllSuffix = "L";
#else
        public const string dllSuffix = "";
#endif
    }
}

namespace FMOD.Studio
{
    public partial class STUDIO_VERSION
    {
#if DEVELOPMENT_BUILD
        public const string dllSuffix = "L";
#else
        public const string dllSuffix = "";
#endif
    }
}

namespace FMODUnity
{
    // This class holds per-platform settings and provides hooks for platform-specific behaviour.
    // Each platform has a parent platform, forming a hierarchy that is rooted at PlatformDefault.
    // By default a platform inherits all of its properties from its parent platform; this behaviour
    // can be overridden for each property.
    //
    // There is at least one concrete derived class for each supported platform; these classes use
    // [InitializeOnLoad] and a static constructor to register themselves as supported platforms by
    // calling Settings.AddPlatformTemplate. The user can also create instances of the PlatformGroup
    // class and use them to group platforms that have settings in common.
    public abstract class Platform : ScriptableObject, IComparable<Platform>
    {
        // This is a persistent identifier. It is used:
        // * To link platforms together at load time
        // * To avoid creating duplicate platforms from templates (in Settings.OnEnable)
        // * As a key for SettingsEditor UI state
        // It should be kept stable for concrete platforms (like PlatformWindows) to support
        // settings migration in the future.
        [SerializeField]
        private string identifier;

        public string Identifier
        {
            get
            {
                return identifier;
            }

            set
            {
                identifier = value;
            }
        }

        // The display name to show for this platform in the UI.
        public abstract string DisplayName { get; }

        // Declares the Unity RuntimePlatforms and BuildTargets this platform implements.
        public abstract void DeclareUnityMappings(Settings settings);

#if UNITY_EDITOR
        // The old FMOD platform identifier that this platform corresponds to, for settings migration.
        public abstract Legacy.Platform LegacyIdentifier { get; }
#endif

        public const float DefaultPriority = 0;

        // The priority to use when finding a platform to support the current Unity runtime
        // platform (higher priorities are tried first).
        public virtual float Priority { get { return DefaultPriority; } }

        // Determines whether this platform matches the current environment. When more than one
        // platform implements the current Unity runtime platform, FMOD for Unity will use the
        // highest-priority platform that returns true from MatchesCurrentEnvironment.
        public virtual bool MatchesCurrentEnvironment { get { return true; } }

        // Whether this platform is a fixed part of the FMOD for Unity settings, or can be
        // added/removed by the user.
        public virtual bool IsIntrinsic { get { return false; } }

        // A hook for platform-specific initialization logic. RuntimeManager.Initialize calls this
        // before calling FMOD.Studio.System.create.
        public virtual void PreSystemCreate(Action<FMOD.RESULT, string> reportResult)
        {
        }

        // A hook for platform-specific initialization logic. RuntimeManager.Initialize calls this
        // just before calling studioSystem.Initialize.
        public virtual void PreInitialize(FMOD.Studio.System studioSystem)
        {
        }

        // The folder in which FMOD .bank files are stored. Used when loading banks.
        public virtual string GetBankFolder()
        {
            return Application.streamingAssetsPath;
        }

        // The location of the FMOD for Unity native libraries. Only non-null if this platform
        // requires an additional download.
        public virtual string GetLibraryPath()
        {
            return null;
        }

        // The base path for FMOD plugins when in a standalone player.
        protected virtual string GetPluginBasePath()
        {
            return string.Format("{0}/Plugins", Application.dataPath);
        }

        // The base path for FMOD plugins when in the Unity editor.
        protected virtual string GetEditorPluginBasePath()
        {
            return string.Format("{0}/FMOD/lib", GetPluginBasePath());
        }

        // Returns the full path for an FMOD plugin.
        public virtual string GetPluginPath(string pluginName)
        {
            throw new NotImplementedException(string.Format("Plugins are not implemented on platform {0}", Identifier));
        }

        // Loads a list of FMOD plugins.
        public virtual void LoadPlugins(FMOD.System coreSystem,
            Dictionary<string, uint> loadedPlugins, Action<FMOD.RESULT, string> reportResult)
        {
            List<string> pluginNames = Plugins;

            if (pluginNames == null)
            {
                return;
            }

            foreach (string pluginName in pluginNames)
            {
                if (string.IsNullOrEmpty(pluginName))
                {
                    continue;
                }

                string pluginPath = GetPluginPath(pluginName);
                uint handle;

                FMOD.RESULT result = coreSystem.loadPlugin(pluginPath, out handle);

#if UNITY_64 || UNITY_EDITOR_64
                // Add a "64" suffix and try again
                if (result == FMOD.RESULT.ERR_FILE_BAD || result == FMOD.RESULT.ERR_FILE_NOTFOUND)
                {
                    string pluginPath64 = GetPluginPath(pluginName + "64");
                    result = coreSystem.loadPlugin(pluginPath64, out handle);
                }
#endif

                reportResult(result, string.Format("Loading plugin '{0}' from '{1}'", pluginName, pluginPath));

                loadedPlugins.Add(pluginName, handle);
            }
        }

        // Ensures that this platform has properties.
        public void AffirmProperties()
        {
            if (!active)
            {
                Properties = new PropertyStorage();
                InitializeProperties();
                active = true;
            }
        }

        // Clears this platform's properties.
        public void ClearProperties()
        {
            if (active)
            {
                Properties = new PropertyStorage();
                active = false;
            }
        }

        // Initializes this platform's properties to their default values.
        protected virtual void InitializeProperties()
        {
            ParentIdentifier = PlatformDefault.ConstIdentifier;
        }

        [SerializeField]
        private string parentIdentifier;

        public string ParentIdentifier
        {
            get
            {
                return parentIdentifier;
            }

            set
            {
                parentIdentifier = value;
            }
        }

        [SerializeField]
        private float displaySortOrder;

        public float DisplaySortOrder
        {
            get
            {
                return displaySortOrder;
            }

            set
            {
                if (displaySortOrder != value)
                {
                    displaySortOrder = value;

                    if (Parent != null)
                    {
                        Parent.children.Sort();
                    }
                }
            }
        }

        public bool IsLiveUpdateEnabled
        {
            get
            {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
                return LiveUpdate != TriStateBool.Disabled;
#else
                return LiveUpdate == TriStateBool.Enabled;
#endif
            }
        }

        public bool IsOverlayEnabled
        {
            get
            {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
                return Overlay != TriStateBool.Disabled;
#else
                return Overlay == TriStateBool.Enabled;
#endif
            }
        }

        public int CompareTo(Platform other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return DisplaySortOrder.CompareTo(other.DisplaySortOrder);
            }
        }

        // A property value that can be inherited from the parent or overridden.
        public class Property<T>
        {
            public T Value;
            public bool HasValue;
        }

        // These stub classes are needed because Unity can't serialize generic classes
        [Serializable]
        public class PropertyBool : Property<TriStateBool>
        {
        }

        [Serializable]
        public class PropertyInt : Property<int>
        {
        }

        [Serializable]
        public class PropertySpeakerMode : Property<FMOD.SPEAKERMODE>
        {
        }

        [Serializable]
        public class PropertyString : Property<string>
        {
        }

        [Serializable]
        public class PropertyStringList : Property<List<string>>
        {
        }

        // This class provides access to a specific property on any Platform object; the property to
        // operate on is determined by the Getter function. This allows client code to operate on
        // platform properties in a generic manner.
        public struct PropertyAccessor<T>
        {
            private readonly Func<PropertyStorage, Property<T>> Getter;
            private readonly T DefaultValue;

            public PropertyAccessor(Func<PropertyStorage, Property<T>> getter, T defaultValue)
            {
                Getter = getter;
                DefaultValue = defaultValue;
            }

            // Determine whether the property has a value in the given platform, or is inherited
            // from the parent.
            public bool HasValue(Platform platform)
            {
                return platform.Active && Getter(platform.Properties).HasValue;
            }

            // Get the (possibly inherited) value of the property for the given platform.
            public T Get(Platform platform)
            {
                for (Platform current = platform; current != null; current = current.Parent)
                {
                    if (current.Active)
                    {
                        Property<T> property = Getter(current.Properties);

                        if (property.HasValue)
                        {
                            return property.Value;
                        }
                    }
                }

                return DefaultValue;
            }

            // Set the value of the property in the given platform, so it is not inherited from the
            // platform's parent.
            public void Set(Platform platform, T value)
            {
                Property<T> property = Getter(platform.Properties);

                property.Value = value;
                property.HasValue = true;
            }

            // Clear the value of the property in the given platform, so it is inherited from the
            // platform's parent.
            public void Clear(Platform platform)
            {
                Getter(platform.Properties).HasValue = false;
            }
        }

        // This class stores all of the inheritable properties for a platform.
        [Serializable]
        public class PropertyStorage
        {
            public PropertyBool LiveUpdate = new PropertyBool();
            public PropertyBool Overlay = new PropertyBool();
            public PropertyBool Logging = new PropertyBool();
            public PropertyInt SampleRate = new PropertyInt();
            public PropertyString BuildDirectory = new PropertyString();
            public PropertySpeakerMode SpeakerMode = new PropertySpeakerMode();
            public PropertyInt VirtualChannelCount = new PropertyInt();
            public PropertyInt RealChannelCount = new PropertyInt();
            public PropertyStringList Plugins = new PropertyStringList();
        }

        [SerializeField]
        private bool active = false;

        // Whether this platform is active in the settings UI.
        public bool Active { get { return active; } }

        // Whether this platform has any properties that are not inherited from the parent.
        public bool HasAnyOverriddenProperties
        {
            get
            {
                return active &&
                    (
                        Properties.LiveUpdate.HasValue
                        || Properties.Overlay.HasValue
                        || Properties.Logging.HasValue
                        || Properties.SampleRate.HasValue
                        || Properties.BuildDirectory.HasValue
                        || Properties.SpeakerMode.HasValue
                        || Properties.VirtualChannelCount.HasValue
                        || Properties.RealChannelCount.HasValue
                        || Properties.Plugins.HasValue
                    );
            }
        }

        [SerializeField]
        protected PropertyStorage Properties = new PropertyStorage();

        // These accessors provide (possibly inherited) property values.
        public TriStateBool LiveUpdate { get { return PropertyAccessors.LiveUpdate.Get(this); } }
        public TriStateBool Overlay { get { return PropertyAccessors.Overlay.Get(this); } }
        public TriStateBool Logging { get { return PropertyAccessors.Logging.Get(this); } }
        public int SampleRate { get { return PropertyAccessors.SampleRate.Get(this); } }
        public string BuildDirectory { get { return PropertyAccessors.BuildDirectory.Get(this); } }
        public FMOD.SPEAKERMODE SpeakerMode { get { return PropertyAccessors.SpeakerMode.Get(this); } }
        public int VirtualChannelCount { get { return PropertyAccessors.VirtualChannelCount.Get(this); } }
        public int RealChannelCount { get { return PropertyAccessors.RealChannelCount.Get(this); } }
        public List<string> Plugins { get { return PropertyAccessors.Plugins.Get(this); } }

        // These accessors provide full access to properties.
        public static class PropertyAccessors
        {
            public static readonly PropertyAccessor<TriStateBool> LiveUpdate
                    = new PropertyAccessor<TriStateBool>(properties => properties.LiveUpdate, TriStateBool.Disabled);

            public static readonly PropertyAccessor<TriStateBool> Overlay
                    = new PropertyAccessor<TriStateBool>(properties => properties.Overlay, TriStateBool.Disabled);

            public static readonly PropertyAccessor<TriStateBool> Logging
                    = new PropertyAccessor<TriStateBool>(properties => properties.Logging, TriStateBool.Disabled);

            public static readonly PropertyAccessor<int> SampleRate
                    = new PropertyAccessor<int>(properties => properties.SampleRate, 0);

            public static readonly PropertyAccessor<string> BuildDirectory
                    = new PropertyAccessor<string>(properties => properties.BuildDirectory, "Desktop");

            public static readonly PropertyAccessor<FMOD.SPEAKERMODE> SpeakerMode
                    = new PropertyAccessor<FMOD.SPEAKERMODE>(properties => properties.SpeakerMode, FMOD.SPEAKERMODE.STEREO);

            public static readonly PropertyAccessor<int> VirtualChannelCount
                    = new PropertyAccessor<int>(properties => properties.VirtualChannelCount, 128);

            public static readonly PropertyAccessor<int> RealChannelCount
                    = new PropertyAccessor<int>(properties => properties.RealChannelCount, 32);

            public static readonly PropertyAccessor<List<string>> Plugins
                    = new PropertyAccessor<List<string>>(properties => properties.Plugins, null);
        }

        [NonSerialized]
        private Platform parent;

        // The parent platform from which this platform inherits its property values.
        public Platform Parent
        {
            get { return parent; }

            set
            {
                if (value != parent)
                {
                    if (parent != null)
                    {
                        parent.children.Remove(this);
                    }

                    parent = value;

                    if (parent != null)
                    {
                        parent.children.Add(this);
                        parent.children.Sort();
                    }

                    ParentIdentifier = (parent != null) ? parent.Identifier : null;
                }
            }
        }

        [NonSerialized]
        private readonly List<Platform> children = new List<Platform>();

        // The platforms which inherit their property values from this platform.
        public List<Platform> Children { get { return children; } }

        // Checks whether this platform inherits from the given platform, so we can avoid creating
        // inheritance loops.
        public bool InheritsFrom(Platform platform)
        {
            if (platform == this)
            {
                return true;
            }
            else if (Parent != null)
            {
                return Parent.InheritsFrom(platform);
            }
            else
            {
                return false;
            }
        }
    }
}
