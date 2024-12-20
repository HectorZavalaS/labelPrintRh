using System;
using System.Management;

namespace file_monitor
{
    public class EventWatcherAsync
    {
        string ComputerName = "localhost";
        string WmiQuery;
        ManagementEventWatcher Watcher;
        ManagementScope Scope;
        delegate void addLineDelegate(string texto);
        delegate void addLine_PFDelegate(string texto);
        String m_path;


        private void WmiEventHandler(object sender, EventArrivedEventArgs e)
        {
            // e.NewEvent
            string wclass = ((ManagementBaseObject)e.NewEvent).SystemProperties["__Class"].Value.ToString();
            string wop = string.Empty;

            string wfilename = ((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["FileName"].ToString();

            if (!string.IsNullOrEmpty(((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["Extension"].ToString()))
            {
                wfilename += "." + ((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["Extension"].ToString();
            }

            string wFullpath = ((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["Name"].ToString();

            switch (wclass)
            {
                case "__InstanceModificationEvent":
                    wop = "Modified";
                    //addLine_PF(String.Format("The File {0} was {1}", wfilename, wop));
                    //m_operations.procesFile(wFullpath, wfilename, m_path);
                    break;
                case "__InstanceCreationEvent":
                    wop = "Created";
                    //addLine_PF(String.Format("The File {0} was {1}", wfilename, wop));
                    //m_operations.procesFile(wFullpath, wfilename, m_path);
                    break;
                case "__InstanceDeletionEvent":
                    wop = "Moved";
                    //addLine_PF(String.Format("The File {0} was {1}", wfilename, wop));
                    break;
            }
            //string wfilename = ((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["FileName"].ToString();

            //if (!string.IsNullOrEmpty(((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["Extension"].ToString()))
            //{
            //    wfilename += "." + ((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["Extension"].ToString();
            //}
            Console.WriteLine(String.Format("The File {0} was {1}", wfilename, wop));



        }

        public EventWatcherAsync(String IP, String tgDrive, String tgPath)
        {
            try
            {
                ComputerName = IP;

                m_path = tgDrive + tgPath;

                //addLine("Setting monitor in " + tgDrive + tgPath);
                if (!ComputerName.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    System.Management.ConnectionOptions Conn = new System.Management.ConnectionOptions();
                    Conn.Username = "Administrator";
                    Conn.Password = "1T4dmin$11xXX11";
                    Conn.Authority = "ntlmdomain:SEM";
                    Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), Conn);
                }
                else
                    Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);
                Scope.Connect();
                WmiQuery = @"Select * From __InstanceOperationEvent Within 1 
                Where TargetInstance ISA 'CIM_DataFile' and TargetInstance.Drive = '" + tgDrive + "' AND TargetInstance.Path='" + tgPath + "'";

                Watcher = new ManagementEventWatcher(Scope, new EventQuery(WmiQuery));
                Watcher.EventArrived += new EventArrivedEventHandler(this.WmiEventHandler);

                //m_operations = new COperations(ref mText1, ref console, ref prFiles, program, dJ, pok, haspanel);
                //addLine("Setting monitor in " + tgDrive + tgPath + " successful");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0} Trace {1}", e.Message, e.StackTrace);
            }

        }
        public bool startWatch()
        {
            bool result = false;

            if (Watcher != null)
            {
                Watcher.Start();
                result = true;
            }
            return result;
        }
        public bool stopWatch()
        {
            bool result = false;

            if (Watcher != null)
            {
                Watcher.Stop();
                result = true;
            }
            return result;
        }
    }
}
