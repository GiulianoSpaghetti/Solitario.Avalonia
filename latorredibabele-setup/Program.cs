using System;
using System.Runtime.Remoting.Lifetime;
using WixSharp;

namespace windatefrom_setup
{
    internal class Program
    {
        static void Main()
        {
            var project = new Project("Solitario.Avalonia",
                              new Dir(@"[ProgramFiles64Folder]\\Solitario.Avalonia",
                                  new DirFiles(@"*.*"),
                                  new Dir("runtimes",
                                      new Dir("win-x64",
                                            new Dir("native",
                                                new File("runtimes\\win-x64\\native\\av_libglesv2.dll"),
                                                new File("runtimes\\win-x64\\native\\libHarfBuzzSharp.dll"),
                                                new File("runtimes\\win-x64\\native\\libSkiaSharp.dll")
                                            )
                                        )
                                 )
                        ),
                        new Dir(@"%ProgramMenu%",
                         new ExeFileShortcut("La torre di babele", "[ProgramFiles64Folder]\\Solitario.Avalonia\\Solitario.Desktop.exe", "") { WorkingDirectory = "[INSTALLDIR]" }
                      )//,
                       //new Property("ALLUSERS","0")
            );

            project.GUID = new Guid("AC9457B4-8962-4BF7-B53C-FD625A325931");
            project.Version = new Version("3.0");
            project.Platform = Platform.x64;
            project.SourceBaseDir = "F:\\source\\Solitario.Avalonia\\Solitario.Desktop\\bin\\Release\\net8.0-windows10.0.22621.0";
            project.LicenceFile = "LICENSE.rtf";
            project.OutDir = "f:\\";
            project.ControlPanelInfo.Manufacturer = "Giulio Sorrentino";
            project.ControlPanelInfo.Name = "La torre di babele";
            project.ControlPanelInfo.HelpLink = "https://github.com/numerunix/WinDateFrom.Avalonia/issues";
            project.Description = "Un semplice solitario per spratichirsi con le carte che riesce sempre";
            //            project.Properties.SetValue("ALLUSERS", 0);
            project.BuildMsi();
        }
    }
}