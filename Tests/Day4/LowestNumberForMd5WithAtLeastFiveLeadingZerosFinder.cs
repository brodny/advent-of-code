using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Tests.Day4
{
    public sealed class LowestNumberForMd5WithAtLeastFiveLeadingZerosFinder : ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder
    {
        public int FindLowestNumber(string secretKey, byte leadingZeros)
        {
            if (secretKey == null)
                throw new ArgumentNullException(nameof(secretKey));

            using (MD5 md5 = MD5.Create())
            {
                int i = 0;
                while (i < int.MaxValue)
                {
                    byte[] hash = GetMd5Hash(md5, $"{secretKey}{i + 1}");
                    if (HasSpecifiedNumberOfLeadingZeros(hash, leadingZeros))
                        return i + 1;
                    ++i;
                }
            }

            throw new ArgumentException("Cannot find number for which hash has at least five leading zeros.");
        }

        private bool HasSpecifiedNumberOfLeadingZeros(byte[] hash, byte leadingZeros)
        {
            if (hash.Length < leadingZeros)
                return false;

            if (leadingZeros == 0)
                return true;

            int bytesForLeadingZeros = (leadingZeros + 1) / 2;
            for (int i = 0; i < bytesForLeadingZeros - 1; ++i)
            {
                if (hash[i] != 0)
                    return false;
            }

            return leadingZeros % 2 == 0
                ? hash[bytesForLeadingZeros - 1] == 0
                : hash[bytesForLeadingZeros - 1] < 10;
        }

        private byte[] GetMd5Hash(MD5 md5, string input)
        {
            Debug.Assert(md5 != null);
            Debug.Assert(input != null);

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            return hash;
        }
    }
}