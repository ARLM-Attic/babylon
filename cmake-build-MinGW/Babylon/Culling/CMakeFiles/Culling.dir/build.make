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
include Babylon/Culling/CMakeFiles/Culling.dir/depend.make

# Include the progress variables for this target.
include Babylon/Culling/CMakeFiles/Culling.dir/progress.make

# Include the compile flags for this target's objects.
include Babylon/Culling/CMakeFiles/Culling.dir/flags.make

Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/flags.make
Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/includes_CXX.rsp
Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj: ../Babylon/Culling/boundingBox.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_1)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Culling.dir\boundingBox.cpp.obj -c C:\Dev\BabylonNative\Babylon\Culling\boundingBox.cpp

Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Culling.dir/boundingBox.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Culling\boundingBox.cpp > CMakeFiles\Culling.dir\boundingBox.cpp.i

Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Culling.dir/boundingBox.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Culling\boundingBox.cpp -o CMakeFiles\Culling.dir\boundingBox.cpp.s

Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.requires:
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.requires

Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.provides: Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.requires
	$(MAKE) -f Babylon\Culling\CMakeFiles\Culling.dir\build.make Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.provides.build
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.provides

Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.provides.build: Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj

Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/flags.make
Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/includes_CXX.rsp
Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj: ../Babylon/Culling/boundingSphere.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_2)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Culling.dir\boundingSphere.cpp.obj -c C:\Dev\BabylonNative\Babylon\Culling\boundingSphere.cpp

Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Culling.dir/boundingSphere.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Culling\boundingSphere.cpp > CMakeFiles\Culling.dir\boundingSphere.cpp.i

Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Culling.dir/boundingSphere.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Culling\boundingSphere.cpp -o CMakeFiles\Culling.dir\boundingSphere.cpp.s

Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.requires:
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.requires

Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.provides: Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.requires
	$(MAKE) -f Babylon\Culling\CMakeFiles\Culling.dir\build.make Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.provides.build
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.provides

Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.provides.build: Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj

Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/flags.make
Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/includes_CXX.rsp
Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj: ../Babylon/Culling/boundingInfo.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_3)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Culling.dir\boundingInfo.cpp.obj -c C:\Dev\BabylonNative\Babylon\Culling\boundingInfo.cpp

Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Culling.dir/boundingInfo.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Culling\boundingInfo.cpp > CMakeFiles\Culling.dir\boundingInfo.cpp.i

Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Culling.dir/boundingInfo.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Culling\boundingInfo.cpp -o CMakeFiles\Culling.dir\boundingInfo.cpp.s

Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.requires:
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.requires

Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.provides: Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.requires
	$(MAKE) -f Babylon\Culling\CMakeFiles\Culling.dir\build.make Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.provides.build
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.provides

Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.provides.build: Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj

Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/flags.make
Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/includes_CXX.rsp
Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj: ../Babylon/Culling/octree.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_4)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Culling.dir\octree.cpp.obj -c C:\Dev\BabylonNative\Babylon\Culling\octree.cpp

Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Culling.dir/octree.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Culling\octree.cpp > CMakeFiles\Culling.dir\octree.cpp.i

Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Culling.dir/octree.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Culling\octree.cpp -o CMakeFiles\Culling.dir\octree.cpp.s

Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.requires:
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.requires

Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.provides: Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.requires
	$(MAKE) -f Babylon\Culling\CMakeFiles\Culling.dir\build.make Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.provides.build
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.provides

Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.provides.build: Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj

Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/flags.make
Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj: Babylon/Culling/CMakeFiles/Culling.dir/includes_CXX.rsp
Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj: ../Babylon/Culling/octreeBlock.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report C:\Dev\BabylonNative\cmake-build-MinGW\CMakeFiles $(CMAKE_PROGRESS_5)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles\Culling.dir\octreeBlock.cpp.obj -c C:\Dev\BabylonNative\Babylon\Culling\octreeBlock.cpp

Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Culling.dir/octreeBlock.cpp.i"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -E C:\Dev\BabylonNative\Babylon\Culling\octreeBlock.cpp > CMakeFiles\Culling.dir\octreeBlock.cpp.i

Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Culling.dir/octreeBlock.cpp.s"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && C:\Dev\Utils\MinGW\bin\g++.exe  $(CXX_DEFINES) $(CXX_FLAGS) -S C:\Dev\BabylonNative\Babylon\Culling\octreeBlock.cpp -o CMakeFiles\Culling.dir\octreeBlock.cpp.s

Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.requires:
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.requires

Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.provides: Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.requires
	$(MAKE) -f Babylon\Culling\CMakeFiles\Culling.dir\build.make Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.provides.build
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.provides

Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.provides.build: Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj

# Object files for target Culling
Culling_OBJECTS = \
"CMakeFiles/Culling.dir/boundingBox.cpp.obj" \
"CMakeFiles/Culling.dir/boundingSphere.cpp.obj" \
"CMakeFiles/Culling.dir/boundingInfo.cpp.obj" \
"CMakeFiles/Culling.dir/octree.cpp.obj" \
"CMakeFiles/Culling.dir/octreeBlock.cpp.obj"

# External object files for target Culling
Culling_EXTERNAL_OBJECTS =

Babylon/Culling/libCulling.a: Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj
Babylon/Culling/libCulling.a: Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj
Babylon/Culling/libCulling.a: Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj
Babylon/Culling/libCulling.a: Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj
Babylon/Culling/libCulling.a: Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj
Babylon/Culling/libCulling.a: Babylon/Culling/CMakeFiles/Culling.dir/build.make
Babylon/Culling/libCulling.a: Babylon/Culling/CMakeFiles/Culling.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --red --bold "Linking CXX static library libCulling.a"
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && $(CMAKE_COMMAND) -P CMakeFiles\Culling.dir\cmake_clean_target.cmake
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && $(CMAKE_COMMAND) -E cmake_link_script CMakeFiles\Culling.dir\link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
Babylon/Culling/CMakeFiles/Culling.dir/build: Babylon/Culling/libCulling.a
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/build

Babylon/Culling/CMakeFiles/Culling.dir/requires: Babylon/Culling/CMakeFiles/Culling.dir/boundingBox.cpp.obj.requires
Babylon/Culling/CMakeFiles/Culling.dir/requires: Babylon/Culling/CMakeFiles/Culling.dir/boundingSphere.cpp.obj.requires
Babylon/Culling/CMakeFiles/Culling.dir/requires: Babylon/Culling/CMakeFiles/Culling.dir/boundingInfo.cpp.obj.requires
Babylon/Culling/CMakeFiles/Culling.dir/requires: Babylon/Culling/CMakeFiles/Culling.dir/octree.cpp.obj.requires
Babylon/Culling/CMakeFiles/Culling.dir/requires: Babylon/Culling/CMakeFiles/Culling.dir/octreeBlock.cpp.obj.requires
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/requires

Babylon/Culling/CMakeFiles/Culling.dir/clean:
	cd /d C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling && $(CMAKE_COMMAND) -P CMakeFiles\Culling.dir\cmake_clean.cmake
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/clean

Babylon/Culling/CMakeFiles/Culling.dir/depend:
	$(CMAKE_COMMAND) -E cmake_depends "MinGW Makefiles" C:\Dev\BabylonNative C:\Dev\BabylonNative\Babylon\Culling C:\Dev\BabylonNative\cmake-build-MinGW C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling C:\Dev\BabylonNative\cmake-build-MinGW\Babylon\Culling\CMakeFiles\Culling.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : Babylon/Culling/CMakeFiles/Culling.dir/depend
