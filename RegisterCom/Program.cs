using System;
using WixSharp;
using WixSharp.CommonTasks;

class Program
{
    static void Main()
    {
        var project =   new Project("MyProduct",
                            new Dir(@"%ProgramFiles%\My Company\My Product",
                                // This shows how to register COM files like .dll or .ocx without the need to depend
                                // on external programs like regsvr32.exe or the use of the frowned upon self registration.
                                //
                                // It utilizes heat.exe from the WiX toolkit to extract the registration data and
                                // adds it to the installers .wxs file, therefore eliminating the need to go through
                                // each file manually to add the entries to the generated .wxs file.
                                //
                                //
                                // CreateComObjects - Used to create better readable entries with TypeLib and ProgId
                                // entries but is more prone to cause errors, therefore disabled by default.
                                // You can compare the two files in the .wxs to have a look for yourself.
                                //
                                // HeatArguments - If you want to customize the call to heat.exe.
                                // You can find the available arguments at:
                                // https://wixtoolset.org/documentation/manual/v3/overview/heat.html
                                //
                                // OverrideDefaults - Omits the default arguments for further customization.
                                // By default heat.exe is called with '-ag' and '-svb6'
                                //
                                // HideWarnings - If you want to hide the warnings received from heat.exe.
                                // You can also pass the argument '-sw<N>' to suppress all or specific warnings.


                                // Register a single File.
                                // Either use the extension,
                                new File(@"Files\CSScriptLibrary.dll").RegisterCom(),
                                // or create a new WixEntity.
                                new File(@"Files\CSScriptLibrary2.dll",
                                    new RegisterCom()
                                    {
                                        CreateComObjects = true,
                                        HeatArguments = new[] { "-gg" },
                                        OverrideDefaults = true,
                                        HideWarnings = true
                                    }
                                )/*,

                                
                                // You can also register multiple files using either Files() or DirFiles().
                                // Again using the extension which does the work for you,
                                new Files(@"Files\*.*").RegisterCom(true),
                                // or by adding it to each file manually.
                                new Files(@"Files\*.*")
                                {
                                    // OnProcess is called for each file when it is actually created
                                    // and can be used to make changes to the individual files.
                                    OnProcess = file =>
                                    {
                                        // Here you can either use the extension or add a WixEntity again.
                                        file.Add(
                                            new RegisterCom()
                                            {
                                                HideWarnings = true
                                            }
                                        );
                                    }
                                },
                                // This is literally what the extension does for Files() and DirFiles().
                                new DirFiles(@"Files\*.*")
                                {
                                    OnProcess = file =>
                                    {
                                        file.RegisterCom();
                                    }
                                }
                                */
                            )
                        );

        project.GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");
        project.PreserveTempFiles = true;

        project.BuildMsi();
    }
}