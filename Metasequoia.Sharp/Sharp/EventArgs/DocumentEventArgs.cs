namespace Metasequoia.Sharp
{
	public class DocumentEventArgs : MetasequoiaEventArgs
	{
		public XmlElement Element
		{
			get
			{
				return MetasequoiaEventArgs.ExtractEventOption(this.Option, "xml_elem");
			}
		}

		public bool SaveUniqueId
		{
			get;
			set;
		}

		public DocumentEventArgs(MetasequoiaEventArgs e)
			: base(e)
		{
		}
	}
}