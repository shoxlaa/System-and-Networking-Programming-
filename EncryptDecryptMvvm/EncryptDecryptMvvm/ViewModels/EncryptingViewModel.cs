using CommunityToolkit.Mvvm.ComponentModel;

namespace EncryptDecryptMvvm.ViewModels
{
    public partial class EncryptingViewModel : BaseViewModel 
    {
        [ObservableProperty]
        string _pop;

        public EncryptingViewModel()
        {
            Pop = "ffrfr";
        }
//        ThreadPool.QueueUserWorkItem((object value) =>
//            {
//                string text = File.ReadAllText(file.FullName);
//        _progress = 0;
//                string newVal = "";
//                lock (lockerKey)
//                {
//                    foreach (var c in text)
//                    {
//                        newVal += (char) ((int) c + 1);
//                        _progress++;
//                    }

//    text = newVal;
//                }

//File.WriteAllText($"{file.FullName.Remove(file.FullName.Length - 3)}enx", text);
//            }, ApartmentState.STA);


    }

}
