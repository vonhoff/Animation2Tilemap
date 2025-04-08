# Animation2Tilemap

![workflow](https://img.shields.io/github/actions/workflow/status/vonhoff/Animation2Tilemap/dotnet.yml)
[![version](https://img.shields.io/badge/version-2.0.0-blue)](https://github.com/vonhoff/Animation2Tilemap/releases)
[![license](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

Animation to Tilemap is a tool that converts images or GIF animations into tilemaps
and tilesets, compatible with [Tiled](https://www.mapeditor.org/) and other tile-based game development tools.

## Features

- **Versatile Input Support**: Process Bmp, Gif, Jpeg, Pbm, Png, Tiff, Tga, and WebP formats.
- **Flexible Animation Handling**: Convert animations from folders or multi-frame images.
- **Animated Tileset Generation**: Create animated tilesets from single images or frame folders.
- **Customization Options**: Adjust tile size, transparent color, and frame duration.
- **Tiled Compatibility**: Generate tilesets and tilemaps in various formats (base64, zlib, gzip, csv).
- **Dual Interface**: Utilize both command-line and user-friendly GUI options.

## Example

Convert a GIF animation into a tileset and tilemap:

|   Input GIF Animation   |    Output Tilemap     |
|:-----------------------:|:---------------------:|
| ![Input](input.gif)     | ![Output](result.png) |

Image source: https://x.com/jmw327/status/1405872936783802384

## Installation

1. Ensure you have [.NET 8 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.
2. Download the latest release from the [releases page](https://github.com/vonhoff/Animation2Tilemap/releases).
3. Extract the zip file to your preferred location.

## Usage

### Command-line Interface

Basic usage:

```
animation2tilemap.console -i <input> -o <output>
```

For a full list of options, use:

```
animation2tilemap.console --help
```

### Graphical User Interface

Launch the `Animation2Tilemap.WinForms.exe` file for a visual interface.

![Program screenshot](screenshot_win.png)

## Motivation

This tool was created to streamline the process of converting a GIF to a tileset and tilemap. 
Manually slicing each frame, manipulating animations, and optimizing tiles can be
time-consuming and error-prone. 

Animation to Tilemap aims to solve these problems:

- Automate the process of slicing images and animations into individual tiles
- Identify and remove duplicate tiles to optimize memory usage
- Convert assets into game engine compatible formats

They are also useful in these scenarios:

- Converting detailed backgrounds into tile-based formats
- Converting character animations into sprite sheets
- Optimize large maps or procedurally generated worlds

## Contributing

Contributions are welcome! Here's how you can help:

- **Report Issues**: Submit bugs or suggest features
  via [GitHub Issues](https://github.com/vonhoff/Animation2Tilemap/issues).
- **Pull Requests**: Improve code, documentation, or add new features.
- **Feedback**: Share your experience or ideas for improvement.

## Support

If you find Animation2Tilemap useful:

- ⭐ Star the [project on GitHub](https://github.com/vonhoff/Animation2Tilemap)
- 💖 Consider [becoming a sponsor](https://github.com/sponsors/vonhoff)

## License

Animation2Tilemap is open-source software licensed under the [MIT License](LICENSE).
