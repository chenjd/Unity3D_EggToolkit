/// <summary>
/// EggBaseToolkit
/// Created by chenjd
/// http://www.cnblogs.com/murongxiaopifu/
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

using UnityEngine;

namespace EggToolkit
{
	public static class EggBase{

		public static void LogRed(object msg)
		{
			Debug.Log("<color=red>" + msg + "</color>");
		}

		public static void LogGreen(object msg)
		{
			Debug.Log("<color=Green>" + msg + "</color>");
		}

		public static void LogBlue(object msg)
		{
			Debug.Log("<color=Blue>" + msg + "</color>");
		}

		public static string Time2Str(float sec)
		{
			int inital = (int)sec;
			int hour = inital / 3600;
			int min = inital % 3600 / 60;
			int second = inital % 3600 % 60;
			string strHour = hour.ToString() + "小时";
			string strMin = min.ToString() + "分";
			string strSec = second.ToString() + "秒";
			string text = string.Empty;
			if (hour != 0)
			{
				text += strHour;
			}
			if (min != 0)
			{
				text += strMin;
			}
			if (second != 0)
			{
				text += strSec;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "0秒";
			}
			return text;
		}

		public static string GetStandardDateTimeString(this DateTime dateTime)
		{
			return dateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
		}

		public static string ParseSecondsToTime(int second)
		{
			int num = second % 60;
			int num2 = second / 60 % 60;
			return string.Concat(new string[]
			                     {
				(second / 3600).ToString("d2"),
				":",
				num2.ToString("d2"),
				":",
				num.ToString("d2")
			});
		}

		public static string IntListToString(List<int> list)
		{
			string text = string.Empty;
			for (int i = 0; i < list.Count; i++)
			{
				text += list[i].ToString();
				if (i + 1 < list.Count)
				{
					text += ",";
				}
			}
			return text;
		}

		public static List<int> String2IntList(string str)
		{
			List<int> list = new List<int>();
			if (string.IsNullOrEmpty(str))
			{
				return list;
			}
			string[] array = str.Split(new char[]
			                              {
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string s = array2[i];
				list.Add(int.Parse(s));
			}
			return list;
		}

		public static bool isValidPassword(string password)
		{
			return Regex.IsMatch(password, "^[/a-zA-z]+[/0-9]+$");
		}
		public static bool isValidEmail(string mail)
		{
			return Regex.IsMatch(mail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
		}
		public static bool isValidAccount(string account)
		{
			return !string.IsNullOrEmpty(account);
		}
	}
}
