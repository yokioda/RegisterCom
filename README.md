# RegisterCom
A way to neatly register COM files using wixsharp.


This is a way to register COM files like .dll or .ocx without the need to depend on external programs like regsvr32.exe or the use of the frowned upon self registration.

It utilizes heat.exe from the WiX toolkit to extract the registration data and adds it to the installers .wxs file, therefore eliminating the need to go through each file manually to add the entries to the generated .wxs file.
