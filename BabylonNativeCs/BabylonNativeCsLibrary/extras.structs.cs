﻿using System;
using Web;

namespace BABYLON
{
    public struct MinMax
    {
        public Vector3 minimum { get; set; }

        public Vector3 maximum { get; set; }
    }

    public struct Size
    {
        public int width { get; set; }

        public int height { get; set; }
    }

    public struct EffectBaseName
    {
        public string baseName { get; set; }

        public string vertexElement { get; set; }

        public string vertex { get; set; }

        public string fragmentElement { get; set; }

        public string fragment { get; set; }
    }

    public struct MinMagFilter
    {
        public int min { get; set; }

        public int mag { get; set; }
    }

    public struct EventDts
    {
        public string name { get; set; }

        public EventListener handler { get; set; }
    }

    public class Cache
    {
        public Node parent { get; set; }
    }

    public class EngineOptions
    {
        public bool antialias { get; set; }
    }

    public class RenderTargetTextureOptions
    {
        public int? samplingMode { get; set; }

        public bool? generateMipMaps { get; set; }

        public bool? generateDepthBuffer { get; set; }
    }

    public class TGAHeader
    {
        public Array<int> origin { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public byte flags { get; set; }

        public byte id_length { get; set; }

        public byte colormap_type { get; set; }

        public byte image_type { get; set; }

        public int colormap_index { get; set; }

        public int colormap_length { get; set; }

        public byte colormap_size { get; set; }

        public byte pixel_size { get; set; }
    }
}