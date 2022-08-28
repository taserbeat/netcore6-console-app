#!/bin/bash

<<COMMENTOUT
CLIアプリケーションのビルド
COMMENTOUT

# -------------------------------------------------------------------------------------------

readonly SCRIPT_EXECUTED_DIR=$(pwd)
readonly SCRIPT_DIR=$(
  cd $(dirname $0)
  pwd
)

# -------------------------------------------------------------------------------------------

function build_tool() {
  cd ${SCRIPT_DIR}
  if [ -e ./build/ ]; then
    rm -rf build/
  fi

  dotnet restore

  # .csprojが存在するディレクトリパス
  readonly CSPROJECT_DIR="${SCRIPT_DIR}/src/Netcore6ConsoleApp"

  # .csprojのファイルパス
  readonly CSPROJECT_PATH="${CSPROJECT_DIR}/Netcore6ConsoleApp.csproj"

  # プロジェクト名
  readonly CSPROJECT_NAME=$(
    basename ${CSPROJECT_PATH} ".csproj"
  )

  # ビルドの成果物を出力するディレクトリ
  readonly BUILD_DEST_DIR_ROOT="${SCRIPT_DIR}/build"

  # zip圧縮で出力するディレクトリ
  readonly ZIP_DEST_DIR="${BUILD_DEST_DIR_ROOT}/zip"

  # https://docs.microsoft.com/ja-jp/dotnet/core/rid-catalog
  readonly RUNTIMES=("linux-x64" "osx-x64" "win-x64")

  # Directory.Build.props が存在するディレクトリ
  readonly PROPS_DIR="${SCRIPT_DIR}/src"
  readonly PROPS_FILENAME="Directory.Build.props"

  cd ${PROPS_DIR}

  readonly VERSION=$(
    cat ${PROPS_FILENAME} |
      grep -E "<Version>([0-9A-Za-z_.\-]+?)</Version>" |
      sed -E "s/[ \f\n\r\t]+.?<Version>([0-9A-Za-z_.\-]+.?)<\/Version>.*$/\1/"
  )

  for runtime in "${RUNTIMES[@]}"; do
    cd ${CSPROJECT_DIR}

    # ビルドの出力先ディレクトリ
    build_dest_dir="${BUILD_DEST_DIR_ROOT}/${runtime}/Netcore6ConsoleApp"

    dotnet publish ${CSPROJECT_PATH} -c Release -o ${build_dest_dir} --runtime ${runtime} --no-self-contained -p:DebugType=None

    # # 以下、必要なファイルをfrontendフォルダからコピー
    # privateroot_dir="${build_dest_dir}/privateroot"
    # wwwroot_dir="${build_dest_dir}/wwwroot"
    # frontend_build_dir="${SCRIPT_DIR}/frontend/build"

    # mkdir -p ${privateroot_dir}
    # mkdir -p ${wwwroot_dir}

    # cp -r "${frontend_build_dir}/" ${privateroot_dir}
    # cp "${frontend_build_dir}/favicon.ico" ${wwwroot_dir}
    # cp "${frontend_build_dir}/manifest.json" ${wwwroot_dir}
    # cp "${frontend_build_dir}/robots.txt" ${wwwroot_dir}

    mkdir -p ${ZIP_DEST_DIR}

    # if [ ! -e ${build_dest_dir} ]; then
    #   continue
    # fi

    # ビルドで出力したディレクトリを一時的にコピーした後、zip圧縮する
    cd ${ZIP_DEST_DIR}

    app_tag="${CSPROJECT_NAME}_${runtime}_${VERSION}"
    tmp_dir_path="./${app_tag}/"
    cp -r ${build_dest_dir} ${tmp_dir_path}

    zip_dest_path="./${app_tag}.zip"
    zip -q -r ${zip_dest_path} ${tmp_dir_path}

    rm -r ${tmp_dir_path}

  done

}

function build_all() {
  build_tool
}

# -------------------------------------------------------------------------------------------

# アクションの実行
build_all

cd ${SCRIPT_EXECUTED_DIR}
