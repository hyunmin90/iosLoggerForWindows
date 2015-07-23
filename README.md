# iosLoggerForWindows

This is iOS system log viewer windows. 

I am using c# as framing the UI and parsing the log.

I am also using the libmobiledevice library for getting syslog line by line.

Execution file is included at Zip file at the root directory of this repo. Have a try at the iosSyslogger.exe with full UI support. 

library was compiled in mingw environment and I highly recommend you build your own version of libimobiledevice library for customization if you intend to develop a tool for iOS devices. 

Some of the functionalities of this tool are log selection, log search, log highlight, log filtering and saving log in JSON format and loading it.  

It also support UTF-8 encoding unlike the native library provided by libimobiledevice. ( I have compiled the code in UTF-8 format to solve this issue in mingw). 

for details, you can reach out to me at hyunmin90@gmail.com 

TIPS for compiling libimobiledevice in Windows environment.

Install below
mingw 
mingw-developer-toolkit
mingw32-base
mingw32-gcc-g++
msys-base
msys-wget
pthreads

###Compiling
 
create a new file in C:\MinGW\msys\1.0\etc\fstab and add a line below using text editor
c:/mingw  /mingw
glib, pkg-config and pkg-config-dev must be included in each folder it correspond to inside mingw 
 

 
 in order to compile libimobiledevice, you must have 
 libxml2(always install the newest version), libplist, libusbmuxd, zlib 
 ( zlib has special compile instructions
 
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
define sleep new Sleep(x*1000) requires ; ()  sort of error. Go ahead and delete this line in your /src/libuxmuxd.c and try to compile it again. This will solve your problem completely. 

Your compile process should be easy afterwarad. 
Special thansk to good instruction for compiling (http://quamotion.mobi/iMobileDevice/Article/compiling) 



