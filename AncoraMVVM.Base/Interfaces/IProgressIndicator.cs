using System.ComponentModel;

namespace AncoraMVVM.Base.Interfaces
{
    public interface IProgressIndicator : INotifyPropertyChanged
    {
        bool IsLoading { get; set; }
        string Text { get; set; }

        /// <summary>
        /// Clear the indicator: empty the text and force stop the animation.
        /// </summary>
        void ClearIndicator();
    }
}
