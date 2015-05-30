using System;
using System.Collections.Generic;
using System.Text;

namespace ID3
{
    /// <summary>
    /// Provide TextEncoding for frames that support it.
    /// </summary>
    public enum TextEncodings
    {
        Ascii = 0,
        UTF_16 = 1,
        UTF_16BE = 2,
        UTF8 = 3
    }

    /// <summary>
    /// Provide frame flags for all frames
    /// </summary>
    [Flags]
    public enum FrameFlags
    {
        TagAlterPreservation = 0x8000,
        FileAlterPreservation = 0x4000,
        ReadOnly = 0x2000,
        Compression = 0x0080,
        Encryption = 0x0040,
        GroupingIdentity = 0x0020
    }

    public enum TimeStamps
    {
        MpegFrame = 1,
        Milliseconds
    }

    public enum IncrementDecrement
    {
        Dcrement = 0,
        Increment
    }

    [Flags]
    public enum ID3v2Flags
    {
        Unsynchronisation = 128,
        ExtendedHeader = 64,
        Expremential = 32
    }

    public enum FilterTypes
    {
        NoFilter = 0,
        LoadFiltersOnly = 1,
        NotLoadFilters = 2
    }
}
