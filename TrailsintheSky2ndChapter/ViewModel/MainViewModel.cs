using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace TrailsintheSky2ndChapter.ViewModel
{
	internal class MainViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private SaveData _saveData = new();

		public GeneralViewModel General { get; private set; } = new(new Model.GeneralModel());

		public ICommand LoadFileCommand { get; init; }
		public ICommand SaveFileCommand { get; init; }

		public MainViewModel()
		{
			LoadFileCommand = new ActionCommand(LoadFile);
			SaveFileCommand = new ActionCommand(SaveFile);
		}

		private void LoadFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			var general = _saveData.Load(dlg.FileName);
			if (general == null) return;

			General = new(general);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(General)));
		}

		private void SaveFile(object? parameter)
		{
			if (General == null) return;

			_saveData.Save(General.Model);
		}
	}
}
