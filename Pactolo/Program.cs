using Pactolo.scr.view;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Pactolo {
    static class Program {

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.ThreadException += new ThreadExceptionEventHandler(Form1_UIThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuInicial());
        }

        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t) {
             ShowThreadExceptionDialog("Erro", false, t.Exception);
        }

        private static void ShowThreadExceptionDialog(string title, bool debug, Exception e) {
            string errorMsg;
            if (debug) {
                errorMsg = "Ocorreu um erro! Mensagem:\n\n";
                errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            }
            else {
                errorMsg = e.Message;
            }
            MessageBox.Show(errorMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}
