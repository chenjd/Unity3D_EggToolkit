/// <summary>
/// EggDeviceToolkit
/// Created by chenjd
/// http://www.cnblogs.com/murongxiaopifu/
/// </summary>
using UnityEngine;
using System.Collections;
namespace EggToolkit
{
	public static class EggDevice {

		public static string GetDeviceIdentifier()
		{
			return SystemInfo.deviceUniqueIdentifier;
		}
		public static string GetDeviceType()
		{
			string type = string.Empty;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				type = "Current OS is ios," + SystemInfo.operatingSystem + "," + SystemInfo.deviceModel;
			}
			else
			{
				type = "Current OS is android," + SystemInfo.operatingSystem + "," + SystemInfo.deviceModel;
			}
			return type;
		}

	}
}