#! /bin/sh

project="GameOfLife"
test_results_path="$(pwd)/Build/testResults.xml"

echo "Attempting to test $project in Editor."
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -runTests \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$PROJECT_DIR"
  -testResults "$test_results_path"

echo 'Logs from test'
cat $(pwd)/unity.log

echo 'Test results'
cat "$test_results_path"