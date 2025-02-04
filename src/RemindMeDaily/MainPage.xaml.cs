using RemindMeDaily.ViewModels;

namespace RemindMeDaily.Pages
{
    public partial class MainPage : ContentPage
    {
        //private readonly RemindersViewModel _viewModel;

        public MainPage(RemindersViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        // protected override async void OnAppearing()
        // {
        //     base.OnAppearing();
        //     await _viewModel.LoadRemindersAsync();
        // }
    }
}
