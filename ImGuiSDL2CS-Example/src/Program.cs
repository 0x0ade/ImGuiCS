using ImGuiSDL2CS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ImGuiSDL2CS.Example {
    static class Program {

        static SDL2Window Instance;

        [STAThread]
        static void Main(string[] args) {
            Queue<string> argq = new Queue<string>(args);
            while (argq.Count > 0) {
                string arg = argq.Dequeue();
                if (arg == "--debug")
                    Debugger.Launch();
            }

            /*ImGuiSDL2CSWindow*/ Instance = new YourGameNamespace.YourGameWindow();
            Instance.Run();
            Instance.Dispose();
        }

    }
}
