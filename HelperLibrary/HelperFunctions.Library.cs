using AspNetCoreHero.ToastNotification.Abstractions;

namespace Budget_Man.Helper.Library {
    public class HelperFunctions{

        // For Toasts, local, C#.
    public INotyfService _notifyService { get; }

    public HelperFunctions(INotyfService notifyService){
        _notifyService = notifyService;
    }


        public static int ConvertStringToInt(string str){
            return Convert.ToInt32(str);
        }

        public static float ConvertStringToFloat(string str){
            return Single.Parse(str);
        }

         public void toasterTest(string msg, int type)
    {
        switch (type)
        {
            case 1: //SUCCESS
                _notifyService.Custom(msg, 5, "#135224", "fa fa-gear"); break;
            case 2: //ERROR
                _notifyService.Error(msg); break;
            default://INFO
                _notifyService.Custom(msg, 5, "whitesmoke", "fa fa-gear"); break;
        }
    }
    }
}