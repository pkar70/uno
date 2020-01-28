#if __ANDROID__
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Windows.ApplicationModel.Contacts
{
	public partial class ContactQueryOptions
	{
		internal ContactQuerySearchFields _whereToSearch;
		internal string _whatToSearch;

		public ContactQueryDesiredFields DesiredFields { get; set; }

		public ContactQueryOptions(string text) 
		{
			_whatToSearch = text;
			_whereToSearch = ContactQuerySearchFields.All;
		}

		public ContactQueryOptions(string text, ContactQuerySearchFields fields)
		{
			_whatToSearch = text;
			_whereToSearch = fields;
		}

		public ContactQueryOptions()
		{
			_whatToSearch = "";
			_whereToSearch = ContactQuerySearchFields.None;
		}

	}
}

#endif
