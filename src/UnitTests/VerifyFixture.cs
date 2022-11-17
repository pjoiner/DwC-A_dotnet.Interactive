namespace UnitTests
{
    public class VerifyFixture : IDisposable
    {
        public VerifyFixture()
        {
            Verifier.DerivePathInfo((sourceFile, projectDirectory, type, method) => new(
                directory: Path.Combine(projectDirectory, "Resources/html"),
                typeName: type.Name,
                methodName: method.Name));
        }

        public void Dispose()
        {
            
        }
    }
}
