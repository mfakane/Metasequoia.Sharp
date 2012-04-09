using System.Runtime.InteropServices;
using System.Text;

namespace Metasequoia
{
	/// <summary>
	/// MQSetting
	/// </summary>
	public class Setting
	{
		[DllImport("kernel32")]
		static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);
		[DllImport("kernel32", EntryPoint = "GetPrivateProfileStringA", CharSet = CharSet.Ansi)]
		static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);
		[DllImport("kernel32", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi)]
		static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

		const int SingleBufferSize = 64;
		const int StringBufferSize = 2048;

		public string FileName
		{
			get;
			private set;
		}

		public string SectionName
		{
			get;
			private set;
		}

		public Setting(string fileName, string sectionName)
		{
			this.FileName = fileName;
			this.SectionName = sectionName;
		}

		public bool LoadBoolean(string name, bool defaultValue)
		{
			return GetPrivateProfileInt(this.SectionName, name, defaultValue ? 1 : 0, this.FileName) != 0;
		}

		public int LoadInt32(string name, int defaultValue)
		{
			return (int)GetPrivateProfileInt(this.SectionName, name, defaultValue, this.FileName);
		}

		public uint LoadUInt32(string name, uint defaultValue)
		{
			return GetPrivateProfileInt(this.SectionName, name, (int)defaultValue, this.FileName);
		}

		public float LoadSingle(string name, float defaultValue)
		{
			var sb = new StringBuilder(SingleBufferSize);

			GetPrivateProfileString(this.SectionName, name, defaultValue.ToString(), sb, (uint)sb.Capacity, this.FileName);

			return float.Parse(sb.ToString());
		}

		public string LoadString(string name, string defaultValue)
		{
			var sb = new StringBuilder(StringBufferSize);

			GetPrivateProfileString(this.SectionName, name, defaultValue.ToString(), sb, (uint)sb.Capacity, this.FileName);

			return sb.ToString();
		}

		public void Save(string name, bool value)
		{
			WritePrivateProfileString(this.SectionName, name, value ? "1" : "0", this.FileName);
		}

		public void Save(string name, int value)
		{
			WritePrivateProfileString(this.SectionName, name, value.ToString(), this.FileName);
		}

		public void Save(string name, uint value)
		{
			WritePrivateProfileString(this.SectionName, name, value.ToString(), this.FileName);
		}

		public void Save(string name, float value)
		{
			WritePrivateProfileString(this.SectionName, name, value.ToString(), this.FileName);
		}

		public void Save(string name, string value)
		{
			WritePrivateProfileString(this.SectionName, name, value, this.FileName);
		}
	}
}
