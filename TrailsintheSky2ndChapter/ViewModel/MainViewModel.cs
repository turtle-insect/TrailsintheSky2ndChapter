using Microsoft.Win32;
using System.ComponentModel;
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
		public ICommand ExportFileCommand { get; init; }
		public ICommand ImportFileCommand { get; init; }

		public MainViewModel()
		{
			LoadFileCommand = new ActionCommand(LoadFile);
			SaveFileCommand = new ActionCommand(SaveFile);
			ExportFileCommand = new ActionCommand(ExportFile);
			ImportFileCommand = new ActionCommand(ImportFile);
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
			_saveData.Reflection(General.Model);
			_saveData.Save();
		}

		private void ExportFile(object? parameter)
		{
			SaveFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			System.IO.File.WriteAllBytes(dlg.FileName, _saveData.Body);
		}

		private void ImportFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			_saveData.Body = System.IO.File.ReadAllBytes(dlg.FileName);
		}
	}
}
