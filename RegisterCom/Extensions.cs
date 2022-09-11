using WixSharp;
using WixSharp.CommonTasks;

namespace WixSharp
{
    //
    // Zusammenfassung:
    //     Collection of generic WixSharp extension methods
    public static class Extensions
    {
        /// <summary>
        /// Registers a file in the registry (e.g. *.dll, *.ocx).<br/>
        /// <br/>
        /// It utilizes heat.exe to extract the registry entries from the file and adds them 
        /// to the build (*.wxs) file.<br/>
        /// Files that do not have any registration entries stay as they are.<br/>
        /// <br/>
        /// The following are examples on how to register files:<br/>
        /// <br/>
        /// 
        /// <example>
        /// Creates the corresponding registry entries for a given file.<br/>
        /// <code>
        /// new File(@"Some\File\Location")
        ///         .RegisterCom();
        /// </code>
        /// </example>
        /// 
        /// <example>
        /// Creates the corresponding registry entries for a given file as COM objects that are easier to read.<br/>
        /// Can lead to Candle errors because heat.exe sometimes leaves stuff empty.<br/>
        /// <code>
        /// new File(@"Some\File\Location")
        ///         .RegisterCom(true);
        /// </code>
        /// </example>
        /// 
        /// <example>
        /// Runs heat.exe with the additional argument '-gg' and omits the default arguments '-ag' and '-svb6'.<br/>
        /// <code>
        /// new File(@"Some\File\Location")
        ///         .RegisterCom(heatArguments: new[] { "-gg" }, overrideDefaults: true);
        /// </code>
        /// This comes down to the same as:<br/>
        /// <code>
        /// new File(@"Some\File\Location")
        ///         .RegisterCom(false, new[] { "-gg" }, true);
        /// </code>
        /// Or:<br/>
        /// <code>
        /// new File(@"Some\File\Location",
        ///     new RegisterCom()
        ///     {
        ///         HeatArguments = new[] { "-gg" },
        ///         OverrideDefaults = true
        ///     }
        /// );
        /// </code>
        /// </example>
        /// 
        /// </summary>
        /// 
        /// <param name="file">The file to register.</param>
        /// <param name="createComObjects">
        /// Whether to create the output as COM objects or plain registry entries (adds argument '-scom' if false).<br/>
        /// COM objects are easier to read but can lead to Candle errors because heat.exe sometimes leaves stuff empty.
        /// </param>
        /// <param name="heatArguments">Additional arguments that should be passed to heat.exe.</param>
        /// <param name="overrideDefaults">Omits the default arguments '-ag' and '-svb6' from the call to heat.exe if further customization is wanted.</param>
        /// <param name="hideWarnings">Whether to ignore the warnings returned by heat.exe and to stop forwarding them to the console.</param>
        public static File RegisterCom(this File file, bool createComObjects = false, string[] heatArguments = null, bool overrideDefaults = false, bool hideWarnings = false)
        {
            file.Add(new RegisterCom()
            {
                CreateComObjects    = createComObjects,
                HeatArguments       = heatArguments,
                OverrideDefaults    = overrideDefaults,
                HideWarnings        = hideWarnings
            });

            return file;
        }

