﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Onlooker.Common.StringResources.Configuration {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ConfigurationProgress {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ConfigurationProgress() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Onlooker.Common.StringResources.Configuration.ConfigurationProgress", typeof(ConfigurationProgress).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The configuration file &apos;{0}&apos; does not exist..
        /// </summary>
        internal static string ConfigurationFileMissing {
            get {
                return ResourceManager.GetString("ConfigurationFileMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to deserialize &apos;{0}&apos; due to {1}.
        /// </summary>
        internal static string DeserializeFailure {
            get {
                return ResourceManager.GetString("DeserializeFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Successfully deserialized &apos;{0}&apos;.
        /// </summary>
        internal static string DeserializeSuccess {
            get {
                return ResourceManager.GetString("DeserializeSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loaded &apos;{0}&apos;..
        /// </summary>
        internal static string FileLoaded {
            get {
                return ResourceManager.GetString("FileLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; received an icon with an incorrect format &apos;{1}&apos;..
        /// </summary>
        internal static string FileReceivedIncorrectIconFormat {
            get {
                return ResourceManager.GetString("FileReceivedIncorrectIconFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; received a file that does not exist &apos;{1}&apos;..
        /// </summary>
        internal static string FileReceivedInvalidFile {
            get {
                return ResourceManager.GetString("FileReceivedInvalidFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Successfully loaded icon &apos;{0}&apos;..
        /// </summary>
        internal static string IconLoaded {
            get {
                return ResourceManager.GetString("IconLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WARNING: The property &apos;{0}&apos; has a ConfigLocation attribute, but is readonly..
        /// </summary>
        internal static string ReadonlyConfigGroupProperty {
            get {
                return ResourceManager.GetString("ReadonlyConfigGroupProperty", resourceCulture);
            }
        }
    }
}
