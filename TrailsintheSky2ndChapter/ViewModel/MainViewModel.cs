using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Input;

namespace TrailsintheSky2ndChapter.ViewModel
{
	internal class MainViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public GeneralViewModel General { get; init; } = new();

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

		private void Initialize()
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(General)));
		}

		private void LoadFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Open(dlg.FileName, false, SaveData.PlatForm.Switch);
			Initialize();
		}

		private void SaveFile(object? parameter)
		{
			SaveData.Instance().Save();
		}

		private void ExportFile(object? parameter)
		{
			SaveFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Export(dlg.FileName);
		}

		private void ImportFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Import(dlg.FileName);
			Initialize();
		}
	}
}
