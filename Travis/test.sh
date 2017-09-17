#! /bin/sh
set -e

project="GameOfLife"
TEST_RESULTS_PATH="$(pwd)/Build/testResults.xml"
TEST_PLATFORM=$1
RUN_TESTS_FOLD_NAME="runTests_$TEST_PLATFORM"

echo "travis_fold:start:$RUN_TESTS_FOLD_NAME"
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
echo "travis_fold:end:$RUN_TESTS_FOLD_NAME"

echo '\n\n===== Test results if file exists =====\n\n'
if [ -f "$TEST_RESULTS_PATH" ]; then
  echo '\n\n===== Test results of $TEST_PLATFORM =====\n\n';
  cat "$TEST_RESULTS_PATH";
else
  echo '\n\n===== Test results of $TEST_PLATFORM do not exist! =====\n\n';
fi

exit $EXIT_CODE