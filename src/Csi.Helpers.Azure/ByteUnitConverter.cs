namespace Csi.Helpers.Azure
{
    /// <summary>
    /// Azure disk and file service uses size in GiB, but csi api uses bytes, 
    /// this is a helper class to convert between the two
    /// </summary>
    public class ByteUnitConverter
    {
        /// <summary>
        /// Convert required byptes to GiB, do round up if size is less than 1 GiB.
        /// </summary>
        /// <param name="sizeInByte"></param>
        /// <returns></returns>
        public int ToGibibyte(long sizeInByte)
        {
            if (sizeInByte <= 0) sizeInByte = 1;
            // round up
            return (int)(((sizeInByte - 1) >> 30) + 1);
        }

        public long FromGigibyte(int sizeInGigibyte) => sizeInGigibyte << 30;
    }
}
