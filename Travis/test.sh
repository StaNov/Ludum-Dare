#! /bin/sh

project="GameOfLife"

echo "Attempting to test $project in Editor."
/Applications/Unity/Unity.app/Contents/MacOS/Unity
  -runTests
  -batchmode 
  -nographics 
  -silent-crashes 
  -logFile $(pwd)/unity.log 
  -projectPath "$(pwd)/Ludum Dare 39/Ludum Dare 39"
  -quit

echo 'Logs from build'
cat $(pwd)/unity.log