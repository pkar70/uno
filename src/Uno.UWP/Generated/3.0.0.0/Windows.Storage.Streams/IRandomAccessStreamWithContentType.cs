#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Storage.Streams
{
	#if __ANDROID__ || __IOS__ || NET461 || __WASM__ || __SKIA__ || __NETSTD_REFERENCE__ || __MACOS__
	[global::Uno.NotImplemented]
	#endif
	public  partial interface IRandomAccessStreamWithContentType : global::Windows.Storage.Streams.IRandomAccessStream,global::System.IDisposable,global::Windows.Storage.Streams.IInputStream,global::Windows.Storage.Streams.IOutputStream,global::Windows.Storage.Streams.IContentTypeProvider
	{
	}
}
