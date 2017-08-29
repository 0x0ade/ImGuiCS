using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImGuiNET {
    public partial class FileDialog {

		static void Path_Split(string path, ref List<string> split) {
            split = new List<string>();
            string parent;
			while (!string.IsNullOrEmpty(path)) {
                parent = Path.GetDirectoryName(path);
				if (string.IsNullOrEmpty(parent))
					split.Insert(0, path);
				else if (string.IsNullOrEmpty(Path.GetDirectoryName(parent)))
                    split.Insert(0, path.Substring(parent.Length));
                else
                    split.Insert(0, path.Substring(parent.Length + 1));
                path = parent;
            }
        }

        readonly static Environment.SpecialFolder[] csid = {
            Environment.SpecialFolder.Desktop,
            Environment.SpecialFolder.Personal,
            Environment.SpecialFolder.Favorites,
            Environment.SpecialFolder.MyMusic,
            Environment.SpecialFolder.MyPictures,
            Environment.SpecialFolder.Recent
        };
        readonly static string[] name = {
            "Desktop",
            "Documents",
            "Favorites",
            "Music",
            "Pictures",
            "Recent"
        };
        static List<string> Directory_GetUserKnownDirectories(ref List<string> pUserKnownDirectoryDisplayNames, ref int pNumberKnownUserDirectoriesExceptDrives, bool forceUpdate) {
            List<string> rv = new List<string>();
            pUserKnownDirectoryDisplayNames = new List<string>();

            for (int i = 0; i < csid.Length; i++) {
                string path = Environment.GetFolderPath(csid[i]);
                if (string.IsNullOrEmpty(path))
                    continue;
                rv.Add(path);
                pUserKnownDirectoryDisplayNames.Add(name[i]);
            }

            pNumberKnownUserDirectoriesExceptDrives = rv.Count;
            return rv;
        }

        static void _Sort(List<string> list, Sorting sorting = Sorting.SORT_ORDER_ALPHABETIC) {
            switch (sorting) {
                case Sorting.SORT_ORDER_ALPHABETIC:
                    list.Sort((x, y) => string.Compare(x, y));
                    break;
                case Sorting.SORT_ORDER_ALPHABETIC_INVERSE:
                    list.Sort((x, y) => -string.Compare(x, y));
                    break;
                default:
                    // FIXME: Implement all possible sortings into _Sort
                    break;
            }
        }

        static void Directory_GetDirectories(string directoryName, ref List<string> result, ref List<string> pOptionalNamesOut, Sorting sorting = Sorting.SORT_ORDER_ALPHABETIC) {
            result = new List<string>(Directory.GetDirectories(directoryName));
            _Sort(result, sorting);

            pOptionalNamesOut = new List<string>();
            foreach (string dir in result)
                pOptionalNamesOut.Add(Path.GetFileName(dir));
        }

        static void Directory_GetFiles(string directoryName, ref List<string> result,ref List<string> pOptionalNamesOut, Sorting sorting= Sorting.SORT_ORDER_ALPHABETIC) {
            result = new List<string>(Directory.GetFiles(directoryName));
            _Sort(result, sorting);

            pOptionalNamesOut = new List<string>();
            foreach (string dir in result)
                pOptionalNamesOut.Add(Path.GetFileName(dir));
        }

        static void Directory_GetFiles(string directoryName, ref List<string> result, string wantedExtensions, ref List<string> pOptionalNamesOut, Sorting sorting = Sorting.SORT_ORDER_ALPHABETIC) {
            result = new List<string>(Directory.GetFiles(directoryName));

            string[] wExts = wantedExtensions.ToLowerInvariant().Split(';');
            for (int i = result.Count; i > -1; --i)
                if (Array.IndexOf(wExts, Path.GetExtension(result[i])) < 0)
                    result.RemoveAt(i);

            _Sort(result, sorting);

            pOptionalNamesOut = new List<string>();
            foreach (string dir in result)
                pOptionalNamesOut.Add(Path.GetFileName(dir));
        }

        class FolderInfo {
			public string fullFolder;
            public string currentFolder;
            public int splitPathIndex;
			static List<string> SplitPath = new List<string>(); // tmp field used internally

            public FolderInfo() {
                reset();
            }
            public FolderInfo(FolderInfo o) {
                set(o);
            }

            public void display() {
				Console.WriteLine("fullFolder=\"{0}\" currentFolder=\"{1}\" splitPathIndex={2}\n", fullFolder, currentFolder, splitPathIndex);
			}
            public void getSplitPath(ref List<string> splitPath) {
                Path_Split(fullFolder, ref splitPath);
			}
            public void set(FolderInfo o) {
                currentFolder = o.currentFolder;
				fullFolder = o.fullFolder;
				splitPathIndex = o.splitPathIndex;
			}
            public void reset() {
				currentFolder = "";
                fullFolder = "";
                splitPathIndex = -1;
			}

            public void fromCurrentFolder(string path)   {
				if (path == null || path.Length==0) reset();
				else {
					currentFolder = path;
					fullFolder = path;
					Path_Split(fullFolder,ref SplitPath);
					splitPathIndex = (int) SplitPath.Count-1;
				}
			}

            public bool isEqual(FolderInfo fi) {
				return fullFolder == fi.fullFolder && currentFolder == fi.currentFolder;
			}
            public bool isEqual(string path) {
				return fullFolder == path && currentFolder == path;
			}
            public int getSplitPathIndexFor(string path) {
				if (path != null || string.Compare(path,0,fullFolder,0,path.Length)!=0) return -1;

				Path_Split(fullFolder,ref SplitPath);
				string tmp="";
				for (int i=0,sz=(int)SplitPath.Count;i<sz;i++) {
                    tmp = Path.Combine(tmp, SplitPath[i]);
					//fprintf(stderr,"%d) \"%s\" <-> \"%s\"\n",i,tmp,path);
					if (tmp == path) return i;
				}
				return -1;
			}
            public bool getFolderInfoForSplitPathIndex(int _splitPathIndex,ref FolderInfo rv) {
				Path_Split(fullFolder,ref SplitPath);
				int splitPathSize = (int)SplitPath.Count;
				if (_splitPathIndex<0 || _splitPathIndex>=splitPathSize) return false;
				rv = this;
				rv.splitPathIndex = _splitPathIndex;

				rv.currentFolder="";
				if (_splitPathIndex>=0 && _splitPathIndex<splitPathSize) {
					for (int i=0;i<=_splitPathIndex;i++)    {
                        rv.currentFolder = Path.Combine(rv.currentFolder, SplitPath[i]);
						//fprintf(stderr,"%d) \"%s\" (\"%s\")\n",i,rv.currentFolder,SplitPath[i]);
					}
				}
				/*fprintf(stderr,"getFolderInfoForSplitPathIndex(%d):\nSource:    ",_splitPathIndex);
				this->display();
				fprintf(stderr,"Result:   ");
				rv.display();*/
				return true;
			}
		}

		class History {
			protected List<FolderInfo> info = new List<FolderInfo>();
            protected int currentInfoIndex;  // into info
            public bool canGoBack()    {
				return currentInfoIndex>0;
			}
            public bool canGoForward()   {
				return currentInfoIndex>=0 && currentInfoIndex<(int)info.Count-1;
			}
            public void reset() {
                info.Clear();
                currentInfoIndex =-1;
            }
            public History() {
                reset();
            }

            // -------------------------------------------------------------------------------------------------
            public void goBack()   {
				if (canGoBack())
                    --currentInfoIndex;
			}
            public void goForward()    {
				if (canGoForward())
                    ++currentInfoIndex;
			}
            public bool switchTo(string currentFolder)    {
				if (currentFolder == null || currentFolder.Length==0) return false;
				if (currentInfoIndex<0) {
					++currentInfoIndex;
					FolderInfo fi = new FolderInfo();
                    info.Add(fi);
					fi.fromCurrentFolder(currentFolder);
					return true;
				}
				else {
					FolderInfo lastInfo = info[currentInfoIndex];
					if (lastInfo.isEqual(currentFolder)) return false;
					int splitPathIndexInsideLastInfo = lastInfo.getSplitPathIndexFor(currentFolder);
					++currentInfoIndex;
                    FolderInfo fi = new FolderInfo();
                    info.Add(fi);
					if (splitPathIndexInsideLastInfo==-1)   fi.fromCurrentFolder(currentFolder);
					else {
						fi = lastInfo;
						fi.splitPathIndex = splitPathIndexInsideLastInfo;
						fi.currentFolder = currentFolder;
					}
					return true;
				}
			}
            public bool switchTo(FolderInfo fi) {
				if (fi.currentFolder == null|| fi.currentFolder.Length==0) return false;
				if (currentInfoIndex>=0) {
					FolderInfo lastInfo = info[currentInfoIndex];
					if (lastInfo.isEqual(fi)) return false;
				}
				++currentInfoIndex;
                info.Add(fi);
				return true;
			}
			//-----------------------------------------------------------------------------------------------------

			public bool isValid() {return (currentInfoIndex>=0 && currentInfoIndex<(int)info.Count);}
            public FolderInfo getCurrentFolderInfo() {return isValid() ? info[currentInfoIndex] : null;}
            public string getCurrentFolder() {return isValid() ? info[currentInfoIndex].currentFolder : null;}
            public bool getCurrentSplitPath(ref List<string> rv) {
				if (isValid()) {
					info[currentInfoIndex].getSplitPath(ref rv);
					return true;
				}
				else return false;
			}
            public int getCurrentSplitPathIndex() {return isValid() ? info[currentInfoIndex].splitPathIndex : 0;}
            public int getInfoSize() {return info.Count;}
		}

		class Internal {
            public List<string> dirs = new List<string>();
            public List<string> files = new List<string>();
            public List<string> dirNames = new List<string>();
            public List<string> fileNames = new List<string>();
            public List<string> currentSplitPath = new List<string>();
            public string currentFolder;
            public bool forceRescan;
            public bool open;
            public ImVec2 wndPos;
            public ImVec2 wndSize;
            public string wndTitle;
            public int sortingMode;

            public History history = new History();
            //-----------------------------------------------------
            public bool isSelectFolderDialog;
            public bool isSaveFileDialog;

            public bool allowDirectoryCreation,forbidDirectoryCreation;
            public bool allowKnownDirectoriesSection;
            public string newDirectoryName;
            public string saveFileName;
            //----------------------------------------------------

            public string chosenPath;
            public bool rescan;
            public int uniqueNumber;

            public ImGuiTextFilter filter = new ImGuiTextFilter().Init();
            public bool allowFiltering;

            public int totalNumBrowsingEntries;
            public int numBrowsingColumns;
            public int numBrowsingEntriesPerColumn;
            public static bool BrowsingPerRow;
            public bool allowDisplayByOption;

            public bool detectKnownDirectoriesAtEveryOpening;
            public bool mustFilterSaveFilePathWithFileFilterExtensionString;

            public bool editLocationCheckButtonPressed;
            public string editLocationInputText;

            // public static bool BrowsingPerRow = false;

			public void resetVariables() {
				currentFolder = "./";
				forceRescan = false;
				open = true;

				wndTitle = "";
				sortingMode = 0;

				history.reset();

				isSelectFolderDialog = false;
				isSaveFileDialog = false;

				allowDirectoryCreation = true;
				forbidDirectoryCreation = false;
				newDirectoryName = "New Folder";
				saveFileName = "";

				uniqueNumber = 0;

				rescan = true;
				chosenPath = "";

				filter.Clear();
				allowFiltering = false;

				totalNumBrowsingEntries = 0;
				numBrowsingColumns = 1;
				numBrowsingEntriesPerColumn = 1000;

				detectKnownDirectoriesAtEveryOpening = false;
				allowDisplayByOption = false;
				allowKnownDirectoriesSection = true;

				mustFilterSaveFilePathWithFileFilterExtensionString = true;

				editLocationCheckButtonPressed = false;
				editLocationInputText = "\0";
			}

			// Just a convenience enum used internally
			public enum Color {
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

			}
            public static void ColorCombine(ref ImVec4 c, ImVec4 r, ImVec4 factor) {
                const float onethird = 1f / 3f;
                float rr = (r.X + r.Y + r.Z) * onethird;
                c.X = rr * factor.X;
                c.Y = rr * factor.Y;
                c.Z = rr * factor.Z;
                c.W = r.W;
            }
        }

        enum Sorting : int {
            SORT_ORDER_ALPHABETIC = 0,
            SORT_ORDER_ALPHABETIC_INVERSE = 1,
            SORT_ORDER_LAST_MODIFICATION = 2,
            SORT_ORDER_LAST_MODIFICATION_INVERSE = 3,
            SORT_ORDER_SIZE = 4,
            SORT_ORDER_SIZE_INVERSE = 5,
            SORT_ORDER_TYPE = 6,
            SORT_ORDER_TYPE_INVERSE = 7,
            SORT_ORDER_COUNT
        };

    }
}
