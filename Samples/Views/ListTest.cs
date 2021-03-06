using System;
using MonoMobile.Views;
using System.Collections.ObjectModel;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
namespace Samples
{
	[Theme(typeof(HoneyDoTheme))]
	[Theme(typeof(FrostedTheme))]
	public class ListTest : ListView
	{
		protected new ObservableCollection<MovieViewModel> DataContext
		{
			get { return (ObservableCollection<MovieViewModel>)GetDataContext(); }
			set { SetDataContext(value); }
		}

		public override void Initialize()
		{
			Add();
		}
		
		[ToolbarButton(MonoTouch.UIKit.UIBarButtonSystemItem.Action)]
		public void Clear()
		{
			DataContext = null;
		}

		[ToolbarButton(MonoTouch.UIKit.UIBarButtonSystemItem.Add)]
		public void Add()
		{
			var dataModel = new MovieDataModel();
			dataModel.Load(20);
			Console.WriteLine("Data Loaded " + DateTime.Now);
		DataContext = dataModel.Movies;

		}

		[ToolbarButton]
		public void One()
		{
			DataContext.Insert(2, new MovieViewModel(){});	
		}

		public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
		}

		public override void UpdateCell(UITableViewCell cell, NSIndexPath indexPath)
		{
			if (DataContext != null && DataContext.Count > indexPath.Row)
				cell.TextLabel.Text = DataContext[indexPath.Row].ToString();
		}
	}
}

