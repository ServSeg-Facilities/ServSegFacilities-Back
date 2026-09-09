namespace ServSegFacilitiesAPI.Application.Convertions
{
    public class ImagemParaBytes
    {
        public static byte[]? ConverterImagem(IFormFile? imagem)
        {
            if (imagem == null) return null;
            using var ms = new MemoryStream();
            imagem.CopyTo(ms);
            return ms.ToArray();
        }
    }
}