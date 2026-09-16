using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TrailsintheSky2ndChapter.Model;

namespace TrailsintheSky2ndChapter.ViewModel
{
	internal class GeneralViewModel
	{
		public GeneralModel Model { get; init; }
		public GeneralViewModel(GeneralModel model)
		{
			Model = model;
		}

		public uint Money
		{
			get => Model.Money;
			set
			{
				value = Util.ValidationValue(0, 99999999, value);
				Model.Money = value;
			}
		}
	}
}
