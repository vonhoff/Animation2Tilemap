﻿namespace Animation2Tilemap.Services.Contracts;

public interface IImageAlignmentService
{
    bool TryAlignImage(string fileName, List<Image<Rgba32>> frames);
}