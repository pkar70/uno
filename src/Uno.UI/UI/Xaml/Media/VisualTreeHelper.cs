﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uno.UI;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Uno.Extensions;
using Uno.Disposables;
using Windows.Globalization.DateTimeFormatting;
using Uno.UI.Extensions;

#if XAMARIN_IOS
using UIKit;
using _View = UIKit.UIView;
using _ViewGroup = UIKit.UIView;
#elif __MACOS__
using AppKit;
using _View = AppKit.NSView;
using _ViewGroup = AppKit.NSView;
#elif XAMARIN_ANDROID
using _ViewGroup = Android.Views.ViewGroup;
using _View = Android.Views.View;
#else
using _View = System.Object;
#endif

namespace Windows.UI.Xaml.Media
{
	public partial class VisualTreeHelper
	{
		private static List<WeakReference<IPopup>> _openPopups = new List<WeakReference<IPopup>>();

		internal static IDisposable RegisterOpenPopup(IPopup popup)
		{
			var weakPopup = new WeakReference<IPopup>(popup);

			_openPopups.AddDistinct(weakPopup);
			return Disposable.Create(() => _openPopups.Remove(weakPopup));
		}

		[Uno.NotImplemented]
		public static void DisconnectChildrenRecursive(UIElement element)
		{
			throw new NotSupportedException();
		}

		public static IEnumerable<UIElement> FindElementsInHostCoordinates(Point intersectingPoint, UIElement subtree)
			=> FindElementsInHostCoordinates(intersectingPoint, subtree, false);

		[Uno.NotImplemented]
		public static IEnumerable<UIElement> FindElementsInHostCoordinates(Rect intersectingRect, UIElement subtree)
		{
			throw new NotSupportedException();
		}

		public static IEnumerable<UIElement> FindElementsInHostCoordinates(Point intersectingPoint, UIElement subtree, bool includeAllElements)
		{
			if (subtree != null)
			{
				if (IsElementIntersecting(intersectingPoint, subtree))
				{
					yield return subtree;
				}

				foreach (var child in subtree.GetChildren().OfType<UIElement>())
				{
					var canTest = includeAllElements
						|| (child.IsHitTestVisible && child.IsViewHit());

					if (child is UIElement uiElement && canTest)
					{
						if (IsElementIntersecting(intersectingPoint, uiElement))
						{
							yield return uiElement;
						}

						foreach (var subChild in FindElementsInHostCoordinates(intersectingPoint, child, includeAllElements))
						{
							yield return subChild;
						}
					}
				}
			}
		}

		private static bool IsElementIntersecting(Point intersectingPoint, UIElement uiElement)
		{
			GeneralTransform transformToRoot = uiElement.TransformToVisual(null);
			var target = transformToRoot.TransformBounds(uiElement.LayoutSlot);
			return target.Contains(intersectingPoint);
		}

		[Uno.NotImplemented]
		public static IEnumerable<UIElement> FindElementsInHostCoordinates(Rect intersectingRect, UIElement subtree, bool includeAllElements)
		{
			throw new NotSupportedException();
		}

		public static DependencyObject GetChild(DependencyObject reference, int childIndex)
		{
#if XAMARIN
			return (reference as _ViewGroup)?
				.GetChildren()
				.OfType<DependencyObject>()
				.ElementAtOrDefault(childIndex);
#else
			return (reference as UIElement)?
				.GetChildren()
				.OfType<DependencyObject>()
				.ElementAtOrDefault(childIndex);
#endif
		}

		public static int GetChildrenCount(DependencyObject reference)
		{
#if XAMARIN
			return (reference as _ViewGroup)?
				.GetChildren()
				.OfType<DependencyObject>()
				.Count() ?? 0;
#else
			return (reference as UIElement)?
				.GetChildren()
				.OfType<DependencyObject>()
				.Count() ?? 0;
#endif
		}

		public static IReadOnlyList<Popup> GetOpenPopups(Window window)
		{
			return _openPopups
				.Select(WeakReferenceExtensions.GetTarget)
				.OfType<Popup>()
				.ToList()
				.AsReadOnly();
		}

		public static DependencyObject GetParent(DependencyObject reference)
		{
#if XAMARIN
			return (reference as _ViewGroup)?
				.FindFirstParent<DependencyObject>();
#else
			return reference.GetParent() as DependencyObject;
#endif
		}

		internal static void CloseAllPopups()
		{
			foreach (var popup in GetOpenPopups(Window.Current))
			{
				popup.IsOpen = false;
			}
		}

		/// <summary>
		/// Adapts a native view by wrapping it in a <see cref="FrameworkElement"/> container so that it can be added to the managed visual tree.
		/// </summary>
		/// <remarks>
		/// This method is present to support adding native view types on Android, iOS and MacOS to Uno's visual tree.
		///
		/// Calling it with a type that's already a <see cref="FrameworkElement"/> will throw an <see cref="InvalidOperationException"/>.
		/// If there's a possibility that the wrapped type may be a <see cref="FrameworkElement"/>, use <see cref="TryAdaptNative(_View)"/>
		/// instead.
		/// </remarks>
		public static FrameworkElement AdaptNative(_View nativeView)
		{
			if (nativeView is FrameworkElement)
			{
				throw new InvalidOperationException($"{nameof(AdaptNative)}() should only be called for non-{nameof(FrameworkElement)} native views." +
					$"Use {nameof(TryAdaptNative)} if it's not known whether view will be native.");
			}

			return new ContentPresenter
			{
				IsNativeHost = true,
				Content = nativeView
			};
		}

		/// <summary>
		/// Adapts a native view by wrapping it in a <see cref="FrameworkElement"/> container so that it can be added to the managed visual tree.
		///
		/// This method is safe to call for any view. If <paramref name="view"/> is a <see cref="FrameworkElement"/>, it will simply be returned unmodified.
		/// </summary>
		public static FrameworkElement TryAdaptNative(_View view)
		{
			if (view is FrameworkElement fe)
			{
				return fe;
			}

			return AdaptNative(view);
		}
	}
}
