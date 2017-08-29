using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace ImGuiNET {
    /// <summary>
    /// C# port of Flix01's imguifilesystem
    /// https://gist.github.com/Flix01/f34b5efa91e50a241c1b
    /// </summary>
    public partial class FileDialog {

        public const int MaxPathBytes = 259;
        public const int MaxFilenameBytes = 255;

        public string ChosenPath { get; private set; }
        public string LastDirectory { get; private set; }

        private bool Rescan = true;

        private Internal _internal = new Internal();

        public FileDialog(
            bool noKnownDirectoriesSection = false,
            bool noCreateDirectorySection = false,
            bool noFilteringSection = false,
            bool detectKnownDirectoriesAtEachOpening = false,
            bool addDisplayByOption = false,
            bool dontFilterSaveFilePathsEnteredByTheUser = false
        ) {
            _internal.resetVariables();

            _internal.detectKnownDirectoriesAtEveryOpening = detectKnownDirectoriesAtEachOpening;
            _internal.allowDisplayByOption = addDisplayByOption;
            _internal.forbidDirectoryCreation = noCreateDirectorySection;
            _internal.allowKnownDirectoriesSection = !noKnownDirectoriesSection;
            _internal.allowFiltering = !noFilteringSection;
            _internal.mustFilterSaveFilePathWithFileFilterExtensionString = !dontFilterSaveFilePathsEnteredByTheUser;
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

        int pNumberKnownUserDirectoriesExceptDrives = 0;
        List<string> pUserKnownDirectoryDisplayNames = new List<string>();
        List<string> pUserKnownDirectories = new List<string>();//& Directory.GetUserKnownDirectories(&pUserKnownDirectoryDisplayNames, &pNumberKnownUserDirectoriesExceptDrives);
        ImVec4[] ColorSet = new ImVec4[(int) Internal.Color.ImGuiCol_Dialog_Size];
        readonly static ImVec4 df = new ImVec4(0.9f, 0.9f, 0.3f, 1f);          // directory color factor
        readonly static ImVec4 ff = new ImVec4(0.7f, 0.7f, 0.7f, 1f);          // file color factor
        readonly static ImVec4 sf = new ImVec4(1.0f, 0.8f, 0.5f, 1f);          // delected folder color factor
        const int numTabs = (int) Sorting.SORT_ORDER_COUNT / 2;
        readonly static string[] names = { "Name", "Modified", "Size", "Type" };

        int id;

        byte[] tmpPathBytes = new byte[MaxPathBytes];

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
            //-----------------------------------------------------------------------------
            Internal I = _internal;
            string rv = I.chosenPath = "";
            //-----------------------------------------------------
            bool isSelectFolderDialog = I.isSelectFolderDialog = _isFolderChooserDialog;
            bool isSaveFileDialog = I.isSaveFileDialog = _isSaveFileDialog;

            bool allowDirectoryCreation = I.allowDirectoryCreation = I.forbidDirectoryCreation ? false : (isSelectFolderDialog || isSaveFileDialog);
            //----------------------------------------------------------
        
            //----------------------------------------------------------
            ImGuiStyle style = ImGui.GetStyle();
            ImVec4 dummyButtonColor = new ImVec4(0.0f,0.0f,0.0f,0.5f);     // Only the alpha is twickable from here
            // Fill ColorSet above and fix dummyButtonColor here
            {
                for (int i = 0, sz = (int) Internal.Color.ImGuiCol_Dialog_Directory_Text; i<=sz;i++)    {
                    ImVec4 r = style.GetColor(i < sz ? (ImGuiCol.Button + i) : ImGuiCol.Text);
                    Internal.ColorCombine(ref ColorSet[i], r,df);
                }
                for (int i = (int) Internal.Color.ImGuiCol_Dialog_File_Background, sz = (int) Internal.Color.ImGuiCol_Dialog_File_Text; i<=sz;i++)    {
                    ImVec4 r = style.GetColor(i < sz ? (ImGuiCol.Button - (int) Internal.Color.ImGuiCol_Dialog_File_Background + i) : ImGuiCol.Text);
                    Internal.ColorCombine(ref ColorSet[i], r,ff);
                }
                if (dummyButtonColor.w>0)   {
                    ImVec4 bbc = style.GetColor(ImGuiCol.Button);
                    dummyButtonColor.x = bbc.x;dummyButtonColor.y = bbc.y;dummyButtonColor.z = bbc.z;dummyButtonColor.w *= bbc.w;
                }
            }

            if (I.rescan)   {
                string validDirectory=".";
                if (directory != null && directory.Length>0) {
                    if (Directory.Exists(directory)) validDirectory = directory;
                    else {
                        validDirectory = Path.GetDirectoryName(directory);
                        if (!Directory.Exists(validDirectory)) validDirectory=".";
                    }
                }
                I.currentFolder = Path.GetFullPath(validDirectory);

                I.editLocationCheckButtonPressed = false;
                I.history.reset(); // reset history
                I.history.switchTo(I.currentFolder);    // init history
                I.dirs.Clear();I.files.Clear();I.dirNames.Clear();I.fileNames.Clear();I.currentSplitPath.Clear();
                I.newDirectoryName = "New Folder";
                if (_saveFileName != null) {
                    //&I.saveFileName[0] = _saveFileName;
                    I.saveFileName = Path.GetFileName(_saveFileName);    // Better!
                }
                else I.saveFileName="";
                isSelectFolderDialog = _isFolderChooserDialog;
                isSaveFileDialog = _isSaveFileDialog;
                allowDirectoryCreation =  I.forbidDirectoryCreation? false : (isSelectFolderDialog || isSaveFileDialog);
                if (isSelectFolderDialog && I.sortingMode>(int) Sorting.SORT_ORDER_LAST_MODIFICATION_INVERSE) I.sortingMode = 0;
                I.forceRescan = true;
                I.open = true;
                I.filter.Clear();
                if (windowTitle == null || windowTitle.Length==0)  {
                    if (isSelectFolderDialog)   I.wndTitle = "Please select a folder";
                    else if (isSaveFileDialog)  I.wndTitle = "Please choose/create a file for saving";
                    else                        I.wndTitle = "Please choose a file";
                }
                else                            I.wndTitle = windowTitle;
                I.wndTitle += "##";
                // char[] tmpWndTitleNumber = new char[12];
                // ImFormatString(tmpWndTitleNumber,11,"%d", I.uniqueNumber);
                string tmpWndTitleNumber = I.uniqueNumber.ToString();
                I.wndTitle += tmpWndTitleNumber;
                I.wndPos = windowPos ?? new ImVec2();
                I.wndSize = windowSize ?? new ImVec2();
                if (I.wndSize.x<=0) I.wndSize.x = 400;
                if (I.wndSize.y<=0) I.wndSize.y = 400;
                ImVec2 mousePos = ImGui.GetMousePos();// ImGui.GetCursorPos();
                if (I.wndPos.x<=0)  I.wndPos.x = mousePos.x - I.wndSize.x*0.5f;
                if (I.wndPos.y<=0)  I.wndPos.y = mousePos.y - I.wndSize.y*0.5f;
                ImVec2 screenSize = ImGui.GetIO().DisplaySize;
                if (I.wndPos.x>screenSize.x-I.wndSize.x) I.wndPos.x = screenSize.x-I.wndSize.x;
                if (I.wndPos.y>screenSize.y-I.wndSize.y) I.wndPos.y = screenSize.y-I.wndSize.y;
                if (I.wndPos.x< 0) I.wndPos.x = 0;
                if (I.wndPos.y< 0) I.wndPos.y = 0;
                //fprintf(stderr,"screenSize = %f,%f mousePos = %f,%f wndPos = %f,%f wndSize = %f,%f\n",screenSize.x,screenSize.y,mousePos.x,mousePos.y,wndPos.x,wndPos.y,wndSize.x,wndSize.y);
                if (I.detectKnownDirectoriesAtEveryOpening) pUserKnownDirectories = Directory_GetUserKnownDirectories(ref pUserKnownDirectoryDisplayNames,ref pNumberKnownUserDirectoriesExceptDrives,true);
            }
            if (!I.open) return rv;

            if (I.forceRescan)    {
                I.forceRescan = false;
                int sortingModeForDirectories = (I.sortingMode <= (int) Sorting.SORT_ORDER_LAST_MODIFICATION_INVERSE) ? I.sortingMode : (I.sortingMode % 2);
                Directory_GetDirectories(I.currentFolder,ref I.dirs,ref I.dirNames,(Sorting)sortingModeForDirectories);  // this is because directories don't return their size or their file extensions (so if needed we sort them alphabetically)
                //I.dirNames.resize(I.dirs.size());for (int i=0,sz=I.dirs.size();i<sz;i++)  Path.GetFileName(I.dirs[i],(char*)I.dirNames[i]);

                if (!isSelectFolderDialog)  {
                    if (fileFilterExtensionString == null || fileFilterExtensionString.Length==0) Directory_GetFiles(I.currentFolder,ref I.files,ref I.fileNames,(Sorting)I.sortingMode);
                    else                                        Directory_GetFiles(I.currentFolder,ref I.files,fileFilterExtensionString,ref I.fileNames,(Sorting)I.sortingMode);
                    //I.fileNames.resize(I.files.size());for (int i=0,sz=I.files.size();i<sz;i++) Path.GetFileName(I.files[i],(char*)I.fileNames[i]);
                }
                else {
                    I.files.Clear();I.fileNames.Clear();
                    I.saveFileName="";
                    string currentFolderName = Path.GetFileName(I.currentFolder);
                    int currentFolderNameSize = currentFolderName.Length;
                    if (currentFolderNameSize==0 || currentFolderName[currentFolderNameSize - 1]==':') currentFolderName += "/";
                    I.saveFileName += currentFolderName;
                }

                I.history.getCurrentSplitPath(ref I.currentSplitPath);

                const int approxNumEntriesPerColumn = 20;//(int) (20.f / browseSectionFontScale);// tweakable
                I.totalNumBrowsingEntries = (int)(I.dirs.Count+I.files.Count);
                I.numBrowsingColumns = I.totalNumBrowsingEntries/approxNumEntriesPerColumn;
                if (I.numBrowsingColumns<=0) I.numBrowsingColumns = 1;
                if (I.totalNumBrowsingEntries%approxNumEntriesPerColumn>(approxNumEntriesPerColumn/2)) ++I.numBrowsingColumns;
                if (I.numBrowsingColumns>6) I.numBrowsingColumns = 6;
                I.numBrowsingEntriesPerColumn = I.totalNumBrowsingEntries/I.numBrowsingColumns;
                if (I.totalNumBrowsingEntries%I.numBrowsingColumns!=0) ++I.numBrowsingEntriesPerColumn;

                //#       define DEBUG_HISTORY
#if DEBUG_HISTORY
                if (I.history.getInfoSize()>0) fprintf(stderr,"\nHISTORY: currentFolder:\"%s\" history.canGoBack=%s history.canGoForward=%s currentHistory:\n", I.currentFolder, I.history.canGoBack()?"true":"false",I.history.canGoForward()?"true":"false");
                if (I.history.getCurrentFolderInfo()) I.history.getCurrentFolderInfo()->display();
#endif //DEBUG_HISTORY
            }

            if (I.rescan) {
                I.rescan = false; // Mandatory

                ImGui.Begin(I.wndTitle, ref I.open, I.wndSize,windowAlpha);
                ImGui.SetWindowPos(I.wndPos);
                ImGui.SetWindowSize(I.wndSize);
                //fprintf(stderr,"\"%s\" wndPos={%1.2f,%1.2f}\n",wndTitle.c_str(),wndPos.x,wndPos.y);
            }
            else ImGui.Begin(I.wndTitle, ref I.open,new ImVec2(0,0),windowAlpha);
            ImGui.Separator();

            //------------------------------------------------------------------------------------
            // History (=buttons: < and >)
            {
                bool historyBackClicked = false;
                bool historyForwardClicked = false;

                // history -----------------------------------------------
                ImGui.PushID("historyDirectoriesID");

                bool historyCanGoBack = I.history.canGoBack();
                bool historyCanGoForward = I.history.canGoForward();

                if (!historyCanGoBack) {
                    ImGui.PushStyleColor(ImGuiCol.Button,dummyButtonColor);
                    ImGui.PushStyleColor(ImGuiCol.ButtonHovered,dummyButtonColor);
                    ImGui.PushStyleColor(ImGuiCol.ButtonActive,dummyButtonColor);
                }
                historyBackClicked = ImGui.Button("<")&historyCanGoBack;
                ImGui.SameLine();
                if (!historyCanGoBack) {
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                }

                if (!historyCanGoForward) {
                    ImGui.PushStyleColor(ImGuiCol.Button,dummyButtonColor);
                    ImGui.PushStyleColor(ImGuiCol.ButtonHovered,dummyButtonColor);
                    ImGui.PushStyleColor(ImGuiCol.ButtonActive,dummyButtonColor);
                }
                historyForwardClicked = ImGui.Button(">")&historyCanGoForward;
                ImGui.SameLine();
                if (!historyCanGoForward) {
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                }

                ImGui.PopID();
                // -------------------------------------------------------

                if (historyBackClicked || historyForwardClicked)    {
                    ImGui.End();

                    if (historyBackClicked)         I.history.goBack();
                    else if (historyForwardClicked) I.history.goForward();

                    I.forceRescan = true;

                    I.currentFolder = I.history.getCurrentFolder();
                    I.editLocationInputText = I.currentFolder;

#if DEBUG_HISTORY
                    if (historyBackClicked) fprintf(stderr,"\nPressed BACK to\t");
                    else fprintf(stderr,"\nPressed FORWARD to\t");
                    fprintf(stderr,"\"%s\" (%d)\n", I.currentFolder,(int)* I.history.getCurrentSplitPathIndex());
#undef DEBUG_HISTOTY
#endif //DEBUG_HISTORY
                    return rv;
                }
            }
            //------------------------------------------------------------------------------------
            // Edit Location CheckButton
            bool editLocationInputTextReturnPressed = false;
            {

                bool mustValidateInputPath = false;
                ImGui.PushStyleColor(ImGuiCol.Button,I.editLocationCheckButtonPressed? dummyButtonColor : style.GetColor(ImGuiCol.Button));

                if (ImGui.Button("L##EditLocationCheckButton"))    {
                    I.editLocationCheckButtonPressed = !I.editLocationCheckButtonPressed;
                    if (I.editLocationCheckButtonPressed) {
                        I.editLocationInputText = I.currentFolder;
                        ImGui.SetKeyboardFocusHere();
                    }
                    //if (!I.editLocationCheckButtonPressed) mustValidateInputPath = true;   // or not ? I mean: the user wants to quit or to validate in this case ?
                }

                ImGui.PopStyleColor();

                if (I.editLocationCheckButtonPressed) {
                    ImGui.SameLine();
                    Encoding.UTF8.GetBytes(I.editLocationInputText, 0, I.editLocationInputText.Length, tmpPathBytes, 0);
                    editLocationInputTextReturnPressed = ImGui.InputText("##EditLocationInputText", tmpPathBytes, MaxPathBytes,ImGuiInputTextFlags.AutoSelectAll|ImGuiInputTextFlags.EnterReturnsTrue);
                    I.editLocationInputText = Encoding.UTF8.GetString(tmpPathBytes);
                    if (editLocationInputTextReturnPressed)  mustValidateInputPath = true;
                    else ImGui.Separator();
                }

                if (mustValidateInputPath)  {
                    // it's better to clean the input path here from trailing slashes:
                    StringBuilder cleanEnteredPathB = new StringBuilder(I.editLocationInputText);
                    int len = cleanEnteredPathB.Length;
                    while (len>0 && (cleanEnteredPathB[len - 1]=='/' || cleanEnteredPathB[len - 1]=='\\')) { cleanEnteredPathB.Remove(len - 1, 1);len = cleanEnteredPathB.Length;}
                    string cleanEnteredPath = cleanEnteredPathB.ToString();

                    if (len==0 || I.currentFolder == cleanEnteredPath)    I.editLocationCheckButtonPressed = false;
                    else if (Directory.Exists(cleanEnteredPath))   {
                        I.editLocationCheckButtonPressed = false; // Optional (return to split-path buttons)
                        //----------------------------------------------------------------------------------
                        I.history.switchTo(cleanEnteredPath);
                        I.currentFolder = cleanEnteredPath;
                        I.forceRescan = true;

                    }
                    //else fprintf(stderr,"mustValidateInputPath NOOP: \"%s\" \"%s\"\n",I.currentFolder,cleanEnteredPath);
                }
                else ImGui.SameLine();

            }
            //------------------------------------------------------------------------------------
            // Split Path control
            if (!I.editLocationCheckButtonPressed && !editLocationInputTextReturnPressed) {
                bool mustSwitchSplitPath = false;
                FolderInfo fi = I.history.getCurrentFolderInfo();

                ImVec2 framePadding = ImGui.GetStyle().FramePadding;
                float originalFramePaddingX = framePadding.x;
                framePadding.x = 0;

                // Split Path
                // Tab:
                {
                    //-----------------------------------------------------
                    // TAB LABELS
                    //-----------------------------------------------------
                    {
                        int numTabs = (int) I.currentSplitPath.Count;
                        int newSelectedTab = fi.splitPathIndex;
                        for (int t = 0; t<numTabs;t++)	{
                            if (t>0) ImGui.SameLine(0,0);
                            if (t==fi.splitPathIndex) {
                                ImGui.PushStyleColor(ImGuiCol.Button,dummyButtonColor);
                                ImGui.PushStyleColor(ImGuiCol.ButtonHovered,dummyButtonColor);
                                ImGui.PushStyleColor(ImGuiCol.ButtonActive,dummyButtonColor);
                            }
                            ImGui.PushID(I.currentSplitPath[t]);
                            bool pressed = ImGui.Button(I.currentSplitPath[t]);
                            ImGui.PopID();
                            if (pressed) {
                                if (fi.splitPathIndex!=t && !mustSwitchSplitPath) mustSwitchSplitPath = true;
                                newSelectedTab = t;
                            }
                            if (t==fi.splitPathIndex) {
                                ImGui.PopStyleColor();
                                ImGui.PopStyleColor();
                                ImGui.PopStyleColor();
                            }
                        }
                        if (mustSwitchSplitPath) {
                            FolderInfo mfi = null;
                            fi.getFolderInfoForSplitPathIndex(newSelectedTab,ref mfi);
                            I.history.switchTo(mfi);
                            I.forceRescan = true;
                            I.currentFolder = I.history.getCurrentFolder();
                            I.editLocationInputText = I.currentFolder;
                            //fprintf(stderr,"%s\n",I.currentFolder);
                        }
                    }
                }

                framePadding.x = originalFramePaddingX;
            }
            //------------------------------------------------------------------------------------

            // Start collapsable regions----------------------------------------------------------
            // User Known directories-------------------------------------------------------------
            if (I.allowKnownDirectoriesSection && pUserKnownDirectories.Count>0)  {
                ImGui.Separator();

                if (ImGui.CollapsingHeader("Known Directories##UserKnownDirectories"))  {
                    ImGui.PushID(id);

                    ImGui.PushStyleColor(ImGuiCol.Text,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Text]);
                    ImGui.PushStyleColor(ImGuiCol.Button,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Background]);
                    ImGui.PushStyleColor(ImGuiCol.ButtonHovered,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Hover]);
                    ImGui.PushStyleColor(ImGuiCol.ButtonActive,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Pressed]);

                    for (int i = 0, sz = (int) pUserKnownDirectories.Count; i<sz;i++)  {
                        string userKnownFolder = pUserKnownDirectories[i];
                        string userKnownFolderDisplayName = pUserKnownDirectoryDisplayNames[i];
                        if (ImGui.SmallButton(userKnownFolderDisplayName) && userKnownFolder != I.currentFolder) {
                            I.currentFolder = userKnownFolder;
                            I.editLocationInputText = I.currentFolder;
                            I.history.switchTo(I.currentFolder);
                            I.forceRescan = true;
                            //------------------------------------------------------------------------------------------------------------------------------
                        }
                        if (i!=sz-1 && (i>=pNumberKnownUserDirectoriesExceptDrives || i%7!=6)) ImGui.SameLine();
                    }

                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();

                    ImGui.PopID();
                }

            }
            // End User Known directories---------------------------------------------------------
            // Allow directory creation ----------------------------------------------------------
            if (allowDirectoryCreation) {
                ImGui.Separator();
                bool mustCreate = false;

                if (ImGui.CollapsingHeader("New Directory##allowDirectoryCreation"))  {
                    ImGui.PushID(id);

                    Encoding.UTF8.GetBytes(I.newDirectoryName, 0, I.newDirectoryName.Length, tmpPathBytes, 0);
                    ImGui.InputText("##createNewFolderName",tmpPathBytes,MaxFilenameBytes);
                    I.newDirectoryName = Encoding.UTF8.GetString(tmpPathBytes);
                    ImGui.SameLine();
                    mustCreate = ImGui.Button("CREATE");

                    ImGui.PopID();
                }

                if (mustCreate && I.newDirectoryName.Length>0)    {
                    string newDirPath = Path.Combine(I.currentFolder,I.newDirectoryName);
                    if (!Directory.Exists(newDirPath)) {
                        //#           define SIMULATING_ONLY
#if SIMULATING_ONLY
                        fprintf(stderr,"creating: \"%s\"\n", newDirPath);
#undef SIMULATING_ONLY
#else //SIMULATING_ONLY
                        Directory.CreateDirectory(newDirPath);
                        if (!Directory.Exists(newDirPath)) Console.Error.WriteLine("Error creating new folder: \"{0}\"\n", newDirPath);
                        else I.forceRescan = true;   // Just update
#endif //SIMULATING_ONLY
                    }
                }
            }
            // End allow directory creation ------------------------------------------------------
            // Filtering entries -----------------------------------------------------------------
            if (I.allowFiltering)  {
                ImGui.Separator();
                if (ImGui.CollapsingHeader("Filtering##fileNameFiltering"))  {
                    ImGui.PushID(id);
                    I.filter.Draw();
                    ImGui.PopID();
                }

            }
            // End filtering entries -------------------------------------------------------------
            // End collapsable regions------------------------------------------------------------

            // Selection field -------------------------------------------------------------------
            if (isSaveFileDialog || isSelectFolderDialog)   {
                ImGui.Separator();
                bool selectionButtonPressed = false;

                ImGui.PushID(id);
                if (isSaveFileDialog)   {
                    ImGui.AlignFirstTextHeightToWidgets();
                    ImGui.Text("File:");ImGui.SameLine();
                    Encoding.UTF8.GetBytes(I.saveFileName, 0, I.saveFileName.Length, tmpPathBytes, 0);
                    ImGui.InputText("##saveFileName", tmpPathBytes, MaxFilenameBytes);
                    I.saveFileName = Encoding.UTF8.GetString(tmpPathBytes);
                    ImGui.SameLine();
                }
                else {
                    ImGui.AlignFirstTextHeightToWidgets();
                    ImGui.Text("Folder:");ImGui.SameLine();

                    ImVec4 r = style.GetColor(ImGuiCol.Text);
                    Internal.ColorCombine(ref ColorSet[(int) Internal.Color.ImGuiCol_Dialog_SelectedFolder_Text], r,sf);

                    ImGui.TextColored(ColorSet[(int) Internal.Color.ImGuiCol_Dialog_SelectedFolder_Text],I.saveFileName);
                    ImGui.SameLine();
                }

                if (isSelectFolderDialog)  selectionButtonPressed = ImGui.Button("Select");
                else selectionButtonPressed = ImGui.Button("Save");

                ImGui.PopID();

                if (selectionButtonPressed) {
                    if (isSelectFolderDialog) {
                        rv = I.currentFolder;
                        I.open = true;
                    }
                    else if (isSaveFileDialog)  {
                        if (I.saveFileName.Length>0)  {
                            bool pathOk = true;
                            if (I.mustFilterSaveFilePathWithFileFilterExtensionString && fileFilterExtensionString != null && fileFilterExtensionString.Length>0)    {
                                pathOk = false;
                                string saveFileNameExtension = Path.GetExtension(I.saveFileName);
                                bool saveFileNameHasExtension = saveFileNameExtension.Length > 0;
                                //-------------------------------------------------------------------
                                string[] wExts = fileFilterExtensionString.Split(';');
                                int wExtsSize = wExts.Length;
                                if (!saveFileNameHasExtension)   {
                                    if (wExtsSize==0) pathOk = true;    // Bad situation, better allow this case
                                    else I.saveFileName += wExts[0];
                                }
                                else    {
                                    // saveFileNameHasExtension
                                    for (int i = 0; i<wExtsSize;i++)	{
                                        string ext = wExts[i];
                                        if (ext == saveFileNameExtension)   {
                                            pathOk = true;
                                            break;
                                        }
                                    }
                                    if (!pathOk && wExtsSize>0) I.saveFileName += wExts[0];
                                }
                            }
                            if (pathOk) {
                                string savePath = Path.Combine(I.currentFolder,I.saveFileName);
                                rv = savePath;
                                I.open = true;
                            }
                        }
                    }
                }

                //ImGui.Spacing();
            }
            // End selection field----------------------------------------------------------------

            ImGui.Separator();
            // sorting --------------------------------------------------------------------
            ImGui.Text("Sorting by: ");ImGui.SameLine();
            {
                int oldSortingMode = I.sortingMode;
                int oldSelectedTab = I.sortingMode / 2;
                //-----------------------------------------------------
                // TAB LABELS
                //-----------------------------------------------------
                {
                    int newSortingMode = oldSortingMode;
                    int numUsedTabs = isSelectFolderDialog ? 2 : numTabs;
                    for (int t = 0; t<numUsedTabs;t++)	{
                        if (t>0) ImGui.SameLine();
                        if (t==oldSelectedTab) {
                            ImGui.PushStyleColor(ImGuiCol.Button,dummyButtonColor);
                        }
                        ImGui.PushID(names[t]);
                        bool pressed = ImGui.SmallButton(names[t]);
                        ImGui.PopID();
                        if (pressed) {
                            if (oldSelectedTab==t) {
                                newSortingMode = oldSortingMode;
                                if (newSortingMode%2==0) ++newSortingMode;// 0,2,4
                                else --newSortingMode;
                            }
                            else newSortingMode = t*2;
                        }
                        if (t==oldSelectedTab) {
                            ImGui.PopStyleColor();
                        }
                    }

                    if (newSortingMode!=oldSortingMode) {
                        I.sortingMode = newSortingMode;
                        //printf("sortingMode = %d\n",sortingMode);
                        I.forceRescan = true;
                    }

                    //-- Browsing per row -----------------------------------
                    if (I.allowDisplayByOption && I.numBrowsingColumns>1)   {
                        ImGui.SameLine();
                        ImGui.Text("   Display by:");
                        ImGui.SameLine();
                        ImGui.PushStyleColor(ImGuiCol.Button,dummyButtonColor);
                        if (ImGui.SmallButton(!Internal.BrowsingPerRow? "Column##browsingPerRow" : "Row##browsingPerRow"))   {
                            Internal.BrowsingPerRow = !Internal.BrowsingPerRow;
                        }
                        ImGui.PopStyleColor();
                    }
                    //-- End browsing per row -------------------------------
                }
            }
            //-----------------------------------------------------------------------------
            ImGui.Separator();

            //-----------------------------------------------------------------------------
            // MAIN BROWSING FRAME:
            //-----------------------------------------------------------------------------
            {
                ImGui.BeginChild("BrowsingFrame");
                // ImGui.SetScrollPosHere();   // possible future ref: while drawing to place the scroll bar
                ImGui.Columns(I.numBrowsingColumns);

                ImGui.PushID(id);
                int cntEntries = 0;
                // Directories --------------------------------------------------------------
                if (I.dirs.Count>0)   {
                    ImGui.PushStyleColor(ImGuiCol.Text,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Text]);
                    ImGui.PushStyleColor(ImGuiCol.Button,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Background]);
                    ImGui.PushStyleColor(ImGuiCol.ButtonHovered,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Hover]);
                    ImGui.PushStyleColor(ImGuiCol.ButtonActive,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_Directory_Pressed]);

                    for (int i = 0, sz = (int) I.dirs.Count; i<sz;i++) {
                        string dirName = I.dirNames[i];
                        if (I.filter.PassFilter(dirName)) {
                            if (ImGui.SmallButton(dirName)) {
                                I.currentFolder = I.dirs[i];
                                I.editLocationInputText = I.currentFolder;
                                I.history.switchTo(I.currentFolder);
                                I.forceRescan = true;
                                //------------------------------------------------------------------------------------------------------------------------------
                            }
                            ++cntEntries;
                            if (Internal.BrowsingPerRow) ImGui.NextColumn();
                            else if (cntEntries==I.numBrowsingEntriesPerColumn) {
                                cntEntries = 0;
                                ImGui.NextColumn();
                            }
                        }
                    }

                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                }
                // Files ----------------------------------------------------------------------
                if (!isSelectFolderDialog && I.files.Count>0)   {
                    ImGui.PushStyleColor(ImGuiCol.Text,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_File_Text]);
                    ImGui.PushStyleColor(ImGuiCol.Button,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_File_Background]);
                    ImGui.PushStyleColor(ImGuiCol.ButtonHovered,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_File_Hover]);
                    ImGui.PushStyleColor(ImGuiCol.ButtonActive,ColorSet[(int) Internal.Color.ImGuiCol_Dialog_File_Pressed]);


                    for (int i = 0, sz = (int) I.files.Count; i<sz;i++) {
                        string fileName = I.fileNames[i];
                        if (I.filter.PassFilter(fileName)) {
                            if (ImGui.SmallButton(fileName)) {
                                if (!isSaveFileDialog)  {
                                    rv = I.files[i];
                                    I.open = true;
                                }
                                else {
                                    I.saveFileName = Path.GetFileName(I.files[i]);
                                }
                            }
                            ++cntEntries;
                            if (Internal.BrowsingPerRow) ImGui.NextColumn();
                            else if (cntEntries==I.numBrowsingEntriesPerColumn) {
                                cntEntries = 0;
                                ImGui.NextColumn();
                            }
                        }
                    }

                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                    ImGui.PopStyleColor();
                }
                //-----------------------------------------------------------------------------
                ImGui.PopID();
                ImGui.EndChild();

            }
            //-----------------------------------------------------------------------------

            ImGui.End();
            return rv;
        }

    }
}
