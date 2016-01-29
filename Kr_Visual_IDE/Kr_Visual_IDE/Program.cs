/*
 *                                                  Vendor:  javavirys
 *      mail:    mailto:javavirys@mail.ru                                                 web:     http://srcblog.ru        *
*/ 

using System;
using System.Windows.Forms;

namespace Kr_Visual_IDE
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            Application.Run(form);
        }
    }
}
