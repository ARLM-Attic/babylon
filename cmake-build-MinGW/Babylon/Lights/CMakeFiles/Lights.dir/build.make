# CMAKE generated file: DO NOT EDIT!
# Generated by "MinGW Makefiles" Generator, CMake Version 2.8

#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:

# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list

# Suppress display of executed commands.
$(VERBOSE).SILENT:

# A target that is always out of date.
cmake_force:
.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

SHELL = cmd.exe

# The CMake executable.
CMAKE_COMMAND = "C:\Program Files (x86)\CMake 2.8\bin\cmake.exe"

# The command to remove a file.
RM = "C:\Program Files (x86)\CMake 2.8\bin\cmake.exe" -E remove -f

# Escaping for special characters.
EQUALS = =

# The program to use to edit the cache.
CMAKE_EDIT_COMMAND = "C:\Program Files (x86)\CMake 2.8\bin\cmake-gui.exe"

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = D:\Developing\BabylonNative

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = D:\Developing\BabylonNative\cmake-build-MinGW

# Include any dependencies generated for this target.
include Babylon/Lights/CMakeFiles/Lights.dir/depend.make

# Include the progress variables for this target.
include Babylon/Lights/CMakeFiles/Lights.dir/progress.make

# Include the compile flags for this target's objects.
include Babylon/Lights/CMakeFiles/Lights.dir/flags.make

Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj: Babylon/Lights/CMakeFiles/Lights.dir/flags.make
Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj: Babylon/Lights/CMakeFiles/Lights.dir/includes_CXX.rsp
Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj: ../Babylon/Lights/light.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report D:\Developing\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_1)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Lights.dir\light.cpp.obj -c D:\Developing\BabylonNative\Babylon\Lights\light.cpp

Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Lights.dir/light.cpp.i"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E D:\Developing\BabylonNative\Babylon\Lights\light.cpp > CMakeFiles\Lights.dir\light.cpp.i

Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Lights.dir/light.cpp.s"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S D:\Developing\BabylonNative\Babylon\Lights\light.cpp -o CMakeFiles\Lights.dir\light.cpp.s

Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.requires:
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.requires

Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.provides: Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.requires
	$(MAKE) -f Babylon\Lights\CMakeFiles\Lights.dir\build.make Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.provides.build
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.provides

Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.provides.build: Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj

Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj: Babylon/Lights/CMakeFiles/Lights.dir/flags.make
Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj: Babylon/Lights/CMakeFiles/Lights.dir/includes_CXX.rsp
Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj: ../Babylon/Lights/shadowGenerator.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report D:\Developing\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_2)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Lights.dir\shadowGenerator.cpp.obj -c D:\Developing\BabylonNative\Babylon\Lights\shadowGenerator.cpp

Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Lights.dir/shadowGenerator.cpp.i"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E D:\Developing\BabylonNative\Babylon\Lights\shadowGenerator.cpp > CMakeFiles\Lights.dir\shadowGenerator.cpp.i

Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Lights.dir/shadowGenerator.cpp.s"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S D:\Developing\BabylonNative\Babylon\Lights\shadowGenerator.cpp -o CMakeFiles\Lights.dir\shadowGenerator.cpp.s

Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.requires:
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.requires

Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.provides: Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.requires
	$(MAKE) -f Babylon\Lights\CMakeFiles\Lights.dir\build.make Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.provides.build
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.provides

Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.provides.build: Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj

Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj: Babylon/Lights/CMakeFiles/Lights.dir/flags.make
Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj: Babylon/Lights/CMakeFiles/Lights.dir/includes_CXX.rsp
Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj: ../Babylon/Lights/pointLight.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report D:\Developing\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_3)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Lights.dir\pointLight.cpp.obj -c D:\Developing\BabylonNative\Babylon\Lights\pointLight.cpp

Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Lights.dir/pointLight.cpp.i"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E D:\Developing\BabylonNative\Babylon\Lights\pointLight.cpp > CMakeFiles\Lights.dir\pointLight.cpp.i

Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Lights.dir/pointLight.cpp.s"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && F:\Dev\MinGW\mingw64\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S D:\Developing\BabylonNative\Babylon\Lights\pointLight.cpp -o CMakeFiles\Lights.dir\pointLight.cpp.s

Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.requires:
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.requires

Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.provides: Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.requires
	$(MAKE) -f Babylon\Lights\CMakeFiles\Lights.dir\build.make Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.provides.build
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.provides

Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.provides.build: Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj

# Object files for target Lights
Lights_OBJECTS = \
"CMakeFiles/Lights.dir/light.cpp.obj" \
"CMakeFiles/Lights.dir/shadowGenerator.cpp.obj" \
"CMakeFiles/Lights.dir/pointLight.cpp.obj"

# External object files for target Lights
Lights_EXTERNAL_OBJECTS =

Babylon/Lights/libLights.a: Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj
Babylon/Lights/libLights.a: Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj
Babylon/Lights/libLights.a: Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj
Babylon/Lights/libLights.a: Babylon/Lights/CMakeFiles/Lights.dir/build.make
Babylon/Lights/libLights.a: Babylon/Lights/CMakeFiles/Lights.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --red --bold "Linking CXX static library libLights.a"
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && $(CMAKE_COMMAND) -P CMakeFiles\Lights.dir\cmake_clean_target.cmake
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && $(CMAKE_COMMAND) -E cmake_link_script CMakeFiles\Lights.dir\link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
Babylon/Lights/CMakeFiles/Lights.dir/build: Babylon/Lights/libLights.a
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/build

Babylon/Lights/CMakeFiles/Lights.dir/requires: Babylon/Lights/CMakeFiles/Lights.dir/light.cpp.obj.requires
Babylon/Lights/CMakeFiles/Lights.dir/requires: Babylon/Lights/CMakeFiles/Lights.dir/shadowGenerator.cpp.obj.requires
Babylon/Lights/CMakeFiles/Lights.dir/requires: Babylon/Lights/CMakeFiles/Lights.dir/pointLight.cpp.obj.requires
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/requires

Babylon/Lights/CMakeFiles/Lights.dir/clean:
	cd /d D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights && $(CMAKE_COMMAND) -P CMakeFiles\Lights.dir\cmake_clean.cmake
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/clean

Babylon/Lights/CMakeFiles/Lights.dir/depend:
	$(CMAKE_COMMAND) -E cmake_depends "MinGW Makefiles" D:\Developing\BabylonNative D:\Developing\BabylonNative\Babylon\Lights D:\Developing\BabylonNative\cmake-build-MinGW D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights D:\Developing\BabylonNative\cmake-build-MinGW\Babylon\Lights\CMakeFiles\Lights.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : Babylon/Lights/CMakeFiles/Lights.dir/depend

