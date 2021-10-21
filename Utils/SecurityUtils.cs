using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace RemoteControlV3.Utils
{
    public static class SecurityUtils
    {
        public static PermissionLevel GetPermissionLevel()
        {
            switch (Program.Platform)
            {
                case Platform.Linux:
                {
                    if (geteuid() == 0)
                    {
                        return PermissionLevel.Root;
                    }
                    else
                    {
                        return PermissionLevel.User;
                    }
                }
                case Platform.Windows:
                {
                    var windowsIdentity = WindowsIdentity.GetCurrent();
                    var windowsPrincipal = new WindowsPrincipal(windowsIdentity);

                    if (windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
                    {
                        return PermissionLevel.Root;
                    }
                    else
                    {
                        return PermissionLevel.User;
                    }
                }
            }

            return PermissionLevel.User;
        }
        
        [DllImport("libc")]
        private static extern int geteuid();
    }
}