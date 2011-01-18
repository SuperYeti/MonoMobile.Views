using System;
using MonoTouch.MVVM;
namespace Samples
{
	public class AddressViewModel : ViewModel
	{
		public string Number 
		{
			get { return Get(()=>Number); }
			set { Set(()=>Number, value); }
		}

		public string Street
		{
			get { return Get (() => Street); }
			set { Set (() => Street, value); }
		}
		
		public string City
		{
			get { return Get (() => City); }
			set { Set (() => City, value); }
		}
		
		public string State
		{
			get { return Get (() => State); }
			set { Set (() => State, value); }
		}
		
		public string Zip
		{
			get { return Get (() => Zip); }
			set { Set (() => Zip, value); }
		}
	}
}

