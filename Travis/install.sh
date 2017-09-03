#! /bin/sh
set -e

BASE_URL=http://beta.unity3d.com/download
HASH=472613c02cf7
VERSION=2017.1.0f3

download() {
  file=$1
  url="$BASE_URL/$HASH/$package"
  mkdir -p UnityInstallFileFolder
  cd UnityInstallFileFolder

  echo "Downloading from $url: "
  curl -o `basename "$package"` "$url"
  cd ..
}

install() {
  package=$1
  download "$package"

  cd UnityInstallFileFolder
  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package `basename "$package"` -target /
  cd ..
}

install "MacEditorInstaller/Unity-$VERSION.pkg"