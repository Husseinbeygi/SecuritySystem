using System;
using System.Diagnostics;

namespace RtspClientCore.Utils
{
    static class ArraySegmentExtensions
    {
        public static ArraySegment<T> SubSegment<T>(this ArraySegment<T> arraySegment, int offset)
        {
            Debug.Assert(arraySegment.Array != null, "arraySegment.Array != null");
            return new ArraySegment<T>(arraySegment.Array, arraySegment.Offset + offset, arraySegment.Count - offset);
        }
    }
}