        /// <summary>
        /// Registers multiple files in the registry (e.g. *.dll, *.ocx).<br/>
        /// <br/>
        /// It utilizes heat.exe to extract the registry entries from the files and adds them 
        /// to the build (*.wxs) file.<br/>
        /// Files that do not have any registration entries stay as they are.<br/>
        /// <br/>
        /// The following are examples on how to register files:<br/>
        /// <br/>
        /// 
        /// <example>
        /// Creates the corresponding registry entries for given files.<br/>
        /// <code>
        /// new Files(@"Some\File\Location\*.*")
        ///         .RegisterCom();
        /// </code>
        /// </example>
        /// 
        /// <example>
        /// Creates the corresponding registry entries for given files as COM objects that are easier to read.<br/>
        /// Can lead to Candle errors because heat.exe sometimes leaves stuff empty.<br/>
        /// <code>
        /// new Files(@"Some\File\Location\*.*")
        ///         .RegisterCom(true);
        /// </code>
        /// </example>
        /// 
        /// <example>
        /// Runs heat.exe with the additional argument '-gg' and omits the default arguments '-ag' and '-svb6'.<br/>
        /// <code>
        /// new Files(@"Some\File\Location\*.*")
        ///         .RegisterCom(heatArguments: new[] { "-gg" }, overrideDefaults: true);
        /// </code>
        /// This comes down to the same as:<br/>
        /// <code>
        /// new Files(@"Some\File\Location\*.*")
        ///         .RegisterCom(false, new[] { "-gg" }, true);
        /// </code>
        /// Or:<br/>
        /// <code>
        /// new Files(@"Some\File\Location\*.*")
        /// {
        ///     OnProcess = file =>
        ///     {
        ///         file.RegisterCom(false, new[] { "-gg" }, true);
        ///     }
        /// };
        /// </code>
        /// </example>
        /// 
        /// </summary>
        /// 
        /// <param name="files">The files to register.</param>
        /// <param name="createComObjects">
        /// Whether to create the output as COM objects or plain registry entries (adds argument '-scom' if false).<br/>
        /// COM objects are easier to read but can lead to Candle errors because heat.exe sometimes leaves stuff empty.
        /// </param>
        /// <param name="heatArguments">Additional arguments that should be passed to heat.exe.</param>
        /// <param name="overrideDefaults">Omits the default arguments '-ag' and '-svb6' from the call to heat.exe if further customization is wanted.</param>
        /// <param name="hideWarnings">Whether to ignore the warnings returned by heat.exe and to stop forwarding them to the console.</param>
        public static Files RegisterCom(this Files files, bool createComObjects = false, string[] heatArguments = null, bool overrideDefaults = false, bool hideWarnings = false)
        {
            files.OnProcess = file =>
            {
                file.RegisterCom(createComObjects, heatArguments, overrideDefaults, hideWarnings);
            };

            return files;
        }

        /// <summary>
        /// Registers multiple files in the registry (e.g. *.dll, *.ocx).<br/>
        /// <br/>
        /// It utilizes heat.exe to extract the registry entries from the files and adds them 
        /// to the build (*.wxs) file.<br/>
        /// Files that do not have any registration entries stay as they are.<br/>
        /// <br/>
        /// The following are examples on how to register files:<br/>
        /// <br/>
        /// 
        /// <example>
        /// Creates the corresponding registry entries for given files.<br/>
        /// <code>
        /// new DirFiles(@"Some\File\Location\*.*")
        ///         .RegisterCom();
        /// </code>
        /// </example>
        /// 
        /// <example>
        /// Creates the corresponding registry entries for given files as COM objects that are easier to read.<br/>
        /// Can lead to Candle errors because heat.exe sometimes leaves stuff empty.<br/>
        /// <code>
        /// new DirFiles(@"Some\File\Location\*.*")
        ///         .RegisterCom(true);
        /// </code>
        /// </example>
        /// 
        /// <example>
        /// Runs heat.exe with the additional argument '-gg' and omits the default arguments '-ag' and '-svb6'.<br/>
        /// <code>
        /// new DirFiles(@"Some\File\Location\*.*")
        ///         .RegisterCom(heatArguments: new[] { "-gg" }, overrideDefaults: true);
        /// </code>
        /// This comes down to the same as:<br/>
        /// <code>
        /// new DirFiles(@"Some\File\Location\*.*")
        ///         .RegisterCom(false, new[] { "-gg" }, true);
        /// </code>
        /// Or:<br/>
        /// <code>
        /// new DirFiles(@"Some\File\Location\*.*")
        /// {
        ///     OnProcess = file =>
        ///     {
        ///         file.RegisterCom(false, new[] { "-gg" }, true);
        ///     }
        /// };
        /// </code>
        /// </example>
        /// 
        /// </summary>
        /// 
        /// <param name="dirFiles">The files to register.</param>
        /// <param name="createComObjects">
        /// Whether to create the output as COM objects or plain registry entries (adds argument '-scom' if false).<br/>
        /// COM objects are easier to read but can lead to Candle errors because heat.exe sometimes leaves stuff empty.
        /// </param>
        /// <param name="heatArguments">Additional arguments that should be passed to heat.exe.</param>
        /// <param name="overrideDefaults">Omits the default arguments '-ag' and '-svb6' from the call to heat.exe if further customization is wanted.</param>
        /// <param name="hideWarnings">Whether to ignore the warnings returned by heat.exe and to stop forwarding them to the console.</param>
        public static DirFiles RegisterCom(this DirFiles dirFiles, bool createComObjects = false, string[] heatArguments = null, bool overrideDefaults = false, bool hideWarnings = false)
        {
            dirFiles.OnProcess = file =>
            {
                file.RegisterCom(createComObjects, heatArguments, overrideDefaults, hideWarnings);
            };

            return dirFiles;
        }
    }
}