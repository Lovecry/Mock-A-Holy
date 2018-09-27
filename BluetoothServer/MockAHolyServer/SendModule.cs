using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net;

namespace MockAHolyServer
{
    public static class SendModule
    {
        public static ObexStatusCode SendFile(BluetoothAddress address, string file_path)
        {
            
            Uri uri = new Uri("obex://" + address.ToString() + "/" + file_path);
            ObexWebResponse response;
            try
            {
                ObexWebRequest request = new ObexWebRequest(uri);
                request.ReadFile(file_path);
                response = (ObexWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                return ObexStatusCode.InternalServerError;
            }

            return response.StatusCode;
        }
    }
}
