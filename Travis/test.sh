#! /bin/sh
set -e

project="GameOfLife"
TEST_RESULTS_PATH="$(pwd)/Build/testResults.xml"
TEST_PLATFORM=$1

echo "\n\n===== Running tests in $TEST_PLATFORM =====\n\n"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -runTests \
  -testPlatform $TEST_PLATFORM \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$PROJECT_DIR" \
  -testResults "$TEST_RESULTS_PATH"

EXIT_CODE=$?

echo '\n\n===== Test results if file exists =====\n\n'
if [ -f "$TEST_RESULTS_PATH" ]; then
  cat "$TEST_RESULTS_PATH";
fi

exit $EXIT_CODE