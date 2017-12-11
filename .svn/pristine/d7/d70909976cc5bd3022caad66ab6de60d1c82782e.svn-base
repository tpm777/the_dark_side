Imports System.ComponentModel.Composition
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO

Namespace Engines
    <Export(GetType(IEncryption))> _
    <PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class EncryptionEngine
        Implements IEncryption

        Private Const ENCRYPTION_KEY As String = "Q3SFA@GGN"
        Private Const SALT_KEY As String = "Q3INFOtech@20151"
        ' This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        ' This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        ' 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        Private Shared ReadOnly initVectorBytes As Byte() = Encoding.ASCII.GetBytes(SALT_KEY)
        Private Const passPhrase As String = "Users12345"
        ' This constant is used to determine the keysize of the encryption algorithm.
        Private Const keysize As Integer = 256
        Private Shared Function ByteToString(buff As Byte()) As String
            Dim plainText As String = String.Empty

            For i As Integer = 0 To buff.Length - 1
                ' hex format
                plainText += buff(i).ToString("X2")
            Next
            Return (plainText)
        End Function

        Public Function DecryptEncryptedString(cipherText As String) As String Implements IEncryption.DecryptEncryptedString
            Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)
            Using password As New PasswordDeriveBytes(passPhrase, Nothing)
                Dim keyBytes As Byte() = password.GetBytes(keysize / 8)
                Using symmetricKey As New RijndaelManaged()
                    symmetricKey.Mode = CipherMode.CBC
                    Using decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
                        Using memoryStream As New MemoryStream(cipherTextBytes)
                            Using cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                                Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}
                                Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
                                Return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Function EncryptText(plainText As String) As String Implements IEncryption.EncryptText
            'string encryptedText = string.Empty;
            Dim encoding As New System.Text.ASCIIEncoding()

            Dim keyByte As Byte() = encoding.GetBytes(ENCRYPTION_KEY)
            Dim hmacsha1 As New HMACSHA1(keyByte)

            Dim messageBytes As Byte() = encoding.GetBytes(plainText)
            Dim hashmessage As Byte() = hmacsha1.ComputeHash(messageBytes)
            Return ByteToString(hashmessage)
        End Function

        Public Function GenerateEncryptedString(value As Object) As String Implements IEncryption.GenerateEncryptedString
            Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(value.ToString())
            Using password As New PasswordDeriveBytes(passPhrase, Nothing)
                Dim keyBytes As Byte() = password.GetBytes(keysize / 8)
                Using symmetricKey As New RijndaelManaged()
                    symmetricKey.Mode = CipherMode.CBC
                    Using encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
                        Using memoryStream As New MemoryStream()
                            Using cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
                                cryptoStream.FlushFinalBlock()
                                Dim cipherTextBytes As Byte() = memoryStream.ToArray()
                                Return Convert.ToBase64String(cipherTextBytes)
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Function
    End Class
End Namespace


