using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;
using src.Classes;


namespace src
{
    public partial class InfoForm : Form
    {

        public InfoForm()
        {
            InitializeComponent();

            var shell = Activator.CreateInstance(Type.GetTypeFromCLSID(Guids.CLSID_ImmersiveShell)) as IServiceProvider10;
            if (shell == null)
            {
                throw new Exception("Could not get type from CLSID");
            }
            DesktopManager = (IVirtualDesktopManagerInternal)shell.QueryService(Guids.CLSID_VirtualDesktopManagerInternal, Guids.IID_IVirtualDesktopManagerInternal);


            RegisterHotKeysForNumpad();

        }

        
        
        

        //Imports
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);



        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private IVirtualDesktopManagerInternal DesktopManager;


        //Constants
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8,
            NoRepeat = 0x4000,
        }

        static readonly int WM_HOTKEY = 0x312;

        private void UnregisterHotKeys()
        {
            for (int i = 0; i < 9; i++)
            {
                UnregisterHotKey(this.Handle, i);
            }
        }

        private void RegisterHotKeysForNumpad()
        {
            int hotkeyModifiers = (int)KeyModifier.Control | (int)KeyModifier.WinKey;

            RegisterHotKey(this.Handle, 0, hotkeyModifiers, Keys.NumPad0.GetHashCode());
            RegisterHotKey(this.Handle, 1, hotkeyModifiers, Keys.NumPad1.GetHashCode());
            RegisterHotKey(this.Handle, 2, hotkeyModifiers, Keys.NumPad2.GetHashCode());
            RegisterHotKey(this.Handle, 3, hotkeyModifiers, Keys.NumPad3.GetHashCode());
            RegisterHotKey(this.Handle, 4, hotkeyModifiers, Keys.NumPad4.GetHashCode());
            RegisterHotKey(this.Handle, 5, hotkeyModifiers, Keys.NumPad5.GetHashCode());
            RegisterHotKey(this.Handle, 6, hotkeyModifiers, Keys.NumPad6.GetHashCode());
            RegisterHotKey(this.Handle, 7, hotkeyModifiers, Keys.NumPad7.GetHashCode());
            RegisterHotKey(this.Handle, 8, hotkeyModifiers, Keys.NumPad8.GetHashCode());
        }
        private void RegisterHotKeysForNonNumpad()
        {
            int hotkeyModifiers = (int)KeyModifier.Control | (int)KeyModifier.WinKey;

            RegisterHotKey(this.Handle, 0, hotkeyModifiers, Keys.Escape.GetHashCode());
            RegisterHotKey(this.Handle, 1, hotkeyModifiers, Keys.F1.GetHashCode());
            RegisterHotKey(this.Handle, 2, hotkeyModifiers, Keys.F2.GetHashCode());
            RegisterHotKey(this.Handle, 3, hotkeyModifiers, Keys.F3.GetHashCode());
            RegisterHotKey(this.Handle, 4, hotkeyModifiers, Keys.F4.GetHashCode());
            RegisterHotKey(this.Handle, 5, hotkeyModifiers, Keys.F5.GetHashCode());
            RegisterHotKey(this.Handle, 6, hotkeyModifiers, Keys.F6.GetHashCode());
            RegisterHotKey(this.Handle, 7, hotkeyModifiers, Keys.F7.GetHashCode());
            RegisterHotKey(this.Handle, 8, hotkeyModifiers, Keys.F8.GetHashCode());
        }
        
        private void DynamicCheckBox_update(object sender, EventArgs e)
        {
            if (dynamicCheckBox.Checked)
            {
                
                UnregisterHotKeys();
                RegisterHotKeysForNonNumpad();
                Debug.WriteLine(hasNumpad);
                Debug.WriteLine("UnregisterHotKey for numpad");
            }
            else
            {
               
                UnregisterHotKeys();
                RegisterHotKeysForNumpad();
                Debug.WriteLine(hasNumpad);
                Debug.WriteLine("UnregisterHotKey for nonnumpad");
            }
        }
        



        private void InfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Unregistering hotkeys
            for (int i = 0; i < 10; i++)
            {
                UnregisterHotKey(this.Handle, i);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int hotkeyId = m.WParam.ToInt32();
                GoToDesktop(hotkeyId);
                Debug.WriteLine(hotkeyId);
            }
            base.WndProc(ref m);
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(
                new ProcessStartInfo("https://github.com/tabarra/txDeskSwitcher") { UseShellExecute = true }
            );
        }

        private void GoToDesktop(int desktopNumber)
        {
            var count = DesktopManager.GetCount();
            if (desktopNumber < 0)
            {
                desktopNumber = 0;
            }
            else if (desktopNumber >= count)
            {
                desktopNumber = count - 1;
            }

            Debug.WriteLine("Switching to desktop " + desktopNumber.ToString());

            // Get desktop
            IObjectArray allDesktops;
            DesktopManager.GetDesktops(out allDesktops);
            IVirtualDesktop targetDesktop;
            allDesktops.GetAt(desktopNumber, Guids.IID_IVirtualDesktop, out targetDesktop);

            // Switch to desktop
            DesktopManager.SwitchDesktop(targetDesktop);

            // Release COM objects
            Marshal.ReleaseComObject(allDesktops);
        }
    }
}
