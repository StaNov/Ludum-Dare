#! /bin/sh

project="GameOfLife"

echo "Attempting to build $project for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity 
  -batchmode 
  -nographics 
  -silent-crashes 
  -logFile $(pwd)/unity.log 
  -projectPath "$(pwd)/Ludum Dare 39/Ludum Dare 39"
  -buildWindowsPlayer "$(pwd)/Build/windows/$project.exe" 
  -quit

echo "Attempting to build $project for WebGL"
/Applications/Unity/Unity.app/Contents/MacOS/Unity 
  -batchmode 
  -nographics 
  -silent-crashes 
  -logFile $(pwd)/unity.log 
  -projectPath "$(pwd)/Ludum Dare 39/Ludum Dare 39"
  -buildWebGLPlayer "$(pwd)/Build/webgl" 
  -quit

echo 'Logs from build'
cat $(pwd)/unity.log


echo 'Attempting to zip builds'
zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/
zip -r $(pwd)/Build/webgl.zip $(pwd)/Build/webgl/