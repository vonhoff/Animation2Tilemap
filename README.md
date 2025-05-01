# Animation2Tilemap

![workflow](https://img.shields.io/github/actions/workflow/status/vonhoff/Animation2Tilemap/dotnet.yml)
[![version](https://img.shields.io/badge/version-2.0.0-blue)](https://github.com/vonhoff/Animation2Tilemap/releases)
[![license](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

Animation to Tilemap is a tool that converts images or GIF animations into tilemaps and tilesets, compatible with [Tiled](https://www.mapeditor.org/) and other tile-based game development tools.

## Features

- **Versatile Input**: Process Bmp, Gif, Jpeg, Pbm, Png, Tiff, Tga, and WebP formats.
- **Flexible Animation Handling**: Convert animations from folders or multi-frame images.
- **Animated Tileset Generation**: Create animated tilesets from single images or frame folders.
- **Customization Options**: Adjust tile size, transparent color, and frame duration.
- **Tiled Compatibility**: Generate tilesets and tilemaps in various formats (base64, zlib, gzip, csv).

## Example

Convert a GIF animation into a tileset and tilemap:

| Input GIF Animation |    Output Tilemap     |
|:-------------------:|:---------------------:|
| ![Input](input.gif) | ![Output](result.png) |

Image source: https://x.com/jmw327/status/1405872936783802384

## Installation

1. Ensure you have [.NET 8 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.
2. Download the latest release from the [releases page](https://github.com/vonhoff/Animation2Tilemap/releases).
3. Extract the zip file to your preferred location.

## Usage

Basic usage:

```
animation2tilemap -i <input> -o <o>
```

For a full list of options, use:

```
animation2tilemap --help
```

## Motivation

Animation to Tilemap was created to make converting animations into tilesets and tilemaps easier. I think the usual process of manually slicing frames, managing animations, and optimizing tiles is both time-consuming and prone to errors.

I created this tool to automate this entire workflow. It does this by dividing images and animations into individual tiles, and then it identifies and removes duplicates. Finally, it changes these assets into formats that game engines can use.

## Support

If you find Animation2Tilemap useful:

- ⭐ Star the [project on GitHub](https://github.com/vonhoff/Animation2Tilemap)
- 💖 Consider [becoming a sponsor](https://github.com/sponsors/vonhoff)

## License

Animation2Tilemap is open-source software licensed under the [MIT License](LICENSE).
