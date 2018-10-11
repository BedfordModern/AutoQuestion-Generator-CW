using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionApp.Accounts
{
    class Password_Hasher
    {
        private static readonly ulong[] HexArrary =
        {
            0x428a2f98d728ae22, 0x7137449123ef65cd, 0xb5c0fbcfec4d3b2f, 0xe9b5dba58189dbbc,
            0x3956c25bf348b538, 0x59f111f1b605d019, 0x923f82a4af194f9b, 0xab1c5ed5da6d8118,
            0xd807aa98a3030242, 0x12835b0145706fbe, 0x243185be4ee4b28c, 0x550c7dc3d5ffb4e2,
            0x72be5d74f27b896f, 0x80deb1fe3b1696b1, 0x9bdc06a725c71235, 0xc19bf174cf692694,
            0xe49b69c19ef14ad2, 0xefbe4786384f25e3, 0x0fc19dc68b8cd5b5, 0x240ca1cc77ac9c65,
            0x2de92c6f592b0275, 0x4a7484aa6ea6e483, 0x5cb0a9dcbd41fbd4, 0x76f988da831153b5,
            0x983e5152ee66dfab, 0xa831c66d2db43210, 0xb00327c898fb213f, 0xbf597fc7beef0ee4,
            0xc6e00bf33da88fc2, 0xd5a79147930aa725, 0x06ca6351e003826f, 0x142929670a0e6e70,
            0x27b70a8546d22ffc, 0x2e1b21385c26c926, 0x4d2c6dfc5ac42aed, 0x53380d139d95b3df,
            0x650a73548baf63de, 0x766a0abb3c77b2a8, 0x81c2c92e47edaee6, 0x92722c851482353b,
            0xa2bfe8a14cf10364, 0xa81a664bbc423001, 0xc24b8b70d0f89791, 0xc76c51a30654be30,
            0xd192e819d6ef5218, 0xd69906245565a910, 0xf40e35855771202a, 0x106aa07032bbd1b8,
            0x19a4c116b8d2d0c8, 0x1e376c085141ab53, 0x2748774cdf8eeb99, 0x34b0bcb5e19b48a8,
            0x391c0cb3c5c95a63, 0x4ed8aa4ae3418acb, 0x5b9cca4f7763e373, 0x682e6ff3d6b2b8a3,
            0x748f82ee5defb2fc, 0x78a5636f43172f60, 0x84c87814a1f0ab72, 0x8cc702081a6439ec,
            0x90befffa23631e28, 0xa4506cebde82bde9, 0xbef9a3f7b2c67915, 0xc67178f2e372532b,
            0xca273eceea26619c, 0xd186b8c721c0c207, 0xeada7dd6cde0eb1e, 0xf57d4f7fee6ed178,
            0x06f067aa72176fba, 0x0a637dc5a2c898a6, 0x113f9804bef90dae, 0x1b710b35131c471b,
            0x28db77f523047d84, 0x32caab7b40c72493, 0x3c9ebe0a15c9bebc, 0x431d67c49c100d4c,
            0x4cc5d4becb3e42b6, 0x597f299cfc657e2a, 0x5fcb6fab3ad6faec, 0x6c44198c4a475817
        };

        private static readonly ulong[] HashArrary =
        {
            0x6a09e667f3bcc908, 0xbb67ae8584caa73b, 0x3c6ef372fe94f82b, 0xa54ff53a5f1d36f1,
            0x510e527fade682d1, 0x9b05688c2b3e6c1f, 0x1f83d9abfb41bd6b, 0x5be0cd19137e2179
        };

        public static string Hash(string msg)
        {
            ulong[] HashArr = DeepCopy(HashArrary);
            ulong[] HexArr = DeepCopy(HexArrary);

            msg += (char)0x80;
            long l = msg.Length / 8 + 2;
            long N = (long)Math.Ceiling((l / 16.0));
            ulong[][] M = new ulong[N][];

            for (int i = 0; i < N; i++)
            {
                M[i] = new ulong[16];
                for (int j = 0; j < 16; j++)
                { // encode 8 chars per uint64 (128 per block)
                    char hi = (char)(MesssageFloor(msg, (i * 128 + j * 8 + 0), 24) | MesssageFloor(msg, (i * 128 + j * 8 + 1), 16)
                             | MesssageFloor(msg, (i * 128 + j * 8 + 2), 8) | MesssageFloor(msg, (i * 128 + j * 8 + 3), 0));
                    char lo = (char)(MesssageFloor(msg, (i * 128 + j * 8 + 4), 24) | MesssageFloor(msg, (i * 128 + j * 8 + 5), 16)
                             | MesssageFloor(msg, (i * 128 + j * 8 + 6), 8) | MesssageFloor(msg, (i * 128 + j * 8 + 7), 0));
                    ulong high = Convert.ToUInt64((((ulong)hi).ToString() + "00000000"));
                    M[i][j] = high + (ulong)lo;
                }
            }

            M[N - 1][14] = 0x0000000000000000;
            char lenHi = (char)(int)(((msg.Length - 1) * 8) / Math.Pow(2, 32));
            char lenLo = (char)((msg.Length - 1) * 8);

            ulong lenHigh = Convert.ToUInt64((((ulong)lenHi).ToString("X") + "00000000"));
            M[N - 1][15] = lenHigh + (ulong)lenLo;

            for (int i = 0; i < N; i++)
            {
                ulong[] W = new ulong[80];

                // 1 - prepare message schedule 'W'
                for (int t = 0; t < 16; t++) W[t] = M[i][t];
                for (int t = 16; t < 80; t++)
                {
                    W[t] = W[t - 2] + W[t - 7] + σ0(W[t - 15]) +(W[t - 16]);
                }
        

                // 2 - initialise working variables a, b, c, d, e, f, g, h with previous hash value
                ulong a = HashArr[0], b = HashArr[1], c = HashArr[2], d = HashArr[3], e = HashArr[4], f = HashArr[5], g = HashArr[6], h = HashArr[7];

                // 3 - main loop (note 'addition modulo 2^64')
                for (int t = 0; t < 80; t++)
                {
                    ulong T1 = h + (Σ1(e)) + (Ch(e, f, g)) + (HexArr[t])+ (W[t]);
                    ulong T2 = Σ0(a) + (Maj(a, b, c));
                    h = g;
                    g = f;
                    f = e;
                    e = d + T1;
                    d = c;
                    c = b;
                    b = a;
                    a = T1 + T2;
                }

                // 4 - compute the new intermediate hash value
                HashArr[0] = HashArr[0] + (a);
                HashArr[1] = HashArr[1] + (b);
                HashArr[2] = HashArr[2] + (c);
                HashArr[3] = HashArr[3] + (d);
                HashArr[4] = HashArr[4] + (e);
                HashArr[5] = HashArr[5] + (f);
                HashArr[6] = HashArr[6] + (g);
                HashArr[7] = HashArr[7] + (h);
            }

            var outArrary = new string[HashArr.Length];

            for (int h = 0; h < HashArr.Length; h++) outArrary[h] = HashArr[h].ToString();

            // concatenate H0..H7, with separator if required

            string output = "";

            foreach(string str in outArrary)
            {
                output += str;
            }

            return output;
        }

        private static ulong[] DeepCopy(ulong[] Arrary)
        {
            var outputArr = new List<ulong>();
            foreach(ulong ul in Arrary)
            {
                outputArr.Add(ul);
            }

            return outputArr.ToArray();
        }

        private static int MesssageFloor(string msg, int pos, int floor)
        {
            try
            {
                return (msg[pos]) << floor;
            }
            catch
            {
                return 0;
            }
        }


        private static ulong Σ0(ulong x) { return xor(xor(ROTR(x, 28), (ROTR(x, 34))),(ROTR(x, 39))); }
        private static ulong Σ1(ulong x) { return xor(xor(ROTR(x, 14), (ROTR(x, 18))), (ROTR(x, 41))); }
        private static ulong σ0(ulong x) { return xor(xor(ROTR(x, 1), (ROTR(x, 8))), (shr(x, 7))); }
        private static ulong σ1(ulong x) { return xor(xor(ROTR(x, 19), (ROTR(x, 61))), (shr(x,(6)))); }
        private static ulong Ch(ulong x, ulong y, ulong z) { return (xor(x & (y), not(x) & (z))); }         // 'choice'
        private static ulong Maj(ulong x, ulong y, ulong z) { return (xor(xor(x & (y), x & (z)), y & (z))); } // 'majority'



        private static ulong ROTR(ulong x, int n)
        {
            if (n == 0) return x;

            var xStr = x.ToString("X");

            string add = "";
            for (int i = 0; i < 16 - xStr.Length; i++) add += "0";
            xStr = add + xStr;

            string hiT = xStr.Substring(0,8);
            string loT = xStr.Substring(8,8);

            if (n == 32) return Convert.ToUInt64(loT + "00000000", 16) + Convert.ToUInt64(hiT, 16);

            ulong hi = Convert.ToUInt64(hiT, 16), lo = Convert.ToUInt64(loT, 16);

            if (n > 32)
            {
                lo = hi; // swap hi/lo
                hi = Convert.ToUInt64(loT);
                n -= 32;
            }


            ulong hi1 = Convert.ToUInt64((((ulong)(hi >> n) | (lo << (32 - n))).ToString("X") + "00000000"), 16);
            ulong lo1 = (lo >> n) | (hi << (32 - n));

            return (hi1 + lo1);
        }

        private static ulong shr(ulong x, int n)
        {
            var xStr = x.ToString("X");

            string add = "";
            for (int i = 0; i < 16 - xStr.Length; i++) add += "0";
            xStr = add + xStr;

            string hiT = xStr;
            string loT = xStr;

            ulong hi = Convert.ToUInt64(hiT), lo = Convert.ToUInt64(loT);

            if (n == 0) return x;
            if (n == 32) return (0 + hi);
            if (n > 32) return (0 + hi >> n - 32);
            /* n < 32 */
            return Convert.ToUInt64("0x" + (hi >> n).ToString("X") + "00000000") + ( lo >> n | hi << (32 - n));
        }

        private static ulong xor(ulong part1, ulong part2)
        {
            return (part1 ^ part2);
        }

        private static ulong not(ulong x)
        {
            return ~x;
        }
    }
}