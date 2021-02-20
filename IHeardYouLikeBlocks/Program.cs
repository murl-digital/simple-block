using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IHeardYouLikeBlocks
{
    class Program
    {
        // i had to write this by hand. i am very sad.
        private static Dictionary<(int, int), int[]> sBox = new Dictionary<(int, int), int[]>
        {
            {(GetHashCode(new[]{0, 0, 0, 0}), GetHashCode(new[]{0, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 0, 0, 0}), GetHashCode(new[]{0, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 0, 0, 0}), GetHashCode(new[]{1, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 0, 0, 0}), GetHashCode(new[]{1, 1})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 0, 0, 1}), GetHashCode(new[]{0, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 0, 0, 1}), GetHashCode(new[]{0, 1})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 0, 0, 1}), GetHashCode(new[]{1, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 0, 0, 1}), GetHashCode(new[]{1, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 0, 1, 0}), GetHashCode(new[]{0, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 0, 1, 0}), GetHashCode(new[]{0, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 0, 1, 0}), GetHashCode(new[]{1, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{0, 0, 1, 0}), GetHashCode(new[]{1, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 0, 1, 1}), GetHashCode(new[]{0, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{0, 0, 1, 1}), GetHashCode(new[]{0, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 0, 1, 1}), GetHashCode(new[]{1, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 0, 1, 1}), GetHashCode(new[]{1, 1})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 1, 0, 0}), GetHashCode(new[]{0, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 1, 0, 0}), GetHashCode(new[]{0, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 1, 0, 0}), GetHashCode(new[]{1, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 1, 0, 0}), GetHashCode(new[]{1, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{0, 1, 0, 1}), GetHashCode(new[]{0, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 1, 0, 1}), GetHashCode(new[]{0, 1})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 1, 0, 1}), GetHashCode(new[]{1, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{0, 1, 0, 1}), GetHashCode(new[]{1, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 1, 1, 0}), GetHashCode(new[]{0, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 1, 1, 0}), GetHashCode(new[]{0, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{0, 1, 1, 0}), GetHashCode(new[]{1, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{0, 1, 1, 0}), GetHashCode(new[]{1, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 1, 1, 1}), GetHashCode(new[]{0, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{0, 1, 1, 1}), GetHashCode(new[]{0, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{0, 1, 1, 1}), GetHashCode(new[]{1, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{0, 1, 1, 1}), GetHashCode(new[]{1, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 0, 0, 0}), GetHashCode(new[]{0, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 0, 0, 0}), GetHashCode(new[]{0, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 0, 0, 0}), GetHashCode(new[]{1, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{1, 0, 0, 0}), GetHashCode(new[]{1, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{1, 0, 0, 1}), GetHashCode(new[]{0, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 0, 0, 1}), GetHashCode(new[]{0, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 0, 0, 1}), GetHashCode(new[]{1, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 0, 0, 1}), GetHashCode(new[]{1, 1})), new[]{1, 1}},
            {(GetHashCode(new[]{1, 0, 1, 0}), GetHashCode(new[]{0, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{1, 0, 1, 0}), GetHashCode(new[]{0, 1})), new[]{1, 1}},
            {(GetHashCode(new[]{1, 0, 1, 0}), GetHashCode(new[]{1, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 0, 1, 0}), GetHashCode(new[]{1, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 0, 1, 1}), GetHashCode(new[]{0, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{1, 0, 1, 1}), GetHashCode(new[]{0, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{1, 0, 1, 1}), GetHashCode(new[]{1, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 0, 1, 1}), GetHashCode(new[]{1, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 1, 0, 0}), GetHashCode(new[]{0, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 1, 0, 0}), GetHashCode(new[]{0, 1})), new[]{1, 1}},
            {(GetHashCode(new[]{1, 1, 0, 0}), GetHashCode(new[]{1, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{1, 1, 0, 0}), GetHashCode(new[]{1, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{1, 1, 0, 1}), GetHashCode(new[]{0, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 1, 0, 1}), GetHashCode(new[]{0, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 1, 0, 1}), GetHashCode(new[]{1, 0})), new[]{1, 1}},
            {(GetHashCode(new[]{1, 1, 0, 1}), GetHashCode(new[]{1, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 1, 1, 0}), GetHashCode(new[]{0, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{1, 1, 1, 0}), GetHashCode(new[]{0, 1})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 1, 1, 0}), GetHashCode(new[]{1, 0})), new[]{0, 0}},
            {(GetHashCode(new[]{1, 1, 1, 0}), GetHashCode(new[]{1, 1})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 1, 1, 1}), GetHashCode(new[]{0, 0})), new[]{0, 1}},
            {(GetHashCode(new[]{1, 1, 1, 1}), GetHashCode(new[]{0, 1})), new[]{1, 0}},
            {(GetHashCode(new[]{1, 1, 1, 1}), GetHashCode(new[]{1, 0})), new[]{1, 0}},
            {(GetHashCode(new[]{1, 1, 1, 1}), GetHashCode(new[]{1, 1})), new[]{1, 1}},
        };
        
        static void Main(string[] args)
        {
            var encryptedString = Crypt("testing!");
            Console.WriteLine(encryptedString);
            var decryptedString = Crypt(encryptedString);
            Console.WriteLine(decryptedString);
        }

        private static string Crypt(string input)
        {
            // this part is basically converting the input into binary, and spitting it into 4 bit chunks
            var inArray = ToBinary(ConvertToByteArray(input, Encoding.ASCII));

            var split = inArray.Select((item, index) => new
                {
                    index,
                    item
                })
                .GroupBy(x => x.index / 4)
                .Select(x => x.Select(y => y.item).ToArray()).ToArray();

            var encrypted = SBlock(split, new[]
            {
                1,
                1,
                1,
                0
            });

            // after the message gets block'd, the string is reconstructed here
            var strings = new List<string>();

            for (int i = 0; i < encrypted.Length - 1; i += 2)
            {
                strings.Add(string.Concat(encrypted[i].Concat(encrypted[i + 1].ToArray())));
            }

            return Encoding.ASCII.GetString(strings.Select(s => Convert.ToByte(s, 2)).ToArray());
        }

        // unfortunately, my first attempt at the block is broken, and im not sure why.
        // my best guess is because i did the s-box wrong (if you want feel free to double check starting at line 11, but i wouldn't)
        // failing that, perhaps im doing the subsitution wrong, but ill address that later.
        static int[][] SBlock(int[][] input, int[] key)
        {
            var result = new List<int[]>();
            foreach (var i in input)
            {
                result.Add(Muddle(i, key));   
            }

            return result.ToArray();
        }

        private static int[] Muddle(int[] chunk, int[] key)
        {
            var buffer = new int[4];
            Array.Copy(chunk, buffer, 4);
            for (var i = 0; i < 2; i++)
            {
                Sub(key, buffer);

                LeftShiftArray(key, 1);
                var swapBuffer = new int[4];
                Array.Copy(buffer, swapBuffer, 4);
                buffer[0] = swapBuffer[2];
                buffer[1] = swapBuffer[3];
                buffer[2] = swapBuffer[0];
                buffer[3] = swapBuffer[1];
            }
            
            Sub(key, buffer);

            return buffer;
        }

        private static void Sub(int[] key, int[] buffer)
        {
            Console.WriteLine(string.Concat(key));
            var sub = sBox[(GetHashCode(key), GetHashCode(new []{buffer[2], buffer[3]}))];
            buffer[0] = buffer[0] ^ sub[0];
            buffer[1] = buffer[1] ^ sub[1];
        }

        public static void LeftShiftArray<T>(T[] arr, int shift)
        {
            shift = shift % arr.Length;
            T[] buffer = new T[shift];
            Array.Copy(arr, buffer, shift);
            Array.Copy(arr, shift, arr, 0, arr.Length - shift);
            Array.Copy(buffer, 0, arr, arr.Length - shift, shift);
        }

        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static int[] ToBinary(Byte[] data)
        {
            var bits = data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0'));

            return bits
                .SelectMany(bit => bit.ToCharArray(), (_, c) => int.Parse(c.ToString())).ToArray();
        }
        
        static int GetHashCode(int[] values)
        {
            int result = 0;
            int shift = 0;
            for (int i = 0; i < values.Length; i++)
            {
                shift = (shift + 11) % 21;
                result ^= (values[i]+1024) << shift;
            }
            return result;
        }
    }
}