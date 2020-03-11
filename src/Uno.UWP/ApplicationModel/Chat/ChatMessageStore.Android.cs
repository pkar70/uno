using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Windows.ApplicationModel.Chat
{
	public partial class ChatMessageStore
	{
		public ChatMessageReader GetMessageReader() => new ChatMessageReader(new TimeSpan(36500, 0, 0, 0));
		public ChatMessageReader GetMessageReader(TimeSpan recentTimeLimit) => new ChatMessageReader(recentTimeLimit);
		public IAsyncAction SaveMessageAsync(Windows.ApplicationModel.Chat.ChatMessage chatMessage)
			=> SaveMessageAsyncTask(chatMessage).AsAsyncAction();

		private async Task SaveMessageAsyncTask(Windows.ApplicationModel.Chat.ChatMessage chatMessage)
		{
			// 1. maybe test permission (for write)?
			var currSmsApp = Android.Provider.Telephony.Sms.GetDefaultSmsPackage(Android.App.Application.Context);
			if (currSmsApp != Android.App.Application.Context.PackageName)
			{
				throw new UnauthorizedAccessException("ChatMessageStore: only app selected as default SMS app can write do SMS store");
			}

			// 2. new SMS
			Android.Content.ContentValues newSMS = new Android.Content.ContentValues();

			newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Body, chatMessage.Body);

			if (chatMessage.IsIncoming)
			{
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Type, 1);
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Read, chatMessage.IsRead ? 1 : 0);
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Seen, chatMessage.IsSeen ? 1 : 0);
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Status, (int)Android.Provider.SmsStatus.None);
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Address, chatMessage.From);

			}
			else
			{
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Type, 4);
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Read, 1);
				newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Seen, 1);

				if (chatMessage.Recipients.Count > 0)
				{
					newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Address, chatMessage.Recipients.ElementAt(0));
				}

				switch (chatMessage.Status)
				{
					case Windows.ApplicationModel.Chat.ChatMessageStatus.ReceiveDownloadFailed:
					case Windows.ApplicationModel.Chat.ChatMessageStatus.SendFailed:
						newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Status, (int)Android.Provider.SmsStatus.Failed);
						break;
					default:
						newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Status, (int)Android.Provider.SmsStatus.Pending);
						break;
				}
			}

			long msecs = chatMessage.LocalTimestamp.ToUnixTimeMilliseconds();
			newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.Date, msecs);
			msecs = chatMessage.NetworkTimestamp.ToUnixTimeMilliseconds();
			newSMS.Put(Android.Provider.Telephony.TextBasedSmsColumns.DateSent, msecs);

			// 3. insert into Uri
			Android.Content.ContentResolver cr = Android.App.Application.Context.ContentResolver;
			var retVal = cr.Insert(Android.Provider.Telephony.Sms.ContentUri, newSMS);
			if (retVal is null)
			{
				// Android reports error, but how to deal with this? UWP API doesn't have similar error...
				// (and, on UWP Desktop, inserting SMS doesn't fail, but SMS is not inserted into ChatStore)
			}
		}
	}

}
