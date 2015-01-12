// Decompiled with JetBrains decompiler
// Type: VRage.Common.Utils.MyRSA
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using VRage.Cryptography;

namespace VRage.Common.Utils
{
    public class MyRSA
    {
        private HashAlgorithm m_hasher;

        public HashAlgorithm HashObject
        {
            get { return this.m_hasher; }
        }

        public MyRSA()
        {
            this.m_hasher = (HashAlgorithm) MySHA256.Create();
            this.m_hasher.Initialize();
        }

        public void GenerateKeys(string publicKeyFileName, string privateKeyFileName)
        {
            byte[] publicKey;
            byte[] privateKey;
            this.GenerateKeys(out publicKey, out privateKey);
            if (publicKey == null || privateKey == null)
                return;
            File.WriteAllText(publicKeyFileName, Convert.ToBase64String(publicKey));
            File.WriteAllText(privateKeyFileName, Convert.ToBase64String(privateKey));
        }

        public void GenerateKeys(out byte[] publicKey, out byte[] privateKey)
        {
            RSACryptoServiceProvider cryptoServiceProvider = (RSACryptoServiceProvider) null;
            try
            {
                cryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters()
                {
                    ProviderType = 1,
                    Flags = CspProviderFlags.UseArchivableKey,
                    KeyNumber = 1
                });
                cryptoServiceProvider.PersistKeyInCsp = false;
                publicKey = cryptoServiceProvider.ExportCspBlob(false);
                privateKey = cryptoServiceProvider.ExportCspBlob(true);
            }
            catch (Exception ex)
            {
                publicKey = (byte[]) null;
                privateKey = (byte[]) null;
            }
            finally
            {
                if (cryptoServiceProvider != null)
                    cryptoServiceProvider.PersistKeyInCsp = false;
            }
        }

        public string SignData(string data, string privateKey)
        {
            byte[] inArray;
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                cryptoServiceProvider.PersistKeyInCsp = false;
                byte[] bytes = new UTF8Encoding().GetBytes(data);
                try
                {
                    cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(privateKey));
                    inArray = cryptoServiceProvider.SignData(bytes, (object) this.m_hasher);
                }
                catch (CryptographicException ex)
                {
                    return (string) null;
                }
                finally
                {
                    cryptoServiceProvider.PersistKeyInCsp = false;
                }
            }
            return Convert.ToBase64String(inArray);
        }

        public string SignHash(byte[] hash, byte[] privateKey)
        {
            byte[] inArray;
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                cryptoServiceProvider.PersistKeyInCsp = false;
                try
                {
                    cryptoServiceProvider.ImportCspBlob(privateKey);
                    inArray = cryptoServiceProvider.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
                }
                catch (CryptographicException ex)
                {
                    return (string) null;
                }
                finally
                {
                    cryptoServiceProvider.PersistKeyInCsp = false;
                }
            }
            return Convert.ToBase64String(inArray);
        }

        public string SignHash(string hash, string privateKey)
        {
            byte[] inArray;
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                cryptoServiceProvider.PersistKeyInCsp = false;
                byte[] bytes = new UTF8Encoding().GetBytes(hash);
                try
                {
                    cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(privateKey));
                    inArray = cryptoServiceProvider.SignHash(bytes, CryptoConfig.MapNameToOID("SHA256"));
                }
                catch (CryptographicException ex)
                {
                    return (string) null;
                }
                finally
                {
                    cryptoServiceProvider.PersistKeyInCsp = false;
                }
            }
            return Convert.ToBase64String(inArray);
        }

        public bool VerifyHash(byte[] hash, byte[] signedHash, byte[] publicKey)
        {
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                try
                {
                    cryptoServiceProvider.ImportCspBlob(publicKey);
                    return cryptoServiceProvider.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signedHash);
                }
                catch (CryptographicException ex)
                {
                    return false;
                }
                finally
                {
                    cryptoServiceProvider.PersistKeyInCsp = false;
                }
            }
        }

        public bool VerifyHash(string hash, string signedHash, string publicKey)
        {
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                byte[] bytes = new UTF8Encoding().GetBytes(hash);
                byte[] rgbSignature = Convert.FromBase64String(signedHash);
                try
                {
                    cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(publicKey));
                    return cryptoServiceProvider.VerifyHash(bytes, CryptoConfig.MapNameToOID("SHA256"), rgbSignature);
                }
                catch (CryptographicException ex)
                {
                    return false;
                }
                finally
                {
                    cryptoServiceProvider.PersistKeyInCsp = false;
                }
            }
        }

        public bool VerifyData(string originalMessage, string signedMessage, string publicKey)
        {
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
            {
                byte[] bytes = new UTF8Encoding().GetBytes(originalMessage);
                byte[] signature = Convert.FromBase64String(signedMessage);
                try
                {
                    cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(publicKey));
                    return cryptoServiceProvider.VerifyData(bytes, (object) this.m_hasher, signature);
                }
                catch (CryptographicException ex)
                {
                    return false;
                }
                finally
                {
                    cryptoServiceProvider.PersistKeyInCsp = false;
                }
            }
        }
    }
}