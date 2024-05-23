using foxit.common.fxcrt;
using System.Runtime.InteropServices;

namespace XfaFlatteningApi.Utils
{
    public class CustomStreamCallback : StreamCallback
    {
        private readonly List<byte[]> _bufferList;
        private long _totalSize;

        public CustomStreamCallback()
        {
            _bufferList = new List<byte[]>();
            _totalSize = 0;
        }

        public override bool Flush() { return true; }

        public override long GetPosition() { return 0; }

        public override long GetSize() { return _totalSize; }

        public override bool IsEOF() { return true; }

        public override bool ReadBlock(IntPtr buffer, long offset, uint size)
        {
            return true;
        }

        public override uint ReadBlock(IntPtr buffer, uint size)
        {
            return 0;
        }

        public override void Release()
        {
        }

        public override StreamCallback? Retain() { return null; }

        public override bool WriteBlock(IntPtr buffer, long offset, uint size)
        {
            // This method stores the buffer data into the list
            byte[] data = new byte[size];
            Marshal.Copy(buffer, data, 0, (int)size);
            _bufferList.Add(data);
            _totalSize += size;
            return true;
        }

        public byte[] GetFlattenedData()
        {
            // Combine all buffer data into a single byte array
            byte[] flattenedData = new byte[_totalSize];
            int offset = 0;
            foreach (var buffer in _bufferList)
            {
                Buffer.BlockCopy(buffer, 0, flattenedData, offset, buffer.Length);
                offset += buffer.Length;
            }
            return flattenedData;
        }
    }

}
