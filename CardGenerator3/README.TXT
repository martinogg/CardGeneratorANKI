
NOTES ON SQLITE3.DLL
I copy the SQLITE3.DLL to the output folder if its in windows. If a windows run crashes when loading
a DB file, please check you have SQLITE3.DLL in the output folder. Likewise, if you have strange crashes
in Mac/Linux, make sure SQLITE3.DLL isnt there

POSIX
I've noticed the Mono.Posix lib has a tendancy to turn itself on now and again. I switch it off for
Windows builds because I couldnt get it working otherwise. If anyone has a solution for this, that
would be great
