using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class IpAddressManager
{
    public static string GetIpAddress(IpAddressVersion version)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (version == IpAddressVersion.v6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType type2 = NetworkInterfaceType.Ethernet;

            if (
                (item.NetworkInterfaceType == type1 || item.NetworkInterfaceType == type2)
                && item.OperationalStatus == OperationalStatus.Up
            )
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (version == IpAddressVersion.v4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }

                    //IPv6
                    else if (version == IpAddressVersion.v6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }
}

public enum IpAddressVersion
{
    v4, v6
}