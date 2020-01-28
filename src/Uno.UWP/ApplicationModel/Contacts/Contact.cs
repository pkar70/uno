using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Uno.Logging;
using Microsoft.Extensions.Logging;
using Uno.Extensions;

namespace Windows.ApplicationModel.Contacts
{
	public partial class Contact
	{
		// public string Name { get; set } // can be unimplemented, see firstname (and lastname?)
		// https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.contacts.contact.name

		public IList<ContactEmail> Emails { get; internal set; }


		public IList<ContactAddress> Addresses { get; internal set; }

		public IList<ContactPhone> Phones { get; internal set; }

		private string _middleName;
		public string MiddleName
		{
			get => _middleName;
			set
			{
				_middleName = value;
				if (_middleName.Length > 64)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.Contact.MiddleName is set to string longer than UWP limit (64 chars)");
					}
				}
			}
		}

		private string _lastName;
		public string LastName
		{
			get => _lastName;
			set
			{
				_lastName = value;
				if (_lastName.Length > 64)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.Contact.LastName is set to string longer than UWP limit (64 chars)");
					}
				}
			}
		}


		private string _firstName;
		public string FirstName
		{
			get => _firstName;
			set
			{
				_firstName = value;
				if (_firstName.Length > 64)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.Contact.FirstName is set to string longer than UWP limit (64 chars)");
					}
				}
			}
		}


		private string _honorificNamePrefix;
		public string HonorificNamePrefix
		{
			get => _honorificNamePrefix;
			set
			{
				_honorificNamePrefix = value;
				if (_honorificNamePrefix.Length > 32)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.Contact.HonorificNamePrefix is set to string longer than UWP limit (32 chars)");
					}
				}
			}
		}

		private string _honorificNameSuffix;
		public string HonorificNameSuffix
		{
			get => _honorificNameSuffix;
			set
			{
				_honorificNameSuffix = value;
				if (_honorificNameSuffix.Length > 32)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.Contact.HonorificNameSuffix is set to string longer than UWP limit (32 chars)");
					}
				}
			}
		}


		public string DisplayName { get; internal set; }

		public Contact()
		{
			Emails = new List<ContactEmail>();
			Phones = new List<ContactPhone>();
			Addresses = new List<ContactAddress>();
		}

	}
}

