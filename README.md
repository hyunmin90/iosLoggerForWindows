# iosLoggerForWindows

This is iOS system log viewer windows. 

I am using c# as framing the UI and parsing the log.

I am also using the libmobiledevice library for getting syslog line by line.

Execution included at iosLoggerForWindows/IosSysLogger/bin/Debug iOSsyslogger.exe (All of the dll and exe files must be included with it to be run correctly)

library was compiled in mingw environment and I highly recommend you build your own version of libimobiledevice library for customization if you intend to develop a tool for iOS devices. 

Some of the functionalities of this tool are log selection, log search, log highlight, log filtering and saving log in JSON format and loading it.  

It also support UTF-8 encoding unlike the native library provided by libimobiledevice. ( I have compiled the code in UTF-8 format to solve this issue in mingw). 

for details, you can reach out to me at hyunmin90@gmail.com 
