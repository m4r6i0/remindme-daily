using RemindMeDaily.ViewModels;

namespace RemindMeDaily.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly RemindersViewModel _viewModel;

        public MainPage(RemindersViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadRemindersAsync();
        }
    }
}
