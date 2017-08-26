using ImGuiNET;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImGuiSDL2CS {
    /// <summary>
    /// C# port of Flix01's imguifilesystem
    /// https://gist.github.com/Flix01/f34b5efa91e50a241c1b
    /// </summary>
    public class FileDialog {

        public string ChosenPath { get; private set; }
        public string LastDirectory { get; private set; }

        private bool Rescan = true;

        public FileDialog(
            bool noKnownDirectoriesSection = false,
            bool noCreateDirectorySection = false,
            bool noFilteringSection = false,
            bool detectKnownDirectoriesAtEachOpening = false,
            bool addDisplayByOption = false,
            bool dontFilterSaveFilePathsEnteredByTheUser = false
        ) {

        }

        public string ChooseFileDialog(
            bool dialogTriggerButton,
            string directory = null,
            string fileFilterExtensionString = null,
            string windowTitle = null,
            ImVec2? windowSize = null,
            ImVec2? windowPos = null,
            float windowAlpha = 0.875f
        ) {
            if (dialogTriggerButton) {
                Rescan = true;
                ChosenPath = "";
            }
            if (dialogTriggerButton || (!Rescan && string.IsNullOrEmpty(ChosenPath))) {
                ChooseFileMainMethod(directory, false, false, "", fileFilterExtensionString, windowTitle, windowSize, windowPos, windowAlpha);
            }
            return ChosenPath;
        }

        public string ChooseFolderDialog(
            bool dialogTriggerButton,
            string directory = null,
            string windowTitle = null,
            ImVec2? windowSize = null,
            ImVec2? windowPos = null,
            float windowAlpha = 0.875f
        ) {
            if (dialogTriggerButton) {
                Rescan = true;
                ChosenPath = "";
            }
            if (dialogTriggerButton || (!Rescan && string.IsNullOrEmpty(ChosenPath))) {
                ChooseFileMainMethod(directory, true, false, "", "", windowTitle, windowSize, windowPos, windowAlpha);
            }
            return ChosenPath;
        }

        public string SaveFileDialog(
            bool dialogTriggerButton,
            string directory = null,
            string startingFileNameEntry = null,
            string fileFilterExtensionString = null,
            string windowTitle = null,
            ImVec2? windowSize = null,
            ImVec2? windowPos = null,
            float windowAlpha = 0.875f
        ) {
            if (dialogTriggerButton) {
                Rescan = true;
                ChosenPath = "";
            }
            if (dialogTriggerButton || (!Rescan && string.IsNullOrEmpty(ChosenPath))) {
                ChooseFileMainMethod(directory, false, true, "", "", windowTitle, windowSize, windowPos, windowAlpha);
            }
            return ChosenPath;
        }

        enum Color {
            ImGuiCol_Dialog_Directory_Background,
            ImGuiCol_Dialog_Directory_Hover,
            ImGuiCol_Dialog_Directory_Pressed,
            ImGuiCol_Dialog_Directory_Text,

            ImGuiCol_Dialog_File_Background,
            ImGuiCol_Dialog_File_Hover,
            ImGuiCol_Dialog_File_Pressed,
            ImGuiCol_Dialog_File_Text,

            ImGuiCol_Dialog_SelectedFolder_Text,
            ImGuiCol_Dialog_Size
        };

        static void ColorCombine(ref ImVec4 c, ImVec4 r, ImVec4 factor)   {
            const float onethird = 1f / 3f;
            float rr = (r.X + r.Y + r.Z) * onethird;
            c.X = rr * factor.X;
            c.Y = rr * factor.Y;
            c.Z = rr * factor.Z;
            c.W = r.W;
        }

        /*
        static size_t ImFormatString(char* buf, size_t buf_size, const char* fmt, ...)
{
    va_list args;
    va_start(args, fmt);
        int w = vsnprintf(buf, buf_size, fmt, args);
    va_end(args);
        buf[buf_size - 1] = 0;
    return (w == -1) ? buf_size : (size_t)w;
}
*/

    private string ChooseFileMainMethod(
            string directory,
            bool _isFolderChooserDialog,
            bool _isSaveFileDialog,
            string _saveFileName,
            string fileFilterExtensionString,
            string windowTitle,
            ImVec2? windowSize,
            ImVec2? windowPos,
            float windowAlpha
        ) {
            string rv = "";

            return rv;
        }

    }
}
