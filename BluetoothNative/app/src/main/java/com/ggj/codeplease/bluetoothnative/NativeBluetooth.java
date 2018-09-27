package com.ggj.codeplease.bluetoothnative;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothServerSocket;
import android.bluetooth.BluetoothSocket;
import android.util.Log;

import java.io.IOException;
import java.io.OutputStream;
import java.util.UUID;

public class NativeBluetooth
{
    private static final String TAG = "NativeBluetooth";

    private static BluetoothAdapter mBluetoothAdapter;
    private static BluetoothSocket btSock;
    private static BluetoothServerSocket btServerSock;
    private static OutputStream outputStream;

    public static void Initialize()
    {
        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
        if(mBluetoothAdapter == null) {
            Log.d(TAG, "Your device does not support Bluetooth");
        }

        try{
            btServerSock = mBluetoothAdapter.listenUsingInsecureRfcommWithServiceRecord("LGP", UUID.fromString("00112233-4455-6677-8899-aabbccddeeff"));
            btSock = btServerSock.accept();
        }
        catch (IOException e)
        {
        }

        if(btSock != null){
            Log.d(TAG, "Null returned");
        }
    }

    public static void WriteOutputBuffer(int code)
    {
        try {
            outputStream = btSock.getOutputStream();
            outputStream.write(code);

            //send what is already in buffer
            outputStream.flush();
        } catch (IOException e) {
            Log.e(TAG, "Exception during write", e);
        }
    }
}
