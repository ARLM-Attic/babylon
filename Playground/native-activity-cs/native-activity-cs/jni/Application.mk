#----------------------------------------------------------------------------------
# File:        3d/native_hdr/jni/Application.mk
# SDK Version: v10.14 
# Email:       tegradev@nvidia.com
# Site:        http://developer.nvidia.com/
#
# Copyright (c) 2007-2012, NVIDIA CORPORATION.  All rights reserved.
#
# TO  THE MAXIMUM  EXTENT PERMITTED  BY APPLICABLE  LAW, THIS SOFTWARE  IS PROVIDED
# *AS IS*  AND NVIDIA AND  ITS SUPPLIERS DISCLAIM  ALL WARRANTIES,  EITHER  EXPRESS
# OR IMPLIED, INCLUDING, BUT NOT LIMITED  TO, IMPLIED WARRANTIES OF MERCHANTABILITY
# AND FITNESS FOR A PARTICULAR PURPOSE.  IN NO EVENT SHALL  NVIDIA OR ITS SUPPLIERS
# BE  LIABLE  FOR  ANY  SPECIAL,  INCIDENTAL,  INDIRECT,  OR  CONSEQUENTIAL DAMAGES
# WHATSOEVER (INCLUDING, WITHOUT LIMITATION,  DAMAGES FOR LOSS OF BUSINESS PROFITS,
# BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR ANY OTHER PECUNIARY LOSS)
# ARISING OUT OF THE  USE OF OR INABILITY  TO USE THIS SOFTWARE, EVEN IF NVIDIA HAS
# BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.
#
#
#----------------------------------------------------------------------------------
# The ARMv7 is significanly faster due to the use of the hardware FPU
APP_ABI := armeabi-v7a
APP_PLATFORM := android-10
APP_STL := stlport_static
#NDK_TOOLCHAIN := clang3.4
APP_CPPFLAGS += -Wno-error=format-security -fexceptions -frtti -std=c++11 -fPIC
