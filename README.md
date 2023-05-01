# Animation2Tilemap

Animation2Tilemap is a command-line tool that converts animations into tilesets and tilemaps that can be used in Tiled, a popular map editor for 2D games. With Animation2Tilemap, you can easily create tile-based versions of your animations and import them into Tiled for further editing.

## Features

Animation2Tilemap offers the following features:

- Supports various input formats, including Bmp, Gif, Jpeg, Pbm, Png, Tiff, Tga, and WebP.
- Allows you to process animations from either folders or images that contain multiple frames.
- Allows you to customize the tile size, transparent color, and frame duration of your animations.
- Generates tilesets and tilemaps in Tiled compatible formats, such as base64, zlib, gzip, and csv.
- Provides a simple and intuitive command-line interface with helpful options and usage information.

## Installation

To install Animation2Tilemap, a tool that converts animations into tilemaps, you need to follow these steps:

- Go to the [releases page](https://github.com/Animation2Tilemap/Animation2Tilemap/releases) and download the latest version of Animation2Tilemap.
- Extract the zip file to a folder of your choice. You can use any file manager or unzip utility to do this.
- Inside the extracted folder, you will find a single executable file called `Animation2Tilemap.exe`. 

To see the help page within the application, you can run Animation2Tilemap with the `--help` option. This will show you the available options and parameters that you can use with Animation2Tilemap.

## Usage

Before using Animation2Tilemap, make sure you have the .NET 7 runtime installed on your system. You can get it from https://dotnet.microsoft.com/en-us/download/dotnet/7.0

Next, open a terminal and go to the folder where you installed Animation2Tilemap. To convert an animation file or folder into a tileset and tilemap, run this command: `animation2tilemap -i <input> -o <output>` where `<input>` and `<output>` are the paths to the animation and output folders respectively.

You can also specify other options to customize the output, such as:

| Option | Description | Default |
| --- | --- | --- |
| `-d`, `--duration <duration>` | The duration of each animation frame in milliseconds. | `125` |
| `-h`, `--height <height>` | The height of each tile in pixels. | `8` |
| `-w`, `--width <width>` | The width of each tile in pixels. | `8` |
| `-t`, `--transparent <transparent>` | The color that will be treated as transparent in the tileset image. The color should be specified in RGBA format. | `00000000` |
| `-f`, `--format <base64|zlib|gzip|csv>` | The format of the tile layer data in the generated tilemap file. | `zlib` |
| `-v`, `--verbose` | Enables verbose logging for debugging purposes. | `False` |
| `-?`, `--help` | Shows help and usage information. |  |

For example, this command converts the `anim` folder into a tileset and a tilemap with 16x16 pixels tiles, magenta transparent color, Gzip compression and 200ms frame duration:

`animation2tilemap -i anim -o output -h 16 -w 16 -t FF00FF -f gzip -d 200`

## License

Animation2Tilemap is licensed under the MIT License. See [LICENSE](LICENSE) file for more details.

## Contribution

If you want to contribute to Animation2Tilemap, you are welcome to submit pull requests or report issues on GitHub.