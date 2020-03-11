#if __ANDROID__
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.ApplicationModel.Chat
{
	public partial class ChatMessage : IChatItem
	{
		public bool IsRead { get; set; }
		public DateTimeOffset NetworkTimestamp { get; set; }
		public bool IsSeen { get; set; }

	}
}

#endif
