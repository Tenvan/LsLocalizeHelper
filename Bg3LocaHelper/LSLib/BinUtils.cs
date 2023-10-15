using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LSLib.LS
{
    public static class BinUtils
    {
        public static T ReadStruct<T>(BinaryReader reader)
        {
            T outStruct;
            int count = Marshal.SizeOf(typeof(T));
            byte[] readBuffer = reader.ReadBytes(count);
            GCHandle handle = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
            outStruct = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return outStruct;
        }

        public static void ReadStructs<T>(BinaryReader reader, T[] elements)
        {
            int elementSize = Marshal.SizeOf(typeof(T));
            int bytes = elementSize * elements.Length;
            byte[] readBuffer = reader.ReadBytes(bytes);
            GCHandle handle = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
            var addr = handle.AddrOfPinnedObject();
            for (var i = 0; i < elements.Length; i++)
            {
                var elementAddr = new IntPtr(addr.ToInt64() + elementSize * i);
                elements[i] = Marshal.PtrToStructure<T>(elementAddr);
            }
            handle.Free();
        }

        public static void WriteStruct<T>(BinaryWriter writer, ref T inStruct)
        {
            int count = Marshal.SizeOf(typeof(T));
            byte[] writeBuffer = new byte[count];
            GCHandle handle = GCHandle.Alloc(writeBuffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(inStruct, handle.AddrOfPinnedObject(), true);
            handle.Free();
            writer.Write(writeBuffer);
        }

        public static void WriteStructs<T>(BinaryWriter writer, T[] elements)
        {
            int elementSize = Marshal.SizeOf(typeof(T));
            int bytes = elementSize * elements.Length;
            byte[] writeBuffer = new byte[bytes];
            GCHandle handle = GCHandle.Alloc(writeBuffer, GCHandleType.Pinned);
            var addr = handle.AddrOfPinnedObject();
            for (var i = 0; i < elements.Length; i++)
            {
                var elementAddr = new IntPtr(addr.ToInt64() + elementSize * i);
                Marshal.StructureToPtr(elements[i], elementAddr, true);
            }
            handle.Free();
            writer.Write(writeBuffer);
        }
    }
}
