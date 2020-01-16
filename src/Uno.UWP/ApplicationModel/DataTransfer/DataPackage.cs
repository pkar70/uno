using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.ApplicationModel.DataTransfer
{
	public partial class DataPackage
	{
		public  global::Windows.ApplicationModel.DataTransfer.DataPackageOperation RequestedOperation { get; set; }
		internal string Text { get; private set; }
		internal string Html { get; private set; }

		public void SetText(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("Text can't be null");
			}

			this.Text = text;
		}

		public  void SetHtmlFormat( string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("Value can't be null");
			}

			this.Html = value;
		}

	}
}
