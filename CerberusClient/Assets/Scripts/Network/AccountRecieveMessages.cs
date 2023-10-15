using NovaCore;
using Packets;

public static class AccountRecieveMessages {

    [MessageHandler((ushort)LoginServerPackets.LS_LoginConfirmed)]
    private static void Packet_LoginConfirmed(Message message) {
        MenuManager.instance.GoToMainMenu();
    }
}