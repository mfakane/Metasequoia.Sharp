using System;

namespace Metasequoia.Sharp
{
	public class UserMessageEventArgs : MetasequoiaEventArgs
	{
		/// <summary>
		/// 送出元プラグインのプロダクト ID
		/// src_product
		/// </summary>
		public uint VendorId
		{
			get
			{
				return (uint)MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "src_product", 0);
			}
		}

		/// <summary>
		/// 送出元プラグインのプラグイン ID
		/// src_id
		/// </summary>
		public uint ProductId
		{
			get
			{
				return (uint)MetasequoiaEventArgs.ExtractEventOptionInt(this.Option, "src_id", 0);
			}
		}

		/// <summary>
		/// メッセージの内容を表す任意の文字列
		/// description
		/// </summary>
		public unsafe string Description
		{
			get
			{
				return new string((char*)MetasequoiaEventArgs.ExtractEventOption(this.Option, "description"));
			}
		}

		/// <summary>
		/// メッセージの内容
		/// message
		/// </summary>
		public IntPtr Message
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOption(this.Option, "message");
			}
		}

		/// <summary>
		/// 送出元プラグインに返す任意の値
		/// </summary>
		public unsafe int Result
		{
			get
			{
				return *(int*)MetasequoiaEventArgs.ExtractEventOption(this.Option, "result");
			}
			set
			{
				*(int*)MetasequoiaEventArgs.ExtractEventOption(this.Option, "result") = value;
			}
		}

		public UserMessageEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}
