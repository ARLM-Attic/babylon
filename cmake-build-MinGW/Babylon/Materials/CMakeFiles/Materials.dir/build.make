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
CMAKE_SOURCE_DIR = C:\Dev\BabylonNative

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = C:\Dev\BabylonNative\cmake-build-MinGW

# Include any dependencies generated for this target.
include Babylon/Materials/CMakeFiles/Materials.dir/depend.make

# Include the progress variables for this target.
include Babylon/Materials/CMakeFiles/Materials.dir/progress.make

# Include the compile flags for this target's objects.
include Babylon/Materials/CMakeFiles/Materials.dir/flags.make

Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/flags.make
Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/includes_CXX.rsp
Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj: ../Babylon/Materials/effect.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_1)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Materials.dir\effect.cpp.obj -c C:\Dev\BabylonNative\Babylon\Materials\effect.cpp

Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Materials.dir/effect.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Materials\effect.cpp > CMakeFiles\Materials.dir\effect.cpp.i

Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Materials.dir/effect.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Materials\effect.cpp -o CMakeFiles\Materials.dir\effect.cpp.s

Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.requires:
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.requires

Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.provides: Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.requires
	$(MAKE) -f Babylon\Materials\CMakeFiles\Materials.dir\build.make Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.provides.build
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.provides

Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.provides.build: Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj

Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/flags.make
Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/includes_CXX.rsp
Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj: ../Babylon/Materials/material.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_2)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Materials.dir\material.cpp.obj -c C:\Dev\BabylonNative\Babylon\Materials\material.cpp

Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Materials.dir/material.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Materials\material.cpp > CMakeFiles\Materials.dir\material.cpp.i

Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Materials.dir/material.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Materials\material.cpp -o CMakeFiles\Materials.dir\material.cpp.s

Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.requires:
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.requires

Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.provides: Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.requires
	$(MAKE) -f Babylon\Materials\CMakeFiles\Materials.dir\build.make Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.provides.build
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.provides

Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.provides.build: Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj

Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/flags.make
Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/includes_CXX.rsp
Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj: ../Babylon/Materials/multiMaterial.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_3)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Materials.dir\multiMaterial.cpp.obj -c C:\Dev\BabylonNative\Babylon\Materials\multiMaterial.cpp

Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Materials.dir/multiMaterial.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Materials\multiMaterial.cpp > CMakeFiles\Materials.dir\multiMaterial.cpp.i

Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Materials.dir/multiMaterial.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Materials\multiMaterial.cpp -o CMakeFiles\Materials.dir\multiMaterial.cpp.s

Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.requires:
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.requires

Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.provides: Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.requires
	$(MAKE) -f Babylon\Materials\CMakeFiles\Materials.dir\build.make Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.provides.build
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.provides

Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.provides.build: Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj

Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/flags.make
Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj: Babylon/Materials/CMakeFiles/Materials.dir/includes_CXX.rsp
Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj: ../Babylon/Materials/standardMaterial.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_4)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Materials.dir\standardMaterial.cpp.obj -c C:\Dev\BabylonNative\Babylon\Materials\standardMaterial.cpp

Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Materials.dir/standardMaterial.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Materials\standardMaterial.cpp > CMakeFiles\Materials.dir\standardMaterial.cpp.i

Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Materials.dir/standardMaterial.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Materials\standardMaterial.cpp -o CMakeFiles\Materials.dir\standardMaterial.cpp.s

Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.requires:
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.requires

Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.provides: Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.requires
	$(MAKE) -f Babylon\Materials\CMakeFiles\Materials.dir\build.make Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.provides.build
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.provides

Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.provides.build: Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj

# Object files for target Materials
Materials_OBJECTS = \
"CMakeFiles/Materials.dir/effect.cpp.obj" \
"CMakeFiles/Materials.dir/material.cpp.obj" \
"CMakeFiles/Materials.dir/multiMaterial.cpp.obj" \
"CMakeFiles/Materials.dir/standardMaterial.cpp.obj"

# External object files for target Materials
Materials_EXTERNAL_OBJECTS =

Babylon/Materials/libMaterials.a: Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj
Babylon/Materials/libMaterials.a: Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj
Babylon/Materials/libMaterials.a: Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj
Babylon/Materials/libMaterials.a: Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj
Babylon/Materials/libMaterials.a: Babylon/Materials/CMakeFiles/Materials.dir/build.make
Babylon/Materials/libMaterials.a: Babylon/Materials/CMakeFiles/Materials.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --red --bold "Linking CXX static library libMaterials.a"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && $(CMAKE_COMMAND) -P CMakeFiles\Materials.dir\cmake_clean_target.cmake
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && $(CMAKE_COMMAND) -E cmake_link_script CMakeFiles\Materials.dir\link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
Babylon/Materials/CMakeFiles/Materials.dir/build: Babylon/Materials/libMaterials.a
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/build

Babylon/Materials/CMakeFiles/Materials.dir/requires: Babylon/Materials/CMakeFiles/Materials.dir/effect.cpp.obj.requires
Babylon/Materials/CMakeFiles/Materials.dir/requires: Babylon/Materials/CMakeFiles/Materials.dir/material.cpp.obj.requires
Babylon/Materials/CMakeFiles/Materials.dir/requires: Babylon/Materials/CMakeFiles/Materials.dir/multiMaterial.cpp.obj.requires
Babylon/Materials/CMakeFiles/Materials.dir/requires: Babylon/Materials/CMakeFiles/Materials.dir/standardMaterial.cpp.obj.requires
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/requires

Babylon/Materials/CMakeFiles/Materials.dir/clean:
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials && $(CMAKE_COMMAND) -P CMakeFiles\Materials.dir\cmake_clean.cmake
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/clean

Babylon/Materials/CMakeFiles/Materials.dir/depend:
	$(CMAKE_COMMAND) -E cmake_depends "MinGW Makefiles" C:\Dev\BabylonNative C:\Dev\BabylonNative\Babylon\Materials C:\Dev\BabylonNative\cmake-build-MinGW C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Materials\CMakeFiles\Materials.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : Babylon/Materials/CMakeFiles/Materials.dir/depend
