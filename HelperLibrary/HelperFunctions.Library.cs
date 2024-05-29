namespace Budget_Man.Helper.Library {
    public class HelperFunctions{
        public static int ConvertStringToInt(string str){
            return Convert.ToInt32(str);
        }

        public static float ConvertStringToFloat(string str){
            return Single.Parse(str);
        }
    }
}