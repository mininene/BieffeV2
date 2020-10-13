using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    public class ConnectionWrapper
    {
        [DllImport("th4log.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern float th4_version();
        public float get_version()
        {
            return th4_version();
        }


        [DllImport("th4log.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint th4_lasterror();
        public uint GetError()
        {
            return th4_lasterror();

        }


        [DllImport("th4log.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint th4_initSession(int version, [MarshalAs(UnmanagedType.LPStr)] string ipaddress);
        public uint ConnectSession(int version, string ipaddress)
        {
            return th4_initSession(version, ipaddress);
        }


        [DllImport("th4log.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint th4_connect(uint handle);
        public uint ConnectApi(uint handle)
        {
            return th4_connect(handle);
        }



        [DllImport("th4log.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint th4_readFile(uint handle, [MarshalAs(UnmanagedType.LPStr)] string remoteFile, [MarshalAs(UnmanagedType.LPStr)] string localFile);
        public uint GetData(uint handle, string remoteFile, string localFile)
        {
            return th4_readFile(handle, remoteFile, localFile);
        }



        [DllImport("th4log.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern uint th4_close(ref uint lhandle);
        public uint CloseConnection(ref uint lhandle)
        {
            return th4_close(ref lhandle);
        }


        [DllImport("th4log.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr th4_message(uint error, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer);
        public IntPtr GetMessage(uint error, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer)
        {
            return th4_message(error, buffer);


        }
    }
}
