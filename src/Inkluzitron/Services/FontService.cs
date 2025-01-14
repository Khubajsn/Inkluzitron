﻿using System;
using System.Linq;
using System.Drawing.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Inkluzitron.Resources.Fonts;
using Microsoft.Extensions.Logging;

namespace Inkluzitron.Services
{
    public class FontService : IDisposable
    {
        private readonly object _lock = new();
        private readonly List<PrivateFontCollection> _fontCollections = new();
        private readonly List<IntPtr> _fontMemories = new();

        public FontFamily OpenSansCondensedLight { get; }
        public FontFamily OpenSansCondensed { get; }
        public ILogger<FontService> Logger { get; }

        public FontService(ILogger<FontService> logger)
        {
            Logger = logger;
            OpenSansCondensed = LoadFontCollectionFromResource(FontsResources.OpenSansCondensed);
            OpenSansCondensedLight = LoadFontCollectionFromResource(FontsResources.OpenSansCondensedLight);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _fontCollections.ForEach(x => x.Dispose());
            _fontMemories.ForEach(Marshal.FreeCoTaskMem);
        }

        // https://stackoverflow.com/a/23658552
        private FontFamily LoadFontCollectionFromResource(byte[] fontBytes)
        {
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);

            lock (_lock)
            {
                var fontCollection = new PrivateFontCollection();
                fontCollection.AddMemoryFont(fontData, fontBytes.Length);

                _fontCollections.Add(fontCollection);
                _fontMemories.Add(fontData);
                return fontCollection.Families.FirstOrDefault()
                    ?? throw new InvalidOperationException("Error loading font from resources.");
            }
        }
    }
}
