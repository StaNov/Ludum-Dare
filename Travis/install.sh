#! /bin/sh
set -e

BASE_URL=http://beta.unity3d.com/download

download() {
  file=$1
  url="$BASE_URL/$INSTALLER_HASH/$package"

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

install "MacEditorInstaller/Unity-$INSTALLER_VERSION.pkg"