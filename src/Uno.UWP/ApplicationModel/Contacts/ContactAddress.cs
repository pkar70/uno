using Uno.Logging;
using Microsoft.Extensions.Logging;
using Uno.Extensions;

namespace Windows.ApplicationModel.Contacts
{
	public partial class ContactAddress
	{
		private string _country;
		public string Country
		{
			get => _country;
			set
			{
				_country = value;
				if (_country.Length > 1024)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.ContactAddress.Country is set to string longer than UWP limit (1024 chars)");
					}
				}
			}
		}

		public ContactAddressKind Kind { get; set; }
		private string _locality;
		public string Locality
		{
			get => _locality;
			set
			{
				_locality = value;
				if (_locality.Length > 1024)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.ContactAddress.Locality is set to string longer than UWP limit (1024 chars)");
					}
				}
			}
		}

		private string _postalCode;
		public string PostalCode
		{
			get => _postalCode;
			set
			{
				_postalCode = value;
				if (_postalCode.Length > 1024)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.ContactAddress.PostalCode is set to string longer than UWP limit (1024 chars)");
					}
				}
			}
		}

		private string _region;
		public string Region
		{
			get => _region;
			set
			{
				_region = value;
				if (_region.Length > 1024)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.ContactAddress.Region is set to string longer than UWP limit (1024 chars)");
					}
				}
			}
		}
		private string _streetAddress;
		public string StreetAddress
		{
			get => _streetAddress;
			set
			{
				_streetAddress = value;
				if (_streetAddress.Length > 1024)
				{
					if (this.Log().IsEnabled(LogLevel.Warning))
					{
						this.Log().LogWarning("Windows.ApplicationModel.Contacts.ContactAddress.StreetAddress is set to string longer than UWP limit (1024 chars)");
					}
				}
			}
		}
	}
}


