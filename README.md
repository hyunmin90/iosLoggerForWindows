# iOSlogViewerForWindows

This is iOS system log viewer windows. 

I am using c# as framing the UI and parsing the log.

I am also using the libmobiledevice library for getting syslog and getting informations regarding device.


![Image of iosSyslgoger](https://github.com/hyunmin90/iosLoggerForWindows/blob/master/syslogger.png) 

###Details

Execution file is included inside Zip file at the root directory of this repo.  iosSyslogger.exe is the exe with UI support that lets you filter and search system log.  

library was compiled in mingw environment and I highly recommend you build your own version of libimobiledevice library for customization if you intend to develop a tool for iOS devices. 

Some of the functionalities of this tool are searching, highlighting, filtering saving log in JSON format and loading previsouly saved log .  

It also support UTF-8 encoding unlike the native library provided by libimobiledevice.


###TIPS for compiling libimobiledevice in Windows environment.

Install below
  ```
mingw 
mingw-developer-toolkit
mingw32-base
mingw32-gcc-g++
msys-base
msys-wget
pthreads
  ```
  
create a new file in C:\MinGW\msys\1.0\etc\fstab and add a line below using text editor
c:/mingw  /mingw
glib, pkg-config and pkg-config-dev must be included in each folder it correspond to inside mingw 
 

 
 in order to compile libimobiledevice, you must obtain 
 libxml2(always install the newest version), libplist, libusbmuxd, zlib, openssl 
 
 most of the package should be compiled installed using 
  ```
  ./autogen.sh --prefix=/mingw --without-cython
  make
  make install
   ```
 Without cython refers to ignoring python clibrary which is not necessarily need for this procedures. If you proceed without
 cython, it will let you avoid python not found errors.
 zlib has special compile instructions
 
 ```
make -f win32/Makefile.gcc
BINARY_PATH=/mingw/bin INCLUDE_PATH=/mingw/include LIBRARY_PATH=/mingw/lib make install -f win32/Makefile.gcc SHARED_MODE=1)
```
and finally openSSL (it also has special instruction for installation) 
 ```
./Configure mingw shared no-jpake no-krb5 no-md2 no-rc5 no-static-engine --prefix=/mingw
make depend
make
make install
 ```
For those of you installing in windows environment might run in to a compilation issue while installing libusmuxd. 
define sleep new Sleep(x*1000) requires ; ()  might cause syntax error on the way. Go ahead and delete this line in your /src/libuxmuxd.c and try to compile it again. This will solve your problem completely. 

Your compile process should be easy afterwarad. 
Detail instruction on how to compile can be found here (http://quamotion.mobi/iMobileDevice/Article/compiling) 

###MAC VERSION 
My co-worker has also built mac version of iOS log viewer tool for mac. Link can be found here
https://github.com/gmggae/DeviceLogView


