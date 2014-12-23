/// <summary>
/// EggMathToolkit
/// Created by chenjd
/// http://www.cnblogs.com/murongxiaopifu/
/// </summary>
using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace EggToolkit
{
	public static class EggMath {
		public const float PI = 3.14f;
		public const float TwoPI = 6.28318548f;
		public const float ThreePI = 9.42f;
		public const float Rad2Deg = 57.3f;
		public const float Deg2Rad =0.017453f;

		public static bool IsFloatEqual(float f1, float f2)
		{
			return Mathf.Approximately(f1, f2);
		}

		public static float Degree2Radian(float deg)
		{
			return deg * Deg2Rad;
		}

		public static float Radian2Degree(float rad)
		{
			return rad * Rad2Deg;
		}

		public static int HexChar2Int(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'a' && c <= 'f')
			{
				return (int)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			return 0;
		}

		public static float LimitValue(float min, float value, float max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		public static string Base64Code(string Message)
		{
			char[] array = new char[]
			{
				'A',
				'B',
				'C',
				'D',
				'E',
				'F',
				'G',
				'H',
				'I',
				'J',
				'K',
				'L',
				'M',
				'N',
				'O',
				'P',
				'Q',
				'R',
				'S',
				'T',
				'U',
				'V',
				'W',
				'X',
				'Y',
				'Z',
				'a',
				'b',
				'c',
				'd',
				'e',
				'f',
				'g',
				'h',
				'i',
				'j',
				'k',
				'l',
				'm',
				'n',
				'o',
				'p',
				'q',
				'r',
				's',
				't',
				'u',
				'v',
				'w',
				'x',
				'y',
				'z',
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9',
				'+',
				'/',
				'='
			};
			byte b = 0;
			ArrayList arrayList = new ArrayList(Encoding.Default.GetBytes(Message));
			int count = arrayList.Count;
			int num = count / 3;
			int num2;
			if ((num2 = count % 3) > 0)
			{
				for (int i = 0; i < 3 - num2; i++)
				{
					arrayList.Add(b);
				}
				num++;
			}
			StringBuilder stringBuilder = new StringBuilder(num * 4);
			for (int j = 0; j < num; j++)
			{
				byte[] array2 = new byte[]
				{
					(byte)arrayList[j * 3],
					(byte)arrayList[j * 3 + 1],
					(byte)arrayList[j * 3 + 2]
				};
				int[] array3 = new int[4];
				array3[0] = array2[0] >> 2;
				array3[1] = ((int)(array2[0] & 3) << 4 ^ array2[1] >> 4);
				if (!array2[1].Equals(b))
				{
					array3[2] = ((int)(array2[1] & 15) << 2 ^ array2[2] >> 6);
				}
				else
				{
					array3[2] = 64;
				}
				if (!array2[2].Equals(b))
				{
					array3[3] = (int)(array2[2] & 63);
				}
				else
				{
					array3[3] = 64;
				}
				stringBuilder.Append(array[array3[0]]);
				stringBuilder.Append(array[array3[1]]);
				stringBuilder.Append(array[array3[2]]);
				stringBuilder.Append(array[array3[3]]);
			}
			return stringBuilder.ToString();
		}
		public static string Base64Decode(string Message)
		{
			if (Message.Length % 4 != 0)
			{
			}
			if (!Regex.IsMatch(Message, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
			{
			}
			string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
			int num = Message.Length / 4;
			ArrayList arrayList = new ArrayList(num * 3);
			char[] array = Message.ToCharArray();
			for (int i = 0; i < num; i++)
			{
				byte[] array2 = new byte[]
				{
					(byte)text.IndexOf(array[i * 4]),
					(byte)text.IndexOf(array[i * 4 + 1]),
					(byte)text.IndexOf(array[i * 4 + 2]),
					(byte)text.IndexOf(array[i * 4 + 3])
				};
				byte[] array3 = new byte[3];
				array3[0] = (byte)((int)array2[0] << 2 ^ (array2[1] & 48) >> 4);
				if (array2[2] != 64)
				{
					array3[1] = (byte)((int)array2[1] << 4 ^ (array2[2] & 60) >> 2);
				}
				else
				{
					array3[2] = 0;
				}
				if (array2[3] != 64)
				{
					array3[2] = (byte)((int)array2[2] << 6 ^ (int)array2[3]);
				}
				else
				{
					array3[2] = 0;
				}
				arrayList.Add(array3[0]);
				if (array3[1] != 0)
				{
					arrayList.Add(array3[1]);
				}
				if (array3[2] != 0)
				{
					arrayList.Add(array3[2]);
				}
			}
			byte[] bytes = (byte[])arrayList.ToArray(Type.GetType("System.Byte"));
			return Encoding.Default.GetString(bytes);
		}
	}
}
