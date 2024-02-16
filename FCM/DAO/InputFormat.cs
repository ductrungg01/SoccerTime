using System;
using System.Collections.Generic;
using System.Text;

namespace FCM.DAO
{
    public class InputFormat
    {
        private static InputFormat instance;
        public static InputFormat Instance
        {
            get { if (instance == null) instance = new InputFormat(); return instance; }
            set => instance = value;
        }
        public bool isNumber(string s)
        {
            foreach (char i in s)
            {
                if ('0' > i || i > '9')
                    return false;
            }
            return true;
        }
        public string FomartSpace(string s)
        {
            string result = "";
            while (s.Length > 0 && s[0] == ' ')
                s = s.Remove(0, 1);
            while (s.Length > 0 && s[s.Length - 1] == ' ')
                s = s.Remove(s.Length - 1, 1);
            int i = 1;
            result = s;
            return result;
        }
    }
}
