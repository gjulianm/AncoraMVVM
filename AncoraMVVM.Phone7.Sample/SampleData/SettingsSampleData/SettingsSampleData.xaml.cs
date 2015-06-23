//      *********    NO MODIFIQUE ESTE ARCHIVO     *********
//      Este archivo se regenera mediante una herramienta de diseño.
//       Si realiza cambios en este archivo, puede causar errores.

namespace AncoraMVVM.Phone.Sample.SampleData.SettingsSampleData
{
// Para reducir de forma significativa la superficie de los datos de ejemplo en la aplicación de producción, puede establecer
// la constante de compilación condicional DISABLE_SAMPLE_DATA y deshabilitar los datos de ejemplo en tiempo de ejecución.
#if DISABLE_SAMPLE_DATA
	internal class SettingsSampleData { }
#else

	public class SettingsSampleData : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public SettingsSampleData()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/AncoraMVVM.Phone7.Sample;component/SampleData/SettingsSampleData/SettingsSampleData.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private Settings _Settings = new Settings();

		public Settings Settings
		{
			get
			{
				return this._Settings;
			}
		}
	}

	public class SettingsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private bool _Value = false;

		public bool Value
		{
			get
			{
				return this._Value;
			}

			set
			{
				if (this._Value != value)
				{
					this._Value = value;
					this.OnPropertyChanged("Value");
				}
			}
		}

		private Options _Options = new Options();

		public Options Options
		{
			get
			{
				return this._Options;
			}
		}
	}

	public class Settings : System.Collections.ObjectModel.ObservableCollection<SettingsItem>
	{ 
	}

	public class OptionsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}

	public class Options : System.Collections.ObjectModel.ObservableCollection<OptionsItem>
	{ 
	}
#endif
}
