#if __ANDROID__

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;


namespace Windows.ApplicationModel.Contacts
{
	public partial class ContactManager
	{

		public static IAsyncOperation<ContactStore> RequestStoreAsync() => RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);

		public static IAsyncOperation<ContactStore> RequestStoreAsync(ContactStoreAccessType accessType) => RequestStoreAsyncTask(accessType).AsAsyncOperation<ContactStore>();

		private static async Task<ContactStore> RequestStoreAsyncTask(ContactStoreAccessType accessType)
		{
			// UWP: AppContactsReadWrite, AllContactsReadOnly, AllContactsReadWrite (cannot be used without special provisioning by Microsoft)
			// Android: Manifest has READ_CONTACTS and WRITE_CONTACTS, no difference between app/limited/full
			// using only AllContactsReadOnly, other: throw NotImplementedException (maybe someone else implement Write functionality)

			if (accessType != ContactStoreAccessType.AllContactsReadOnly)
			{
				throw new NotImplementedException();
			}

			// do we have declared this permission in Manifest?
			Android.Content.Context context = Android.App.Application.Context;
			Android.Content.PM.PackageInfo packageInfo =
				context.PackageManager.GetPackageInfo(context.PackageName, Android.Content.PM.PackageInfoFlags.Permissions);
			var requestedPermissions = packageInfo?.RequestedPermissions;
			if (requestedPermissions is null)
			{
				throw new UnauthorizedAccessException("no ReadContacts permission in Manifest defined (no permission at all)");
			}

			bool bInManifest = requestedPermissions.Any(p => p.Equals(Android.Manifest.Permission.ReadContacts, StringComparison.OrdinalIgnoreCase));
			if (!bInManifest)
			{
				throw new UnauthorizedAccessException("no ReadContacts permission in Manifest defined");
			}

			if(await Windows.Extensions.PermissionsHelper.CheckPermission(CancellationToken.None, Android.Manifest.Permission.ReadContacts)
				|| await Windows.Extensions.PermissionsHelper.TryGetPermission(CancellationToken.None, Android.Manifest.Permission.ReadContacts))
			{
				return new ContactStore();
			}

			return null;

		}
	}
}
#endif 
