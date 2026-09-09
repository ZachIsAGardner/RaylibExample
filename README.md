## How to run:
1) Execute ``dotnet build`` (only if bin folder doesn't exist) 
2) Select "Run" under Run and Debug

## About
setup_debug.js automatically moves ``raylib.dll`` and ``SDL2.dll`` into the ``/bin`` folder, but only for Windows. For other platforms you gotta do this manually.

This project uses a slightly modified version of Raylib. By default Raylib uses GLFW, but I wanted controller rumble which GLFW does not support so I recompiled Raylib to use SDL2 instead.
