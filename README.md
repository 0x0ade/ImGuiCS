# ImGuiCS
### dear ImGui + fork of [ImGui.NET](https://github.com/mellinoe/ImGui.NET/) + SDL2-CS, XNA and FNA samples
##### MIT-licensed, fork recursively
----

### Special thanks to my [patrons on Patreon](https://www.patreon.com/0x0ade):
- [Chad Yates](https://twitter.com/ChadCYates)
- [Renaud Bédard](https://twitter.com/renaudbedard)
- [Artus Elias Meyer-Toms](https://twitter.com/artuselias)

### [ImGui.NET](https://github.com/mellinoe/ImGui.NET/) exists already, why fork?
[ImGui.NET](https://github.com/mellinoe/ImGui.NET/) didn't fit my personal needs. It removed the ImGui prefix from the classes and while it already wraps imgui structs well, accessing some things like the fonts still required unsafe blocks in your own project. ImGuiCS aims to avoid forcing you to go the "dirty" route.  
Admittedly, going `unsafe` is the more accurate route, but it makes dealing with things like ImVector more complicated as it should be for a beginner.

### SDL2-CS doesn't load / it's an empty directory! What happened?
You need to `git clone --recursive` to download the SDL2-CS "submodule". If you're not using ImGuiSDL2CS (f.e. you're using ImGuiXNA instead), you can simply disable SDL2-CS in your IDE (Visual Studio / MonoDevelop).

### Visual Studio 2010 can't deal with the new language features!
If you require compatibility, I'd be thankful if you could help me as I don't plan to install Visual Studio 2010. This also means that I don't plan on preserving compatibility with any C# version older than the one provided in VS2015.  
You can still build ImGuiCS and the other projects in VS2015+ and use the resulting binaries.  
Tip: If you're dealing with XNA Game Studio, MXA provides downloads to get it up and running in VS2015: https://mxa.codeplex.com/releases

There is an unofficial, VS2010-compatible lock-step fork available here: https://github.com/conatuscreative/ImGuiCS

### cimgui.dll / SDL2.dll doesn't load!
Use the native libraries from [libs/x86 (32 bit) or libs/x64 (64 bit)](https://github.com/0x0ade/ImGuiCS/tree/master/libs) instead. You could also ship both directories and create a `.dll.config` file for `ImGuiCS.dll` / `SDL2-CS.dll` that takes this into account.

