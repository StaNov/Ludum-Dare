#! /bin/sh

project="GameOfLife"
TEST_RESULTS_PATH="$(pwd)/Build/testResults.xml"

echo "===== Attempting to test project in Editor ====="
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

echo '===== Test results if file exists ====='
if [ -f "$TEST_RESULTS_PATH" ]; then
  cat "$TEST_RESULTS_PATH";
fi

exit $EXIT_CODE