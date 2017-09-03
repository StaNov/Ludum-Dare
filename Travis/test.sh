#! /bin/sh
set -e

project="GameOfLife"
TEST_RESULTS_PATH="$(pwd)/Build/testResults.xml"
TEST_PLATFORM=$1

echo "\n\n===== Attempting to test project in Editor =====\n\n"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -runTests \
  -testPlatform $TEST_PLATFORM \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$PROJECT_DIR" \
  -testResults "$TEST_RESULTS_PATH"

EXIT_CODE=$?

echo 'Logs from test'
cat $(pwd)/unity.log

echo '\n\n===== Test results if file exists =====\n\n'
if [ -f "$TEST_RESULTS_PATH" ]; then
  cat "$TEST_RESULTS_PATH";
fi

exit $EXIT_CODE