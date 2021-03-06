// 
// MonoMobileApplication.cs
// 
// Author:
//   Robert Kozak (rkozak@gmail.com / Twitter:@robertkozak)
// 
// Copyright 2011, Nowcom Corporation.
// 
// Code licensed under the MIT X11 license
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
namespace MonoMobile.Views
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using MonoMobile.Views;
	using MonoTouch.Foundation;
	using MonoTouch.UIKit;
	
	[Register("MonoMobileApplication")]
	public class MonoMobileApplication : UIApplication
	{
		private static DialogViewController _PreviousDialogViewController;
		private static UIViewController _PreviousController;
		private static UINavigationController _NavigationController;

		public const string Version = "0.9";

		public static Type[] ViewTypes { get; private set;}

		public static UIWindow Window { get; set; }
		public static UINavigationController NavigationController { get; set; }
		public static DialogViewController CurrentDialogViewController { get; set; }
		public static UIViewController CurrentViewController { get; set; }
		public static List<UIView> Views { get; set; }
		public static List<DialogViewController> DialogViewControllers { get; private set;}

		public static string Title { get; set; }
 
		public static Action ResumeFromBackgroundAction { get; set; }

		static MonoMobileApplication()
		{
			DialogViewControllers = new List<DialogViewController>();
		}
		
		public static void ToggleSearchbar()
		{
			if (CurrentDialogViewController != null)
			{
				CurrentDialogViewController.ToggleSearchbar();
			}
		}
		
		public static void PushView(UIView view)
		{
			PushView(view, true);
		}

		public static void PushView(UIView view, bool animated)
		{
			CurrentDialogViewController = CreateDialogViewController(view, false);
			CurrentViewController = CurrentDialogViewController;
			
			NavigationController.PushViewController(CurrentViewController, animated);
		}

		public static void PresentModelView(UIView view)
		{
			PresentModelView(view, UIModalTransitionStyle.CoverVertical);
		}

		public static void PresentModelView(Type viewType)
		{
			var view = Activator.CreateInstance(viewType) as UIView;
			var intializable = view as IInitializable;
			if(intializable != null)
			{
				intializable.Initialize();
			}

			PresentModelView(view);
		}

		public static void PresentModelView(UIView view, UIModalTransitionStyle transistionStyle)
		{			
			_PreviousController = CurrentViewController;
			_PreviousDialogViewController = CurrentDialogViewController;

			CurrentDialogViewController = CreateDialogViewController(view, true);

			_NavigationController = new UINavigationController();
			_NavigationController.ViewControllers = new UIViewController[] { CurrentDialogViewController };
		
			CurrentViewController = _NavigationController;
			
			CurrentViewController.ModalTransitionStyle = transistionStyle;

			NavigationController.PresentModalViewController(_NavigationController, true);
		}

		public static void DismissModalView(bool animated)
		{
			NavigationController.DismissModalViewControllerAnimated(animated);

			CurrentViewController = _PreviousController;
			CurrentDialogViewController = _PreviousDialogViewController;

			_PreviousController = null;
		}

		public static void Run(string title, Type mainViewType, string[] args)
		{
			Title = title;
			ViewTypes = new Type[] { mainViewType };
			UIApplication.Main(args, "MonoMobileApplication", "MonoMobileAppDelegate");
		}

		public static void Run(string title, Type[] viewTypes, string[] args)
		{
			Title = title;
			ViewTypes = viewTypes;
			UIApplication.Main(args, "MonoMobileApplication", "MonoMobileAppDelegate");
		}

		public static void Run(string delegateName, string[] args)
		{
			UIApplication.Main(args, "MonoMobileApplication", delegateName);
		}

		private static DialogViewController CreateDialogViewController(UIView view, bool isModal)
		{
			Theme theme = null;
			
			if (CurrentDialogViewController != null)
			{
				theme = CurrentDialogViewController.Root.Theme;
				theme.TableViewStyle = UITableViewStyle.Grouped;
			}
			
			string title = null;
			var hasCaption = view as IView;
			if (hasCaption != null)
			{
				title = hasCaption.Caption;
			}
			
			if (string.IsNullOrEmpty(title))
				title = MonoMobileApplication.Title;
			
			var dvc = new DialogViewController(title, view, true) { Autorotate = true };
			dvc.IsModal = isModal;
			
			return dvc;
		}	
	}
}
