#! /bin/sh

project="GameOfLife"
TEST_RESULTS_PATH="$(pwd)/Build/testResults.xml"

echo "Attempting to test $project in Editor."
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -runTests \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$PROJECT_DIR"

echo 'Logs from test'
cat $(pwd)/unity.log