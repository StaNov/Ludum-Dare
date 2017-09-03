#! /bin/sh
set -e

BASE_URL=http://beta.unity3d.com/download
HASH=472613c02cf7
VERSION=2017.1.0f3

download() {
  file=$1
  url="$BASE_URL/$HASH/$package"

  echo "Downloading from $url: "
  curl -o UnityInstallFileFolder/`basename "$package"` "$url"
}

install() {
  package=$1
  
  if [ ! -f UnityInstallFileFolder/`basename "$package"` ]; then
    echo "\n\n=== Unity install file not found, downloading new! === \n\n"
    download "$package"
  else
    echo "\n\n=== Unity install file found cached, using this. === \n\n"
  fi

  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package UnityInstallFileFolder/`basename "$package"` -target /
}

install "MacEditorInstaller/Unity-$VERSION.pkg"