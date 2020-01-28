﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Windows.ApplicationModel.Contacts
{

	public partial class ContactBatch
	{

		public IReadOnlyList<Contact> Contacts {get; }

#if __ANDROID__
		internal ContactBatch(IReadOnlyList<Contact> contacts)
		{
			Contacts = contacts;
		}
#endif

	}
}

