using System.IO;

namespace Octodiff.Core
{
    public class SignatureWriter : ISignatureWriter
    {
        private readonly BinaryWriter signatureStream;

        public SignatureWriter(Stream signatureStream)
        {
            this.signatureStream = new BinaryWriter(signatureStream);
        }

        public void WriteMetadata(IHashAlgorithm hashAlgorithm, IRollingChecksum rollingChecksumAlgorithm, byte[] hash)
        {
            this.signatureStream.Write(BinaryFormat.SignatureHeader);
            this.signatureStream.Write(BinaryFormat.Version);
            this.signatureStream.Write(hashAlgorithm.Name);
            this.signatureStream.Write(rollingChecksumAlgorithm.Name);
            this.signatureStream.Write(BinaryFormat.EndOfMetadata);
        }

        public void WriteChunk(ChunkSignature signature)
        {
            this.signatureStream.Write(signature.Length);
            this.signatureStream.Write(signature.RollingChecksum);
            this.signatureStream.Write(signature.Hash);
        }
    }
}