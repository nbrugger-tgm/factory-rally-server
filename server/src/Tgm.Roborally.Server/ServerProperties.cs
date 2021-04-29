using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Tgm.Roborally.Server {
    public class ServerProperties {
        public static string mapRepo = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\Maps";
    }
}