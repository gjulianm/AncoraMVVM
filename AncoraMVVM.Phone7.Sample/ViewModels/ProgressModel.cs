using AncoraMVVM.Base;
using PropertyChanged;

namespace AncoraMVVM.Phone7.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class ProgressModel : ViewModelBase
    {
        public DelegateCommand AddLoading { get; set; }
        public DelegateCommand RemoveLoading { get; set; }
        public int LoadingCount { get; set; }
        public DelegateCommand ClearLoading { get; set; }
        public DelegateCommand SetText { get; set; }
        public string BarText { get; set; }

        public ProgressModel()
        {
            AddLoading = new DelegateCommand(() => { Progress.IsLoading = true; LoadingCount++; });
            RemoveLoading = new DelegateCommand(() => { Progress.IsLoading = false; if (LoadingCount > 0) LoadingCount--; });
            LoadingCount = 0;
            ClearLoading = new DelegateCommand(() => Progress.ClearIndicator());
            BarText = "";
            SetText = new DelegateCommand(() => Progress.Text = BarText);
        }
    }
}
