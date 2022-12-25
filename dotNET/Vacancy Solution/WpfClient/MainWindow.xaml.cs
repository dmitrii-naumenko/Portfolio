using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void ButtonOnline_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(searchTextEditor.Text))
			{
				MessageBox.Show("Введите слово для поиска вначале");
				return;
			}

			using (var service = new VacancyServiceReference.VacancyServiceClient())
			{
				var vacancies = await service.GetOnlineVacanciesAsync(searchTextEditor.Text);
				ListBoxView.ItemsSource = vacancies;
			}
		}

		private async void ButtonLocal_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(searchTextEditor.Text))
			{
				MessageBox.Show("Введите слово для поиска вначале");
				return;
			}

			using (var service = new VacancyServiceReference.VacancyServiceClient())
			{
				var vacancies = await service.GetLocalVacanciesAsync(searchTextEditor.Text);
				ListBoxView.ItemsSource = vacancies;
			}
		}


		private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}
	}
}
