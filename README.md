# Animation2Tilemap

![workflow](https://img.shields.io/github/actions/workflow/status/vonhoff/Animation2Tilemap/dotnet.yml)
[![version](https://img.shields.io/badge/version-2.0.0-blue)](https://github.com/vonhoff/Animation2Tilemap/releases)
[![license](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

Animation to Tilemap is a tool that converts images or GIF animations into tilemaps and tilesets, compatible with [Tiled](https://www.mapeditor.org/) and other tile-based game development tools.

## Features

- **Versatile Input**: Process PNG, BMP, GIF, JPEG, PBM, TIFF, TGA, and WebP formats.
- **Flexible Animation Handling**: Convert animations from folders or multi-frame images.
- **Animated Tileset Generation**: Create animated tilesets from single images or frame folders.
- **Customization Options**: Adjust tile size, transparency color, and frame duration.
- **Tiled Compatibility**: Generate tilesets and tilemaps in various formats (Base64, CSV, zlib, gzip).

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
animation2tilemap -i <input file/folder> -o <output folder>
```

For a full list of options, use:

```
animation2tilemap --help
```

## Motivation

Animation to Tilemap was created to make converting animations into tilesets and tilemaps easier. I think the usual process of manually slicing frames, managing animations, and optimizing tiles is both time-consuming and prone to errors.

## Algorithm Details

The conversion process begins by loading any specified input images. These can be standard image files, multi-frame GIFs, or sequences of images from a directory. If the input is a multi-frame format such as a GIF, the frames are automatically grouped together.

If the input is a directory of images with matching dimensions, the tool checks if they could form an animation. If so, it asks the user if they should be treated as animation frames. If the user agrees, the images are grouped into a single animation. A base name for the group is automatically generated-usually based on common patterns in the filenames-and the tool creates a set of output files (.png, .tsx, .tmx) representing the complete animation. If the user declines, each image is processed separately, resulting in individual output files for each.

Once grouped, all frames go through an alignment process. The tool finds the maximum width and height of all the frames in the group, then rounds these dimensions up to the nearest multiple of the tile size. Each frame is then centered on a new canvas of that size, with transparent padding added as needed so that all the frames are uniformly sized.

Next, the tool processes the aligned frames to generate the tileset and tile animations. Each frame is divided into tiles based on the specified tile size. For every tile position (e.g., top-left tile, the one next to it, etc.), the tool tracks the sequence of tile images appearing at that position across all frames of the animation.

Image hashing is used to identify unique tile images across all frames and positions, ensuring that only one copy of each unique tile image is stored in the final tileset image file. The tool then defines tile animations within the tiled tileset file. For each tile position in the grid, as its appearance changes across frames, a tile animation sequence is created. This sequence lists the unique IDs of the tiles appearing at that position, along with the duration for which each tile should be displayed. Durations for consecutive identical tile images within the sequence are combined into a single animation frame entry.

The tool then generates the tilemap file. This file references the tileset created in the previous step. It reconstructs the original animation sequence by creating a map layer where each tile corresponds to a frame of the original input animation. It uses the unique tile IDs assigned during tileset creation to place the correct tiles in the correct order on the map.

## Support

If you find Animation2Tilemap useful:

- ⭐ Star the [project on GitHub](https://github.com/vonhoff/Animation2Tilemap)
- 💖 Consider [becoming a sponsor](https://github.com/sponsors/vonhoff)

## License

Animation2Tilemap is open-source software licensed under the [MIT License](LICENSE).
