#! /bin/sh
set -e

BUILD_FOLD_NAME="runTests_$TEST_PLATFORM"

echo -en "travis_fold:start:$BUILD_FOLD_NAME\r"
echo "===== Building Windows player ====="
  
/Applications/Unity/Unity.app/Contents/MacOS/Unity \ 
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$PROJECT_DIR" \
  -buildWindowsPlayer "$(pwd)/Build/windows/build.exe"

EXIT_CODE=$?
echo -en "travis_fold:end:$BUILD_FOLD_NAME\r"

echo "=== Build complete! ==="

exit $EXIT_CODE