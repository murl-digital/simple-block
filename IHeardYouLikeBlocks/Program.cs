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

        public delegate int[][] Encryptor(int[][] input, int[] key, bool decrypt);
        
        static void Main(string[] args)
        {
            var key = new[]
            {
                1,
                1,
                1,
                0
            };
            
            var encryptedString = Crypt("testing!", key, SBlock);
            Console.WriteLine(encryptedString);
            var decryptedString = Crypt(encryptedString, key, SBlock, true);
            Console.WriteLine(decryptedString);

            var cbcEncryptedString = Crypt("testing!", key, SBlockCBC);
            Console.WriteLine(cbcEncryptedString);
            var cbcDecryptedString = Crypt(cbcEncryptedString, key, SBlockCBC, true);
            Console.WriteLine(cbcDecryptedString);
        }

        private static string Crypt(string input, int[] key, Encryptor encryptor, bool decrypt = false)
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

            var encrypted = encryptor(split, key, decrypt);

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
        private static int[][] SBlock(int[][] input, int[] key, bool decrypt)
        {
            var result = new List<int[]>();
            foreach (var i in input)
            {
                var chunkKey = new int[4];
                Array.Copy(key, chunkKey, 4);
                result.Add(decrypt ? UnMuddle(i, chunkKey) : Muddle(i, chunkKey));   
            }

            return result.ToArray();
        }
        
        private static int[][] SBlockCBC(int[][] input, int[] key, bool decrypt)
        {
            var result = new List<int[]>();
            var random = new Random(GetHashCode(key));
            var xor = new int[4];
            for (var i = 0; i < xor.Length; i++)
            {
                xor[i] = random.Next(0, 2);
            }

            if (decrypt)
            {
                /*for (int i = input.Length - 1; i >= 0; i--)
                {
                    var chunkKey = new int[4];
                    Array.Copy(key, chunkKey, 4);
                    if (i == 0)
                    {
                        for (var j = 0; j < xor.Length; j++)
                        {
                            xor[j] = random.Next(0, 2);
                        }
                    }
                    else
                    {
                        Array.Copy(input[i-1], xor, 4);
                    }

                    var chunk = UnMuddle(input[i], chunkKey);
                    for (int j = 0; j < chunk.Length; j++)
                    {
                        chunk[j] = chunk[j] ^ xor[j];
                    }
                    result.Insert(0, chunk);
                }*/
                foreach (var i in input)
                {
                    var chunkKey = new int[4];
                    Array.Copy(key, chunkKey, 4);
                    var chunk = UnMuddle(i, chunkKey);
                    for (int j = 0; j < i.Length; j++)
                    {
                        chunk[j] = chunk[j] ^ xor[j];
                    }
                    result.Add(chunk);
                    Array.Copy(i, xor, 4);
                }
            }
            else
            {
                foreach (var i in input)
                {
                    var chunkKey = new int[4];
                    Array.Copy(key, chunkKey, 4);
                    for (int j = 0; j < i.Length; j++)
                    {
                        i[j] = i[j] ^ xor[j];
                    }

                    var chunk = Muddle(i, chunkKey);
                    result.Add(chunk);
                    Array.Copy(chunk, xor, 4);
                }
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

                RotateLeft(key, 1);
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
        
        private static int[] UnMuddle(int[] chunk, int[] key)
        {
            var buffer = new int[4];
            Array.Copy(chunk, buffer, 4);
            RotateLeft(key, 2);
            for (var i = 0; i < 2; i++)
            {
                Sub(key, buffer);

                RotateRight(key, 1);
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
            var sub = sBox[(GetHashCode(key), GetHashCode(new []{buffer[2], buffer[3]}))];
            buffer[0] = buffer[0] ^ sub[0];
            buffer[1] = buffer[1] ^ sub[1];
        }

        private static void RotateLeft(int[] array, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var buffer = new int[3];
                Array.Copy(array, 1, buffer, 0, 3);
                array[3] = array[0];

                array[0] = buffer[0];
                array[1] = buffer[1];
                array[2] = buffer[2];
            }
        }

        private static void RotateRight(int[] array, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var buffer = new int[3];
                Array.Copy(array, 0, buffer, 0, 3);
                array[0] = array[3];

                array[1] = buffer[0];
                array[2] = buffer[1];
                array[3] = buffer[2];
            }
        }

        private static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        private static int[] ToBinary(Byte[] data)
        {
            var bits = data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0'));

            return bits
                .SelectMany(bit => bit.ToCharArray(), (_, c) => int.Parse(c.ToString())).ToArray();
        }
        
        private static int GetHashCode(int[] values)
        {
            var result = 0;
            var shift = 0;
            foreach (var t in values)
            {
                shift = (shift + 11) % 21;
                result ^= (t+1024) << shift;
            }
            return result;
        }
    }
}