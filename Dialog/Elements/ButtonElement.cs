//
// ButtonElement.cs
//
// Author:
//   Robert Kozak (rkozak@gmail.com / Twitter:@robertkozak
//
// Copyright 2011, Nowcom Corporation
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
	using System.Drawing;
	using MonoTouch.Foundation;
	using MonoMobile.Views;
	using MonoTouch.UIKit;

	/// <summary>
	///  The button element can be used to render a cell as a button that responds to Tap events
	/// </summary>
	[Preserve(AllMembers=true)]
	public class ButtonElement : Element, ITappable, ISelectable
	{
		public ICommand Command { get; set; }
		public object CommandParameter { get; set; }

		public ButtonElement() : this(string.Empty)
		{
		}

		public ButtonElement(string caption) : base(caption)
		{
			DataBinding = new ElementDataBinding(this);
		}

		public override void InitializeCell(UITableView tableView)
		{
			base.InitializeCell(tableView);
			
			Cell.Accessory = UITableViewCellAccessory.None;
			Cell.TextLabel.BackgroundColor = UIColor.Clear;
			Cell.TextLabel.TextAlignment = UITextAlignment.Center;
		}

		public override string ToString()
		{
			return Caption;
		}

		public void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath indexPath)
		{
			if (Command != null && Command.CanExecute(CommandParameter))
				Command.Execute(CommandParameter);
		}
	}
}

