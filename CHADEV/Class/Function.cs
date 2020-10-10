using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

public class Function
{
    public const string SOUND_FILE_WAV = "Beep.wav";
    public const string SOUND_FILE_MP3 = "Silent.mp3";
    public const string SIGNED_FLOAT_KEY_REGX = "[^0-9,.]";
    public const string SIGNED_INTEGER_KEY_REGX = "[^0-9]";
    //public const string NETWORK_SOUND_PATH = @"\\" + CONFIG.DRIVER_CONNECTION + @"\tuwdev-report\Sound\" + SOUND_FILE_WAV;
    //public const string NETWORK_SOUND_PATH2 = @"\\" + CONFIG.DRIVER_CONNECTION + @"\tuwdev-report\Sound\" + SOUND_FILE_MP3;
    //public const string NETWORK_SOUND_PATH = @"\\192.168.1.11\Software_TUW\Sound\Beep.wav";

    //public const string SIGNED_FLOAT_REGX = @"^[+-]?[0-9]*(\.[0-9]+)?([Ee][+-]?[0-9]+)?$";
    //public const string SIGNED_INTEGER_REGX = @"^[+-]?[0-9]+$";

    public Function() { }

    public bool msgQuiz(string message, string caption = "CONFIRM ?")
    {
        bool result = false;
        if (MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }



    public void msgInfo(string message, string caption = "COMPLETE")
    {
        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public void msgWarning(string message, string caption = "WARNING !")
    {
        if (System.IO.File.Exists(@"D:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"D:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        else if (System.IO.File.Exists(@"C:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"C:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        //else if (System.IO.File.Exists(NETWORK_SOUND_PATH) == true)
        //{
        //    SoundPlayer sp = new SoundPlayer(NETWORK_SOUND_PATH);
        //    sp.Play();
        //}
        else
        {
            Console.Beep();
            Console.Beep();
        }

        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public void msgError(string message, string caption = "ERROR !!")
    {
        if (System.IO.File.Exists(@"D:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"D:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        else if (System.IO.File.Exists(@"C:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"C:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        //else if (System.IO.File.Exists(NETWORK_SOUND_PATH) == true)
        //{
        //    SoundPlayer sp = new SoundPlayer(NETWORK_SOUND_PATH);
        //    sp.Play();
        //}
        else
        {
            Console.Beep();
            Console.Beep();
        }

        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void msgERROR(string message, string caption = "ERROR !!")
    {
        if (System.IO.File.Exists(@"D:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"D:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        else if (System.IO.File.Exists(@"C:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"C:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        //else if (System.IO.File.Exists(NETWORK_SOUND_PATH2) == true)
        //{
        //    SoundPlayer sp = new SoundPlayer(NETWORK_SOUND_PATH2);
        //    sp.Play();
        //}
        else
        {
            Console.Beep();
            Console.Beep();
        }

        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void VoiceError()
    {
        if (System.IO.File.Exists(@"D:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"D:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        else if (System.IO.File.Exists(@"C:\Sound\" + SOUND_FILE_WAV) == true)
        {
            SoundPlayer sp = new SoundPlayer(@"C:\Sound\" + SOUND_FILE_WAV);
            sp.Play();
        }
        //else if (System.IO.File.Exists(NETWORK_SOUND_PATH) == true)
        //{
        //    SoundPlayer sp = new SoundPlayer(NETWORK_SOUND_PATH);
        //    sp.Play();
        //}
        else
        {
            Console.Beep();
            Console.Beep();
        }
    }

    public void isDecimalOnly(TextBox txtBox)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(txtBox.Text, Function.SIGNED_FLOAT_KEY_REGX))
        {
            //MessageBox.Show("Please enter only numbers.");
            txtBox.Text = txtBox.Text.Remove(txtBox.Text.Length - 1);
        }
    }

    public void isNumberOnly(TextBox txtBox)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(txtBox.Text, Function.SIGNED_INTEGER_KEY_REGX))
        {
            txtBox.Text = txtBox.Text.Remove(txtBox.Text.Length - 1);
        }

    }

    public bool checkDigitOnly(int index)
    {
        bool chkDigit = false;
        switch (index)
        {
            case 48 - 57:
                chkDigit = false;
                break;
            case 8:
            case 13:
                chkDigit = false;
                break;
            default:
                chkDigit = true;
                break;
        }

        return chkDigit;
    }

    public string chkErrorCase(string data)
    {
        string result = data;
        if (result != "")
        {
            result = result.Replace("~", "-");
            result = result.Replace("'", "");
            result = result.Trim();
        }
        return result;
    }

    public string findLastDateofMonth(int Month, int Year)
    {
        string lastDate = Convert.ToDateTime(DateTime.DaysInMonth(Year, Month).ToString() + "/" + Month + "/" + Year).ToString("dd/MM/yyyy");

        return lastDate;
    }

    public string findLastofMonth(int Month, int Year)
    {
        string lastDate = Convert.ToDateTime("01/" + Month + "/" + Year).ToString("yyyy-MM-") + (DateTime.DaysInMonth(Convert.ToInt32(Year), Convert.ToInt32(Month))).ToString();
        return lastDate;
    }

    public string convertDateFormat(string DateF)
    {
        string sendDate = "";
        if (DateF == "")
        {
            sendDate = "";
        }
        else if (DateF.Length == 8)
        {
            sendDate = "0" + DateF.Substring(0, 2) + "0" + DateF.Substring(2, 6);
        }
        else if (DateF.Length == 9)
        {
            if (DateF.Substring(1, 1) == "/")
            {
                sendDate = "0" + DateF;
            }
            else
            {
                sendDate = DateF.Substring(0, 3) + "0" + DateF.Substring(3, 6);
            }
        }
        else
        {
            sendDate = DateF;
        }

        return sendDate;
    }


    public double Evaluate(String expr)
    {
        // 2+(100/5)+10 = 32
        //((2.5+10)/5)+2.5 = 5
        // (2.5+10)/5+2.5 = 1.6666
        Stack<String> stack = new Stack<String>();

        string value = "";
        for (int i = 0; i < expr.Length; i++)
        {
            String s = expr.Substring(i, 1);
            char chr = s.ToCharArray()[0];

            if (!char.IsDigit(chr) && chr != '.' && value != "")
            {
                stack.Push(value);
                value = "";
            }

            if (s.Equals("("))
            {

                string innerExp = "";
                i++; //Fetch Next Character
                int bracketCount = 0;
                for (; i < expr.Length; i++)
                {
                    s = expr.Substring(i, 1);

                    if (s.Equals("("))
                        bracketCount++;

                    if (s.Equals(")"))
                        if (bracketCount == 0)
                            break;
                        else
                            bracketCount--;


                    innerExp += s;
                }

                stack.Push(Evaluate(innerExp).ToString());

            }
            else if (s.Equals("+")) stack.Push(s);
            else if (s.Equals("-")) stack.Push(s);
            else if (s.Equals("*")) stack.Push(s);
            else if (s.Equals("/")) stack.Push(s);
            else if (s.Equals("sqrt")) stack.Push(s);
            else if (s.Equals(")"))
            {
            }
            else if (char.IsDigit(chr) || chr == '.')
            {
                value += s;

                if (value.Split('.').Length > 2)
                    throw new Exception("Invalid decimal.");

                if (i == (expr.Length - 1))
                    stack.Push(value);

            }
            else
                throw new Exception("Invalid character.");

        }


        double result = 0;
        while (stack.Count >= 3)
        {

            double right = Convert.ToDouble(stack.Pop());
            string op = stack.Pop();
            double left = Convert.ToDouble(stack.Pop());

            if (op == "+") result = left + right;
            else if (op == "+") result = left + right;
            else if (op == "-") result = left - right;
            else if (op == "*") result = left * right;
            else if (op == "/") result = left / right;

            stack.Push(result.ToString());
        }


        return Convert.ToDouble(stack.Pop());
    }

    public void SetKeyboardLayout(string layout) //EX. SetKeyboardLayout("FA");
    {
        foreach (InputLanguage Lng in InputLanguage.InstalledInputLanguages)
        {
            if (Lng.Culture.EnglishName.ToUpper().StartsWith(layout))
            {
                InputLanguage.CurrentInputLanguage = Lng;
            }

        }

    }

    public string CONVERT_MONTH_TO_NUMBER(string MONTH_NAME)
    {
        string ret_Month = "";
        MONTH_NAME = MONTH_NAME.Trim().ToLower();
        if (MONTH_NAME != "")
        {
            switch (MONTH_NAME)
            {
                case "january":
                    {
                        ret_Month = "01";
                        break;
                    }
                case "february":
                    {
                        ret_Month = "02";
                        break;
                    }
                case "march":
                    {
                        ret_Month = "03";
                        break;
                    }
                case "april":
                    {
                        ret_Month = "04";
                        break;
                    }
                case "may":
                    {
                        ret_Month = "05";
                        break;
                    }
                case "june":
                    {
                        ret_Month = "06";
                        break;
                    }
                case "july":
                    {
                        ret_Month = "07";
                        break;
                    }
                case "august":
                    {
                        ret_Month = "08";
                        break;
                    }
                case "september":
                    {
                        ret_Month = "09";
                        break;
                    }
                case "october":
                    {
                        ret_Month = "10";
                        break;
                    }
                case "november":
                    {
                        ret_Month = "11";
                        break;
                    }
                case "december":
                    {
                        ret_Month = "12";
                        break;
                    }
                default:
                    {
                        ret_Month = "";
                        break;
                    }

            }
        }
        return ret_Month;
    }

    public string CONVERT_THAI_DATE(string MONTH_SHORT_LONG = "SHORT", string xDATE = "")
    {
        if (xDATE == "")
        {
            xDATE = DateTime.Now.ToString("dd/MM/yyyy");
        }

        string NEW_DATE = "";
        string xD = Convert.ToDateTime(xDATE).ToString("dd");
        string xM = Convert.ToDateTime(xDATE).ToString("MM");
        string xY = Convert.ToString(Convert.ToInt32(Convert.ToDateTime(xDATE).ToString("yyyy")) + 543);

        string xMS = "";
        string xML = "";

        switch (Convert.ToInt32(xM))
        {
            case 1:
                {
                    xMS = "ม.ค.";
                    xML = "มกราคม";
                    break;
                }
            case 2:
                {
                    xMS = "ก.พ.";
                    xML = "กุมภาพันธ์";
                    break;
                }
            case 3:
                {
                    xMS = "มี.ค.";
                    xML = "มีนาคม";
                    break;
                }
            case 4:
                {
                    xMS = "เม.ย.";
                    xML = "เมษายน";
                    break;
                }
            case 5:
                {
                    xMS = "พ.ค.";
                    xML = "พฤษภาคม";
                    break;
                }
            case 6:
                {
                    xMS = "มิ.ย.";
                    xML = "มิถุนายน";
                    break;
                }
            case 7:
                {
                    xMS = "ก.ค.";
                    xML = "กรกฎาคม";
                    break;
                }
            case 8:
                {
                    xMS = "ส.ค.";
                    xML = "สิงหาคม";
                    break;
                }
            case 9:
                {
                    xMS = "ก.ย.";
                    xML = "กันยายน";
                    break;
                }
            case 10:
                {
                    xMS = "ต.ค.";
                    xML = "ตุลาคม";
                    break;
                }
            case 11:
                {
                    xMS = "พ.ย.";
                    xML = "พฤศจิกายน";
                    break;
                }
            case 12:
                {
                    xMS = "ธ.ค.";
                    xML = "ธันวาคม";
                    break;
                }
            default:
                {
                    xM = "-";
                    xML = "-";
                    break;
                }
        }

        if (MONTH_SHORT_LONG == "SHORT")
        {
            NEW_DATE = xD + " " + xMS + " " + xY;
        }
        else if (MONTH_SHORT_LONG == "LONG")
        {
            NEW_DATE = xD + " " + xML + " " + xY;
        }

        return NEW_DATE;
    }

    public string FIND_SEASON()
    {
        string retSEASON = "";

        string strYEAR = DateTime.Now.ToString("yy");
        string strMONTH = DateTime.Now.ToString("MM");
        switch (Convert.ToInt32(strMONTH))
        {
            case 1:
                retSEASON = strYEAR + "SS (NOV - JAN)";
                break;
            case 11:
            case 12:
                retSEASON = Convert.ToDouble(Convert.ToDouble(strYEAR) + 1).ToString("00") + "SS (NOV - JAN)";
                break;
            case 2:
            case 3:
            case 4:
                retSEASON = strYEAR + "SS (FEB - APR)";
                break;
            case 5:
            case 6:
            case 7:
                retSEASON = strYEAR + "AW (MAY - JUL)";
                break;
            case 8:
            case 9:
            case 10:
                retSEASON = strYEAR + "AW (AUG - OCT)";
                break;
            default:
                retSEASON = "";
                break;
        }

        return retSEASON;
    }
}


